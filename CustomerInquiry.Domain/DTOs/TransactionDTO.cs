namespace CustomerInquiry.Domain.DTOs
{
    public class TransactionDto : TransactionBaseDto
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }

    }
}
