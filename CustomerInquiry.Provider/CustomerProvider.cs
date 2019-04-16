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
        readonly IGenericEfRepository<Customer> _rep;

        public CustomerProvider(IGenericEfRepository<Customer> rep)
        {
            _rep = rep;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            try
            {
                var item = await _rep.Get();
                var dtOs = Mapper.Map<IEnumerable<CustomerDto>>(item);
                return dtOs;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<CustomerDto> GetCustomer(int id, bool includeRelatedEntities = false)
        {
            try
            {
                var item = await _rep.Get(id, includeRelatedEntities);
                if (item == null) return null;
                var dto = Mapper.Map<CustomerDto>(item);
                return dto;

            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public CustomerDto AddCustomer(CustomerBaseDto customer)
        {
            try
            {
                var itemToCreate = Mapper.Map<Customer>(customer);
                _rep.Add(itemToCreate);
                if (!_rep.Save()) return null;
                var customerDto = Mapper.Map<CustomerDto>(itemToCreate);
                return customerDto;
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
