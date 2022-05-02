using ReportApp.Common;
using ReportApp.Entities;
using ReportApp.Logic.Repositories.Interfacies;

namespace ReportApp.Logic.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}