using Order.Application.DTOs;
using Order.Domain.Enums;

namespace Order.Application.Contracts
{
    public interface ITransactionService
    {
        Task<int> CreateNewTransaction(CreateTransactionRequest request);
        Task<bool> UpdateTransactionStatus(int transactionId, TransactionStatus newStatus);
    }
}