using App.Entities;
using App.Entities.Dto;
using ReportApp.Common;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportApp.Logic.Services
{
    public class ExternalDataService : IExternalDataService
    {
        private IUnitOfWork uow;

        public ExternalDataService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Competention GetCompitetion(int? id)
        {
            if (id == null)
                return new Competention()
                {
                    //ApplicationUsers = new List<ApplicationUser>()
                };
            else
            {
                return uow.CompetentionRepository.GetEntity((int)id);
            }
        }

        public List<Competention> GetCompitetions()
        {
            return uow.CompetentionRepository.GetEntities().ToList();
        }

        public ApplicationUser GetParticipant(int? id)
        {
            if (id == null)
                return new ApplicationUser()
                {
                    /*ParticipantDto = new ParticipantDto(),
                    RoleDto = new RoleDto(),
                    Team = new Team(),
                    Competention = new Competention()*/
                };
            else
            {
                return uow.UserRepository.GetEntity((int)id);
            }
        }

        public List<ApplicationUser> GetParticipants()
        {
            return uow.UserRepository.GetEntities().ToList();
        }

        public RoleApiDto GetRole(int? id)
        {
            if (id == null)
                return new RoleApiDto()
                {
                    //ApplicationUsers = new List<ApplicationUser>()
                };
            else
            {
                return uow.RoleDtoRepository.GetEntity((int)id);
            }
        }

        public List<RoleApiDto> GetRoles()
        {
            return uow.RoleDtoRepository.GetEntities().ToList();
        }

        public Team GetTeam(int? id)
        {
            if (id == null)
                return new Team()
                {
                    //TeamDto = new TeamDto(),
                    //ApplicationUsers = new List<ApplicationUser>()
                };
            else
            {
                return uow.TeamRepository.GetEntity((int)id);
            }
        }

        public List<Team> GetTeams()
        {
            return uow.TeamRepository.AllIncluding(t => t.Competention,
            t => t.TeamDto).ToList();
        }

        public void InsertOrUpdateTeamDto(TeamDto action)
        {
            action.Competention = null;
            
            var existingAction = uow.TeamDtoRepository.GetEntity(action.Id);
            if (existingAction != null)
            {
                uow.TeamDtoRepository.Update(action);
            }
            else
            {
                uow.TeamDtoRepository.Add(action);
            }
            uow.Save();
        }

        public void InsertOrUpdateCompetetion(Competention competention)
        {
            //competention.ApplicationUsers = null;

            var existingCompetention = uow.CompetentionRepository.GetEntity(competention.Id);
            if (existingCompetention != null)
            {
                uow.CompetentionRepository.Update(competention);
            }
            else
            {
                uow.CompetentionRepository.Add(competention);
            }
            uow.Save();
        }
               
        public void InsertOrUpdateTeam(Team team)
        {
            var existing = uow.TeamRepository.GetEntity(team.Id);
            if (existing != null)
            {
                uow.TeamRepository.Update(team);
            }
            else
            {
                uow.TeamRepository.Add(team);
            }
            uow.Save();
        }

        public void RemoveTeamDto(TeamDto action)
        {
            /* action.ParticipantDtos = null;
             action.TeamDtos = null;*/
            uow.TeamDtoRepository.Delete(action);
            uow.Save();
        }

        public void RemoveCompetetion(Competention competention)
        {
            /*foreach (var item in competention.ApplicationUsers)
            {
                uow.ApplicationUserRepository.Delete(item);
            }*/
            /*competention.ApplicationUsers = null;
            foreach (var item in competention.Teams)
            {
                uow.TeamRepository.Delete(item);
            }
            competention.Teams = null;*/
            uow.CompetentionRepository.Delete(competention);
            uow.Save();
        }

        public void RemoveParticipant(Participant participant)
        {

            uow.ParticipantRepository.Delete(participant);
        }

        public void RemoveTeam(Team team)
        {
            //team.ApplicationUsers = null;
            uow.TeamRepository.Delete(team);
            uow.Save();
        }
    }
}
