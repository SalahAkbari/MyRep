using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerInquiry.Domain.DTOs
{
    public class CustomerBaseDto
    {
        [Required]
        [MaxLength(30)]
        public string CustomerName { get; set; }
        public string ContactEmail { get; set; }
        [Required(ErrorMessage = "You must provide a Mobile number")]
        [Display(Name = "Mobile No")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNo { get; set; }
    }
}
