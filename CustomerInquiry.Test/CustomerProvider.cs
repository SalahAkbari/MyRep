using CustomerInquiry.DataAccess;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CustomerInquiry.Provider;

namespace CustomerInquiry.Test
{
    public class CustomerProvider : ICustomerProvider
    {
        ICustomerInquiryMockRepository _rep;

        public CustomerProvider(ICustomerInquiryMockRepository rep)
        {
            _rep = rep;

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Customer, CustomerBaseDTO>();
                config.CreateMap<CustomerBaseDTO, Customer>();
                config.CreateMap<Customer, CustomerDTO>();
                config.CreateMap<CustomerDTO, Customer>();
                config.CreateMap<CustomerBaseDTO, CustomerDTO>();
                config.CreateMap<CustomerDTO, CustomerBaseDTO>();
            });
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            try
            {
                var item = await _rep.GetCustomers();
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
                var item = await _rep.GetCustomer(id, includeRelatedEntities);
                if (item == null) return null;
                return item;

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
                var itemToCreate = Mapper.Map<CustomerDTO>(customer);
                _rep.AddCustomer(itemToCreate);
                if (!_rep.Save()) return null;
                return itemToCreate;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<bool?> DeleteCustomer(int id)
        {
            throw new NotImplementedException();

            //try
            //{
            //    if (!_rep.Exists(id)) return false;
            //    var entityToDelete = await _rep.Get(id);
            //    _rep.Delete(entityToDelete);
            //    if (!_rep.Save()) return null;
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    //Logger.ErrorException(e.Message, e);
            //    throw;
            //}
        }

    }
}
