using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Services.Interfacies
{
    public interface IApplicationUserService
    {
        void InsertOrUpdate(ApplicationUser user);
        void Delete(int id);
        List<ApplicationUser> GetUsers();
        ApplicationUser GetUserById(int id);
        Task<ApplicationUser> GetUserByLogin(string login);
    }
}
