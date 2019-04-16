using CustomerInquiry.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CustomerInquiry.Provider;
using System.Linq;
using CustomerInquiry.DataAccess;

namespace CustomerInquiry.Test
{
    public class TransactionProvider : ITransactionProvider
    {
        readonly IGenericEfRepository<TransactionDto> _rep;

        public TransactionProvider(IGenericEfRepository<TransactionDto> rep)
        {
            _rep = rep;

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<TransactionDto, TransactionBaseDto>();
                config.CreateMap<TransactionBaseDto, TransactionDto>();
            });
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactions(int customerId)
        {
            try
            {
                var item = (await _rep.Get()).Where(b => b.CustomerId.Equals(customerId));
                var dtOs = Mapper.Map<IEnumerable<TransactionDto>>(item);
                return dtOs;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<TransactionDto> GetTransaction(int customerId, int id)
        {
            try
            {
                var item = await _rep.Get(id);
                if (item == null || !item.CustomerId.Equals(customerId)) return null;
                return item;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public TransactionDto AddTransaction(int customerId, TransactionBaseDto transaction)
        {
            try
            {
                var itemToCreate = Mapper.Map<TransactionDto>(transaction);
                itemToCreate.CustomerId = customerId;
                _rep.Add(itemToCreate);
                return !_rep.Save() ? null : itemToCreate;
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
