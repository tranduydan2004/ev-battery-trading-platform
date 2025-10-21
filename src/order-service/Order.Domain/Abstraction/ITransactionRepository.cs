namespace Order.Domain.Abstraction
{
    public interface ITransactionRepository
    {
        Task<Order.Domain.Entities.Transaction> GetByIdAsync(int transactionId);
        Task AddAsync(Order.Domain.Entities.Transaction transaction);
        Task UpdateAsync(Order.Domain.Entities.Transaction transaction);
    }
}