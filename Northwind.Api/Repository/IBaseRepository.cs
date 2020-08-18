using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Data.Entities;

namespace Northwind.Data
{
    // To implement the Repository Pattern, an abstract generics class will be used with CRUD operations.
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(string id);
    }
}