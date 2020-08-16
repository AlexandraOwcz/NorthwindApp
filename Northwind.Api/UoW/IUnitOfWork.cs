using System;
using System.Threading.Tasks;

namespace Northwind.Api.UoW
{
    public interface IUnitOfWork : IDisposable
    {
         Task<bool> Commit();
    }
}