using System.Collections.Generic;

namespace CustomerInquiry.Domain.DTOs
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactEmail { get; set; }
        public string MobileNo { get; set; }
        public ICollection<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
    }
}
