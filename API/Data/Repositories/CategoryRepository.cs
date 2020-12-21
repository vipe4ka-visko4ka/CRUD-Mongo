using API.Models;

namespace API.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDatabaseContext context) : base(context.Categories)
        {
        }
    }

    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }
}
