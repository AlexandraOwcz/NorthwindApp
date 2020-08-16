using Northwind.Data;
using Northwind.Data.Entities;

namespace Northwind.Api.Repository
{
    public interface ICategoryRepository : IBaseRepository<Categories>
    {
        
    }

    public class CategoryRepository : BaseRepository<Categories>, ICategoryRepository
    {
        public CategoryRepository(INorthwindMongoDbContext context) : base(context)
        {
        }
    }


}