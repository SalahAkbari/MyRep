using CustomerInquiry.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CustomerInquiry.Provider;
using System.Linq;

namespace CustomerInquiry.Test
{
    public class TransactionProvider : ITransactionProvider
    {
        ICustomerInquiryMockRepository _rep;

        public TransactionProvider(ICustomerInquiryMockRepository rep)
        {
            _rep = rep;

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<TransactionDTO, TransactionBaseDTO>();
                config.CreateMap<TransactionBaseDTO, TransactionDTO>();
            });
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactions(int customerId)
        {
            try
            {
                var item = (await _rep.GetTransactions(customerId)).Where(b => b.CustomerId.Equals(customerId));
                var DTOs = Mapper.Map<IEnumerable<TransactionDTO>>(item);
                return DTOs;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<TransactionDTO> GetTransaction(int customerId, int id)
        {
            try
            {
                var item = await _rep.GetTransaction(customerId,id);
                if (item == null || !item.CustomerId.Equals(customerId)) return null;
                return item;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public TransactionDTO AddTransaction(int customerId, TransactionBaseDTO transaction)
        {
            try
            {
                var itemToCreate = Mapper.Map<TransactionDTO>(transaction);
                itemToCreate.CustomerId = customerId;
                _rep.AddTransaction(itemToCreate);
                if (!_rep.Save()) return null;
                return itemToCreate;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public Task<bool?> DeleteTransaction(int id)
        {
            throw new NotImplementedException();
        }
    }
}
