using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Data
{
    // To implement the Repository Pattern, an abstract generics class will be used with CRUD operations.
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(Guid id);
    }
}