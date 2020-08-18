

using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Northwind.Data.Entities;
using Northwind.Data.Attributes;
using System;
using System.Linq;
using ServiceStack;

namespace Northwind.Data
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IEntity
    {
        protected readonly INorthwindMongoDbContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected BaseRepository(INorthwindMongoDbContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(GetCollectionName());
        }

        public virtual Task Add(TEntity obj)
        {
            return _context.AddCommand(async () => await DbSet.InsertOneAsync(obj));
        }

        public virtual async Task<TEntity> GetById(string id)
        {
            var data = await DbSet.FindAsync(x => x.Id.Equals(id));
            return data.FirstOrDefault();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            // await DbSet.Find(a => true).ToListAsync();
            var all = await DbSet.Find(_ => true).ToListAsync();
            return all;
        }
        // TODO: https://www.skylinetechnologies.com/Blog/Skyline-Blog/February_2018/how-to-use-dot-net-core-cli-create-multi-project

        public virtual Task Update(TEntity obj)
        {
            return _context.AddCommand(async () =>
            {
                await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj);
            });
        }

        public virtual Task Remove(string id) => _context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        private string GetCollectionName()
        {
            return (typeof(TEntity).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }
    }
}