using CustomerInquiry.DataAccess;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace CustomerInquiry.Provider
{
    public class CustomerProvider : ICustomerProvider
    {
        IGenericEFRepository<Customer> _rep;

        public CustomerProvider(IGenericEFRepository<Customer> rep)
        {
            _rep = rep;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            try
            {
                var item = await _rep.Get();
                var DTOs = Mapper.Map<IEnumerable<CustomerDTO>>(item);
                return DTOs;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<CustomerDTO> GetCustomer(int id, bool includeRelatedEntities = false)
        {
            try
            {
                var item = await _rep.Get(id, includeRelatedEntities);
                if (item == null) return null;
                var DTO = Mapper.Map<CustomerDTO>(item);
                return DTO;

            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public CustomerDTO AddCustomer(CustomerBaseDTO customer)
        {
            try
            {
                var itemToCreate = Mapper.Map<Customer>(customer);
                _rep.Add(itemToCreate);
                if (!_rep.Save()) return null;
                var customerDTO = Mapper.Map<CustomerDTO>(itemToCreate);
                return customerDTO;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<bool?> DeleteCustomer(int id)
        {
            try
            {
                if (!_rep.Exists(id)) return false;
                var entityToDelete = await _rep.Get(id);
                _rep.Delete(entityToDelete);
                if (!_rep.Save()) return null;
                return true;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }

    }
}
