using CustomerInquiry.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerInquiry.Provider
{
    public interface ITransactionProvider
    {

        Task<IEnumerable<TransactionDTO>> GetAllTransactions(int customerId);
        Task<TransactionDTO> GetTransaction(int customerId, int id);
        TransactionDTO AddTransaction(int customerId, TransactionBaseDTO transaction);
        Task<bool?> DeleteTransaction(int id);
    }
}
