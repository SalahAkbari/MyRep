using CustomerInquiry.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerInquiry.Provider
{
    public interface ICustomerProvider
    {

        Task<IEnumerable<CustomerDTO>> GetAllCustomers();
        Task<CustomerDTO> GetCustomer(int id, bool includeRelatedEntities = false);
        CustomerDTO AddCustomer(CustomerBaseDTO customer);
        Task<bool?> DeleteCustomer(int id);
    }
}
