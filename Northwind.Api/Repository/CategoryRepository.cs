using Northwind.Data;
using Northwind.Data.Entities;

namespace Northwind.Api.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        
    }

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(INorthwindMongoDbContext context) : base(context)
        {
        }
    }


}