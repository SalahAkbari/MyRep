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
        IGenericEFRepository<Transaction> _rep;

        public TransactionProvider(IGenericEFRepository<Transaction> rep)
        {
            _rep = rep;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactions(int customerId)
        {
            try
            {
                var item = (await _rep.Get()).Where(b => b.CustomerId.Equals(customerId));   
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
                var item = await _rep.Get(id);
                if (item == null || !item.CustomerId.Equals(customerId)) return null;
                var DTO = Mapper.Map<TransactionDTO>(item);
                return DTO;

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
                var itemToCreate = Mapper.Map<Transaction>(transaction);
                itemToCreate.CustomerId = customerId;
                _rep.Add(itemToCreate);
                if (!_rep.Save()) return null;
                var createdDTO = Mapper.Map<TransactionDTO>(itemToCreate);
                return createdDTO;
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
