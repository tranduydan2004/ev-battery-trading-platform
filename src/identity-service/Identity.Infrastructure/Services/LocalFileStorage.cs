using Identity.Domain.Abtractions;

namespace Identity.Infrastructure.Services
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        public LocalFileStorage(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string?> SaveFileAsync(string folder, string fileName, Stream fileStream, CancellationToken cancellationToken)
        {
            var userFolder = Path.Combine(_env.WebRootPath, folder);
            if (!Directory.Exists(userFolder))
                Directory.CreateDirectory(userFolder);

            var filePath = Path.Combine(userFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(stream, cancellationToken);
            }

            return $"/{folder}/{fileName}";
        }
    }
}
