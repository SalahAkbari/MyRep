using CustomerInquiry.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.Test
{
    public class MockData
    {
        public static MockData Current { get; } = new MockData();
        public List<CustomerDTO> Customers { get; set; }
        public List<TransactionDTO> Transactions { get; set; }

        public MockData()
        {
            Customers = new List<CustomerDTO>()
            {
                new CustomerDTO() { CustomerID = 1,
                    CustomerName = "John", ContactEmail = "John@domain.com", MobileNo = "1234567891" },
                new CustomerDTO() { CustomerID = 2,
                    CustomerName = "Sara", ContactEmail = "Sara@domain.com", MobileNo = "7412589630" },
                new CustomerDTO() { CustomerID = 3,
                    CustomerName = "Eric", ContactEmail = "Eric@domain.com", MobileNo = "3692581473" }
            };

            Transactions = new List<TransactionDTO>
            {
                new TransactionDTO { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 2, Amount = 10.52M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionID = 1 },
                new TransactionDTO { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 1, Amount = 15.76M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionID = 2 },
                new TransactionDTO { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 2, Amount = 37.12M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionID = 3 },
                new TransactionDTO { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 1, Amount = 5.25M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionID = 4 }
            };
        }
    }
}
