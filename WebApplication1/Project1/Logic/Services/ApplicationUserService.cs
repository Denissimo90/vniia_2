using ReportApp.Common;
using ReportApp.Logic.Repositories;
using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportApp.Logic.Services.Interfacies;
using Microsoft.EntityFrameworkCore;

namespace ReportApp.Logic.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork uow;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public void Delete(int id)
        {
            uow.UserRepository.Delete(id);
            uow.SaveAsync();
        }

        public ApplicationUser GetUserById(int id)
        {
            return uow.UserRepository.GetEntity(id);

        }

        public async Task<ApplicationUser> GetUserByLogin(string login)
        {
            return await uow.UserRepository.GetApplicationUserByLoginAsync(login);
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await uow.UserRepository.GetEntities().ToListAsync();
        }

        public void InsertOrUpdate(ApplicationUser user)
        {
            //    var pwdHash = PasswordCrypt.HashPassword(user.Password);
            //    var verify = PasswordCrypt.VerifyHashedPassword(pwdHash, user.Password);
            if (user.Id == 0)
                uow.UserRepository.Add(user);
            else
                uow.UserRepository.Update(user);
            uow.SaveAsync();
        }

    }
}
