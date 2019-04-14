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
        public async Task AddCustomer(CustomerDTO Customer)
        {
            var id = (await GetCustomers()).Max(m => m.CustomerID) + 1;
            Customer.CustomerID = id;
            MockData.Current.Customers.Add(Customer);
        }

        public async Task<CustomerDTO> GetCustomer(int CustomerId, bool includeTransactions = false)
        {
            var Customer = await Task.FromResult(MockData.Current.Customers.FirstOrDefault(p => p.CustomerID.Equals(CustomerId)));

            if (includeTransactions && Customer != null)
            {
                Customer.Transactions = MockData.Current.Transactions.Where(b => b.CustomerId.Equals(CustomerId)).ToList();
            }

            return Customer;
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomers()
        {
            return await Task.FromResult(MockData.Current.Customers);
        }

        public bool CustomerExists(int CustomerId)
        {
            return MockData.Current.Customers.Count(p => p.CustomerID.Equals(CustomerId)).Equals(1);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactions(int customerId)
        {
            return await Task.FromResult(MockData.Current.Transactions.Where(b => b.CustomerId.Equals(customerId)));
        }

        public async Task<TransactionDTO> GetTransaction(int customerId, int transactionId)
        {
            return await Task.FromResult(MockData.Current.Transactions.FirstOrDefault(b => b.CustomerId.Equals(customerId) 
                     && b.TransactionID.Equals(transactionId)));
        }

        public void AddTransaction(TransactionDTO transaction)
        {
            var id = MockData.Current.Transactions.Max(m => m.TransactionID) + 1;
            transaction.TransactionID = id;
            MockData.Current.Transactions.Add(transaction);
        }
    }
}
