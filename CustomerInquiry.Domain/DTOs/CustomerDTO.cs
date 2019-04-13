using System.Collections.Generic;

namespace CustomerInquiry.Domain.DTOs
{
    public class CustomerDTO : CustomerBaseDTO
    {
        public int CustomerID { get; set; }
        public ICollection<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
    }
}
