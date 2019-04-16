using CustomerInquiry.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerInquiry.Provider
{
    public interface ITransactionProvider
    {

        Task<IEnumerable<TransactionDto>> GetAllTransactions(int customerId);
        Task<TransactionDto> GetTransaction(int customerId, int id);
        TransactionDto AddTransaction(int customerId, TransactionBaseDto transaction);
        Task<bool?> DeleteTransaction(int id);
    }
}
