using CustomerInquiry.Domain.Enums;
using System;

namespace CustomerInquiry.Domain.DTOs
{
    public class TransactionDTO
    {
        public int TransactionID { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public StatusType Status { get; set; }
        public int CustomerId { get; set; }
    }
}
