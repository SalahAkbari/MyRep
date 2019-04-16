using CustomerInquiry.DataAccess.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomerInquiry.DataAccess
{
    public class GenericEfRepository<TEntity> : IGenericEfRepository<TEntity>
        where TEntity : class
    {
        private readonly SqlDbContext _db;
        public GenericEfRepository(SqlDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await Task.FromResult(_db.Set<TEntity>());
        }

        public async Task<TEntity> Get(int id, bool includeRelatedEntities = false)
        {
            var entity = await Task.FromResult(_db.Set<TEntity>().Find(id));

            if (entity != null && includeRelatedEntities)
            {
                // Get the names of all DbSets in the DbContext
                var dbsets = typeof(SqlDbContext)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(z => z.PropertyType.Name.Contains("DbSet"))
                .Select(z => z.Name);
                // Get the names of all the properties (tables) in the generic
                // type T that is represented by a DbSet
                var tables = typeof(TEntity)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(z => dbsets.Contains(z.Name))
                .Select(z => z.Name);
                // Eager load all the tables referenced by the generic type T
                if (tables.Any())
                    foreach (var table in tables)
                        _db.Entry(entity).Collection(table).Load();
            }

            return entity;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public void Add(TEntity item) 
        {
            _db.Add(item);
        }

        public bool Exists(int id)
        {
            return _db.Set<TEntity>().Find(id) != null;
        }

        public void Delete(TEntity item) 
        {
            _db.Set<TEntity>().Remove(item);
        }
    }
}
