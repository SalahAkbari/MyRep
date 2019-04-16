using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerInquiry.Domain.DTOs;

namespace CustomerInquiry.Test
{
    //As an alternative, we can also use one of the many mocking frameworks available for instance Moq.
    public class CustomerInquiryMockRepository : ICustomerInquiryMockRepository
    {
        public bool Save()
        {
            return true;
        }
        
        public async Task<CustomerDto> GetCustomer(int customerId, bool includeTransactions = false)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            throw new System.NotImplementedException();
        }

        public bool CustomerExists(int customerId)
        {
            return MockData.Current.Customers.Count(p => p.CustomerId.Equals(customerId)).Equals(1);
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactions(int customerId)
        {
            return await Task.FromResult(MockData.Current.Transactions.Where(b => b.CustomerId.Equals(customerId)));
        }

        public async Task<TransactionDto> GetTransaction(int customerId, int transactionId)
        {
            return await Task.FromResult(MockData.Current.Transactions.FirstOrDefault(b => b.CustomerId.Equals(customerId) 
                     && b.TransactionId.Equals(transactionId)));
        }

        public void AddTransaction(TransactionDto transaction)
        {
            var id = MockData.Current.Transactions.Max(m => m.TransactionId) + 1;
            transaction.TransactionId = id;
            MockData.Current.Transactions.Add(transaction);
        }

        public Task AddCustomer(CustomerDto customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
