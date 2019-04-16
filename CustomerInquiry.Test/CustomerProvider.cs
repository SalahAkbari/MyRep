using CustomerInquiry.DataAccess;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerInquiry.Provider;

namespace CustomerInquiry.Test
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly ICustomerInquiryMockRepository _rep;

        public CustomerProvider(ICustomerInquiryMockRepository rep)
        {
            _rep = rep;

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Customer, CustomerBaseDto>();
                config.CreateMap<CustomerBaseDto, Customer>();
                config.CreateMap<Customer, CustomerDto>();
                config.CreateMap<CustomerDto, Customer>();
                config.CreateMap<CustomerBaseDto, CustomerDto>();
                config.CreateMap<CustomerDto, CustomerBaseDto>();
            });
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            try
            {
                var item = await _rep.GetCustomers();
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
                var item = await _rep.GetCustomer(id, includeRelatedEntities);
                return item;
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
                var itemToCreate = Mapper.Map<CustomerDto>(customer);
                var id = MockData.Current.Customers.AsEnumerable().Max(m => m.CustomerId) + 1;
                itemToCreate.CustomerId = id;
                _rep.AddCustomer(itemToCreate);
                //if (!_rep.Save()) return null;
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
