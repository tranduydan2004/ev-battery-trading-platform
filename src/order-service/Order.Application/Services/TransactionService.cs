using Order.Application.Contracts;
using Order.Application.DTOs;
using Order.Domain.Abstraction;
using Order.Domain.Entities;
using Order.Domain.Enums;

namespace Order.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        
        // Dependency Injection qua constructor
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<int> CreateNewTransaction(CreateTransactionRequest request)
        {
            // Sử dụng constructor nghiệp vụ để tạo đối tượng
            var transaction = new Transaction(
                request.ProductId,
                request.SellerId,
                request.BuyerId,
                request.FeeId,
                request.ProductType,
                request.BasePrice,
                request.CommissionFee,
                request.ServiceFee,
                request.BuyerAmount,
                request.SellerAmount,
                request.PlatformAmount
            );

            await _transactionRepository.AddAsync(transaction);
            return transaction.TransactionId;
        }

        public async Task<bool> UpdateTransactionStatus(int transactionId, TransactionStatus newStatus)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null) return false;

            transaction.UpdateStatus(newStatus);
            await _transactionRepository.UpdateAsync(transaction);
            return true;
        }
    }
}