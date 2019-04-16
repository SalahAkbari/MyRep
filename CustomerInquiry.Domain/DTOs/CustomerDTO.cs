using System.Collections.Generic;

namespace CustomerInquiry.Domain.DTOs
{
    public class CustomerDto : CustomerBaseDto
    {
        public int CustomerId { get; set; }
        public ICollection<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
