using AuthService.Common;
using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Repositories.Interfacies
{
    public interface IApplicationUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetApplicationUserByLoginAsync(string login);
        Task<ApplicationUser> GetApplicationUserByIdAsync(int id);
        Task<IEnumerable<ApplicationUser>> GetAllApplicationUsersAsync();
    }
   
}
