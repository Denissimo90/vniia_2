using ReportApp.Common;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Repositories.Interfacies
{
    public interface IApplicationUserRepository : IBaseRepository<ApplicationUser>
    {
        ApplicationUser GetApplicationUserByLogin(string login);
        ApplicationUser GetApplicationUserById(int id);
        IEnumerable<ApplicationUser> GetAllApplicationUsers();
    }
   
}
