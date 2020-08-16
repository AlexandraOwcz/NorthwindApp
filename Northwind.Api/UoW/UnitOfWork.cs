using System.Threading.Tasks;
using Northwind.Data.Entities;

namespace Northwind.Api.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly INorthwindMongoDbContext _context;

        public UnitOfWork(INorthwindMongoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
           _context.Dispose();
        }
    }
}