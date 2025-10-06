using Identity.Domain.Entities;

namespace Identity.Application.Contracts
{
    public interface IUserQueries
    {
        Task<IReadOnlyList<User>> SearchAsync(string q, CancellationToken ct = default);
    }
}
