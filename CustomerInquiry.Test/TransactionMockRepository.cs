using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerInquiry.DataAccess;
using CustomerInquiry.Domain.DTOs;

namespace CustomerInquiry.Test
{
    //As an alternative, we can also use one of the many mocking frameworks available for instance Moq.
    public class TransactionMockRepository<TEntity> : IGenericEfRepository<TEntity>
        where TEntity : TransactionDto
    {
        public async Task<IEnumerable<TEntity>> Get()
        {
            return (IEnumerable<TEntity>) await Task.FromResult(MockData.Current.Transactions.AsEnumerable());
        }

        public async Task<TEntity> Get(int id, bool includeRelatedEntities = false)
        {
            return await Task.FromResult(MockData.Current.Transactions.FirstOrDefault(b => b.TransactionId.Equals(id))) as TEntity;
        }

        public bool Save()
        {
            return true;
        }

        public void Add(TEntity item)
        {
            var id = MockData.Current.Transactions.Max(m => m.TransactionId) + 1;
            item.TransactionId = id;
            MockData.Current.Transactions.Add(item);
        }

        public bool Exists(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TEntity item)
        {
            throw new System.NotImplementedException();
        }
    }
}
