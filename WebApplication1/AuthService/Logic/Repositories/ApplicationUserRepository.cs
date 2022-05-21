using Microsoft.EntityFrameworkCore;
using AuthService.Common;
using AuthService.Entities;
using AuthService.Logic.Repositories.Interfacies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<ApplicationUser>> GetAllApplicationUsersAsync()
        {
            return await GetEntities().ToListAsync();
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(int id)
        {
            return await FindByCondition(user => user.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> GetApplicationUserByLoginAsync(string login)
        {
            return await FindByCondition(user => user.Username.Equals(login)).FirstOrDefaultAsync();
        }
    }
}