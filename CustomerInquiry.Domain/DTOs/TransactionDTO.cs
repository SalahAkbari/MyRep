using CustomerInquiry.Domain.Enums;
using System;

namespace CustomerInquiry.Domain.DTOs
{
    public class TransactionDTO : TransactionBaseDTO
    {
        public int TransactionID { get; set; }
        public int CustomerId { get; set; }

    }
}
