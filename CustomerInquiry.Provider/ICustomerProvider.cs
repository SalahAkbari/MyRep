using CustomerInquiry.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerInquiry.Provider
{
    public interface ICustomerProvider
    {

        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        Task<CustomerDto> GetCustomer(int id, bool includeRelatedEntities = false);
        CustomerDto AddCustomer(CustomerBaseDto customer);
        Task<bool?> DeleteCustomer(int id);
    }
}
