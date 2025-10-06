using Catalog.Application;
using Catalog.Application.Contracts;
using Catalog.Application.DTOs;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddCatalogInfrastructure(cs);
builder.Services.AddCatalogApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
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

// Health check, cache 30 giây
app.MapGet("/catalog/health", (HttpResponse response) =>
{
    response.Headers["Cache-Control"] = "public, max-age=30"; // cache 30 giây
    response.Headers["Expires"] = DateTime.UtcNow.AddSeconds(30).ToString("R");

    return Results.Ok(new { ok = true, svc = "catalog" });
});

// Search products, cache 60 giây
app.MapGet("/catalog/products/search", async (string q, IProductQueries query, HttpResponse response) =>
{
    response.Headers["Cache-Control"] = "public, max-age=60";
    response.Headers["Expires"] = DateTime.UtcNow.AddSeconds(60).ToString("R");

    var result = await query.SearchAsync(q);
    return Results.Ok(result);
});

// Create product, không cache (POST ??ng)
app.MapPost("/catalog/products", async (CreateProductReq req, IProductCommands cmd, HttpResponse response) =>
{
    response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    response.Headers["Pragma"] = "no-cache";
    response.Headers["Expires"] = "0";

    var id = await cmd.CreateAsync(new CreateProductDto
    {
        Title = req.Title,
        Price = req.Price,
        SellerId = req.SellerId,
        PickupAddress = req.PickupAddress,
        ProductName = req.ProductName,
        Description = req.Description,
        RegistrationCard = req.RegistrationCard,
        FileUrl = req.FileUrl,
        ImageUrl = req.ImageUrl
    });

    return Results.Created($"/catalog/products/{id}", new { productId = id });
});


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
record CreateProductReq(string Title, decimal Price, int SellerId, string PickupAddress,
    string ProductName, string Description, string? RegistrationCard, string? FileUrl, string? ImageUrl);