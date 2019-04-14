using CustomerInquiry.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.Test
{
    public interface ICustomerInquiryMockRepository
    {
        Task<IEnumerable<CustomerDTO>> GetCustomers();
        Task<CustomerDTO> GetCustomer(int customerId, bool includeTransactions = false);
        Task AddCustomer(CustomerDTO customer);
        bool CustomerExists(int customerId);
        bool Save();

        IEnumerable<TransactionDTO> GetTransactions(int customerId);
        TransactionDTO GetTransaction(int customerId, int transactionId);
        void AddTransaction(TransactionDTO transaction);
    }
}
