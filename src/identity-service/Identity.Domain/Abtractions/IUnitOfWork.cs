namespace Identity.Domain.Abtractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
