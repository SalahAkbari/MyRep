﻿using CustomerInquiry.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerInquiry.Domain.DTOs
{
    public class TransactionBaseDTO
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm}")]
        public string TransactionDateTime { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Amount { get; set; }
        [MaxLength(3)]
        public string CurrencyCode { get; set; }
        public StatusType Status { get; set; }
    }
}
