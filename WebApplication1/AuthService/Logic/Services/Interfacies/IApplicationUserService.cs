using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Services.Interfacies
{
    public interface IApplicationUserService
    {
        void InsertOrUpdate(ApplicationUser user);
        void Delete(int id);
        Task<List<ApplicationUser>> GetUsers();
        ApplicationUser GetUserById(int id);
        Task<ApplicationUser> GetUserByLogin(string login);
    }
}
