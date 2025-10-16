using _BE.Models;
using _BE.Repositories.Interface;

namespace _BE.Repositories.Impl
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FunewsManagementContext context) : base(context)
        {
        }
    }
}
