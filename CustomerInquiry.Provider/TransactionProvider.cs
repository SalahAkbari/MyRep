using CustomerInquiry.DataAccess;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

namespace CustomerInquiry.Provider
{
    public class TransactionProvider : ITransactionProvider
    {
        readonly IGenericEfRepository<Transaction> _rep;

        public TransactionProvider(IGenericEfRepository<Transaction> rep)
        {
            _rep = rep;
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
                var dto = Mapper.Map<TransactionDto>(item);
                return dto;

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
                var itemToCreate = Mapper.Map<Transaction>(transaction);
                itemToCreate.CustomerId = customerId;
                _rep.Add(itemToCreate);
                if (!_rep.Save()) return null;
                var createdDto = Mapper.Map<TransactionDto>(itemToCreate);
                return createdDto;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<bool?> DeleteTransaction(int id)
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
