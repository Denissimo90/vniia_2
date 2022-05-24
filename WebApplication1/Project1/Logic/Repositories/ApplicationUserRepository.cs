using Microsoft.EntityFrameworkCore;
using ReportApp.Common;
using App.Entities;
using ReportApp.Logic.Repositories.Interfacies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<ApplicationUser> GetAllApplicationUsers()
        {
            return GetEntities().ToList();
        }

        public ApplicationUser GetApplicationUserById(int id)
        {
            return FindByCondition(user => user.Id.Equals(id)).FirstOrDefault();
        }
        public ApplicationUser GetApplicationUserByLogin(string login)
        {
            return FindByCondition(user => user.UserName.Equals(login)).FirstOrDefault();
        }
    }
}