using CustomerInquiry.Domain.DTOs;
using System;
using System.Collections.Generic;

namespace CustomerInquiry.Test
{
    public class MockData
    {
        public static MockData Current { get; } = new MockData();
        public List<CustomerDto> Customers { get; set; }
        public List<TransactionDto> Transactions { get; set; }

        public MockData()
        {
            Customers = new List<CustomerDto>()
            {
                new CustomerDto() { CustomerId = 1,
                    CustomerName = "John", ContactEmail = "John@domain.com", MobileNo = "1234567891" },
                new CustomerDto() { CustomerId = 2,
                    CustomerName = "Sara", ContactEmail = "Sara@domain.com", MobileNo = "7412589630" },
                new CustomerDto() { CustomerId = 3,
                    CustomerName = "Eric", ContactEmail = "Eric@domain.com", MobileNo = "3692581473" }
            };

            Transactions = new List<TransactionDto>
            {
                new TransactionDto { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 2, Amount = 10.52M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionId = 1 },
                new TransactionDto { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 1, Amount = 15.76M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionId = 2 },
                new TransactionDto { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 2, Amount = 37.12M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionId = 3 },
                new TransactionDto { TransactionDateTime = DateTime.Now.ToString(), CustomerId = 1, Amount = 5.25M, CurrencyCode = "USD", Status = Domain.Enums.StatusType.Success, TransactionId = 4 }
            };
        }
    }
}
