using _BE.Models;
using _BE.Repositories.Interface;

namespace _BE.Repositories.Impl
{
    public class SystemAccountRepository : GenericRepository<SystemAccount>, ISystemAccountRepository
    {
        public SystemAccountRepository(FunewsManagementContext context) : base(context)
        {
        }
    }
}
