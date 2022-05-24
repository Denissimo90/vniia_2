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

        public CompetentionDto GetCompitetion(int? id)
        {
            if (id == null)
                return new CompetentionDto()
                {
                    ParticipantDtos = new List<ParticipantDto>(),
                    TeamDtos = new List<TeamDto>(),
                };
            else
            {
                return uow.CompetentionDtoRepository.GetEntity((int)id);
            }
        }

        public List<CompetentionDto> GetCompitetions()
        {
            return uow.CompetentionDtoRepository.GetEntities().ToList();
        }

        public ParticipantDto GetParticipant(int? id)
        {
            if (id == null)
                return new ParticipantDto()
                {
                    Action = new ActionDto(),
                    Competention = new CompetentionDto(),
                    Role = new RoleDto()
                };
            else
            {
                return uow.ParticipantDtoRepository.GetEntity((int)id);
            }
        }

        public List<ParticipantDto> GetParticipants()
        {
            return uow.ParticipantDtoRepository.GetEntities().ToList();
        }

        public RoleDto GetRole(int? id)
        {
            if (id == null)
                return new RoleDto()
                {
                    ParticipantDtos = new List<ParticipantDto>()
                };
            else
            {
                return uow.RoleDtoRepository.GetEntity((int)id);
            }
        }

        public List<RoleDto> GetRoles()
        {
            return uow.RoleDtoRepository.GetEntities().ToList();
        }

        public TeamDto GetTeam(int? id)
        {
            if (id == null)
                return new TeamDto()
                {
                    Competention = new CompetentionDto()
                };
            else
            {
                return uow.TeamDtoRepository.GetEntity((int)id);
            }
        }

        public List<TeamDto> GetTeams()
        {
            return uow.TeamDtoRepository.GetEntities().ToList();
        }

        public void InsertOrUpdateAction(ActionDto action)
        {
            action.ParticipantDtos = null;
            action.TeamDtos = null;

            var existingAction = uow.ActionDtoRepository.GetEntity(Convert.ToInt32(action.ActionId));
            if(existingAction != null)
            {
                uow.ActionDtoRepository.Update(action);
            }
            else
            {
                uow.ActionDtoRepository.Add(action);
            }
            uow.Save();
        }

        public void InsertOrUpdateCompetetion(CompetentionDto competention)
        {
            competention.ParticipantDtos = null;
            competention.TeamDtos = null;

            var existingCompetention = uow.CompetentionDtoRepository.GetEntity(competention.Id);
            if (existingCompetention != null)
            {
                uow.CompetentionDtoRepository.Update(competention);
            }
            else
            {
                uow.CompetentionDtoRepository.Add(competention);
            }
            uow.Save();
        }

        public void InsertOrUpdateParticipant(ParticipantDto participant)
        {
            var existing = uow.CompetentionDtoRepository.GetEntity(participant.Id);
            if (existing != null)
            {
                uow.ParticipantDtoRepository.Update(participant);
            }
            else
            {
                uow.ParticipantDtoRepository.Add(participant);
            }
            uow.Save();
        }

        public void InsertOrUpdateTeam(TeamDto team)
        {
            var existing = uow.TeamDtoRepository.GetEntity(team.Id);
            if (existing != null)
            {
                uow.TeamDtoRepository.Update(team);
            }
            else
            {
                uow.TeamDtoRepository.Add(team);
            }
            uow.Save();
        }

        public void RemoveAction(ActionDto action)
        {
            action.ParticipantDtos = null;
            action.TeamDtos = null;
            uow.ActionDtoRepository.Delete(action);
            uow.Save();
        }

        public void RemoveCompetetion(CompetentionDto competention)
        {
            competention.ParticipantDtos = null;
            competention.TeamDtos=null;
            uow.CompetentionDtoRepository.Delete(competention);
            uow.Save();
        }

        public void RemoveParticipant(ParticipantDto participant)
        {
            uow.ParticipantDtoRepository.Delete(participant);
        }

        public void RemoveTeam(TeamDto team)
        {
            team.Participants = null;
            uow.TeamDtoRepository.Delete(team);
            uow.Save();
        }
    }
}
