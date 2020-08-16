using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Northwind.Data.Entities;
using ServiceStack;

namespace Northwind.Data
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly INorthwindMongoDbContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected BaseRepository(INorthwindMongoDbContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual Task Add(TEntity obj)
        {
            return _context.AddCommand(async () => await DbSet.InsertOneAsync(obj));
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.FirstOrDefault();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            // await DbSet.Find(a => true).ToListAsync();
            var all = (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty)).ToList();
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

        public virtual Task Remove(Guid id) => _context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}