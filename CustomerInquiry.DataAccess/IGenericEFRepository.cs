using System.Collections.Generic;

namespace CustomerInquiry.DataAccess
{
    public interface IGenericEFRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id, bool includeRelatedEntities = false);
        bool Save();
        void Add(TEntity item);
        bool Exists(int id);
        void Delete(TEntity item);
    }
}
