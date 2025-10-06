using Identity.Application;
using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddIndentityInfrastructure(cs);
builder.Services.AddIdentityApplication();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
/*builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // http
    options.ListenAnyIP(8081, o => o.UseHttps()); // https
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
// auto-migrate cho demo
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    bool hasMigrations = false;
    try
    {
        hasMigrations = db.Database.GetAppliedMigrations().Any();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Cannot connect to DB: " + ex.Message);
    }

    if (!hasMigrations)
        db.Database.EnsureCreated();
}

//app.MapGet("/identity/health", () => Results.Ok(new { ok = true, svc = "identity" }));
app.MapPost("/identity/login", async (
    [FromBody] LoginRequest request,
    IAuthService authService,
    CancellationToken cancellationToken) =>
{
    var result = await authService.LoginAsync(request, cancellationToken);
    return result is null
        ? Results.Unauthorized()
        : Results.Ok(result);
});
app.UseStaticFiles();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.Run();
