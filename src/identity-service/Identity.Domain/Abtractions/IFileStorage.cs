namespace Identity.Domain.Abtractions
{
    public interface IFileStorage
    {
        Task<string?> SaveFileAsync(string folder, string fileName, Stream fileStream, CancellationToken cancellationToken);
    }
}
