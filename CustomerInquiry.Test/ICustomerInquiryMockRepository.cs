using CustomerInquiry.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.Test
{
    public interface ICustomerInquiryMockRepository
    {
        Task<IEnumerable<CustomerDto>> GetCustomers();
        Task<CustomerDto> GetCustomer(int customerId, bool includeTransactions = false);
        Task AddCustomer(CustomerDto customer);
        bool CustomerExists(int customerId);
        bool Save();

        Task<IEnumerable<TransactionDto>> GetTransactions(int customerId);
        Task<TransactionDto> GetTransaction(int customerId, int transactionId);
        void AddTransaction(TransactionDto transaction);
    }
}
