using ReportApp.Common;
using ReportApp.Logic.Repositories;
using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportApp.Logic.Services.Interfacies;

namespace ReportApp.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public void Delete(int id)
        {
            uow.UserRepository.Delete(id);
            uow.Save();
            //uow.Dispose();
        }

        public ApplicationUser GetUserById(int id)
        {
            return uow.UserRepository.GetEntity(id);

        }

        public List<ApplicationUser> GetUsers()
        {
            return uow.UserRepository.GetEntities();
        }

        public void InsertOrUpdate(ApplicationUser user)
        {
        //    var pwdHash = PasswordCrypt.HashPassword(user.Password);
        //    var verify = PasswordCrypt.VerifyHashedPassword(pwdHash, user.Password);
            if (user.Id == 0)
                uow.UserRepository.Add(user);
            else
                uow.UserRepository.Update(user);
            uow.Save();
        }
        
    }
}
