using ReportApp.Common;
using ReportApp.Logic.Repositories;
using App.Entities;
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
            uow.Save();
        }

        public ApplicationUser GetUserById(int id)
        {
            return uow.UserRepository.GetEntity(id);

        }

        public ApplicationUser GetUserById(string id)
        {
            return uow.UserRepository.FindByCondition(p => p.Id == id).FirstOrDefault();

        }

        public ApplicationUser GetUserByLogin(string login)
        {
            return uow.UserRepository.GetApplicationUserByLogin(login);
        }

        public List<ApplicationUser> GetUsers()
        {
            return uow.UserRepository.GetEntities().ToList();
        }

        public string InsertOrUpdate(ApplicationUser user)
        {
            //    var pwdHash = PasswordCrypt.HashPassword(user.Password);
            //    var verify = PasswordCrypt.VerifyHashedPassword(pwdHash, user.Password);
            var existingUser = uow.UserRepository.FindByCondition(u => u.Id == user.Id).FirstOrDefault();
            string str = string.Empty;
            if (existingUser == null)
            {
                var existPartisipantDto = uow.ParticipantDtoRepository.GetEntities()
                  .FirstOrDefault(d => (d.FirstName == user.FirstName
                  && d.SecondName == user.LastName
                  && d.ThirdName == user.MiddleName));
                if (existPartisipantDto == null) return "Пользователя с таким ФИО невозможно создать, так как он не является участником.";
                //user.CompetentionId = existPartisipantDto.CompetentionId;
                user.RoleDtoId = existPartisipantDto.RoleId;
                user.ParticipantDtoId = existPartisipantDto.Id;
                user.IsNew = true;


                uow.UserRepository.Add(user);
            }
            else
            {

                if (existingUser.PasswordHash != user.PasswordHash) { str = "Пароль изменен."; user.IsNew = false; }

                uow.UserRepository.Update(user);
            }
            return uow.Save() == 1 ? "Операция прошла успешно." + str : "Ошибка!";


        }

        public List<ApplicationUser> GetUsersByCompetentionId(int compId)
        {
            var users = uow.UserRepository./*AllIncluding(t => t.ParticipantDto,
                    t => t.RoleDto,
                    t => t.Team,
                    t => t.Competention)*/GetEntities().Where(u => u.CompetentionId == compId).ToList();
            return users;
        }


    }
}
