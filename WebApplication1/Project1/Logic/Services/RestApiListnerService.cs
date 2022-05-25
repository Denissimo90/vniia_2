using App.Entities;
using App.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using ReportApp.Common;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ReportApp.Logic.Services
{
    public class RestApiListnerService : IRestApiListnerService
    {
        private IUnitOfWork uow;

        public RestApiListnerService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public List<RoleApiDto> GetApiRoles()
        {
            List<RoleApiDto> roles;
            string values = EndpointService.GetRequestFromEndpoint("role", new List<string>().ToArray()).Result + "";

            if (!string.IsNullOrEmpty(values))
            {
                roles = JsonSerializer.Deserialize<List<RoleApiDto>>(values);
                return roles;
            }

            return new List<RoleApiDto>();
        }



        public bool UpsertDataFromApi(int competentionId)
        {
            var rolesDto = GetApiRoles();
            var competentionDto = GetApiCompetentions();
            var teamDto = GetApiTeams(competentionId);
            var participantDto = GetApiParticipants(competentionId);

            List<ParticipantDto> newPartis;
            List<CompetentionDto> newComp;
            List<TeamDto> newTeam;
            List<RoleApiDto> newRoles;


            List<ApplicationUser> newAppUser = new List<ApplicationUser>();
            List<Team> newTeams = new List<Team>();
            List<Competention> newCompetentions = new List<Competention>();

            using (uow)
            {
                var existingRole = uow.RoleDtoRepository.GetEntities().ToList();
                newRoles = rolesDto.Where(r => !existingRole.Any(e => e.Name == r.Name)).ToList();
                foreach (var roleDto in newRoles)
                {
                    uow.RoleDtoRepository.InsertOrUpdate(roleDto, roleDto.Id);
                }
                var existingCompetention = uow.CompetentionDtoRepository.GetEntities().ToList();
                newComp = competentionDto.Where(r => !existingCompetention.Any(e => e.Title == r.Title)).ToList();
                foreach (var item in newComp)
                {
                    var compe = new Competention { Title = item.Title, ShortTitle = item.ShortTitle, CompetentionDtoId = item.Id, GroupWorkplaceId = 0 };
                    newCompetentions.Add(compe);
                    uow.CompetentionRepository.Add(compe);
                    uow.CompetentionDtoRepository.InsertOrUpdate(item, item.Id);
                }
                var existingTeam = uow.TeamDtoRepository.GetEntities().ToList();
                newTeam = teamDto.Where(r => !existingTeam.Any(e => e.Name == r.Name && e.CompetentionId == r.CompetentionId)).ToList();
                foreach (var item in newTeam)
                {

                    var team = new Team { Name = item.Name, CompetentionId = item.CompetentionId, TeamDtoId = item.Id};
                    newTeams.Add(team);
                    uow.TeamRepository.Add(team);
                    uow.TeamDtoRepository.InsertOrUpdate(item, item.Id);
                }

                var existingPart = uow.ParticipantDtoRepository.GetEntities().ToList();
                newPartis = participantDto.Where(r => !existingPart.Any(e => e.Id == r.Id && e.FirstName == r.FirstName
                && e.SecondName == r.SecondName
                && e.ThirdName == r.ThirdName)).ToList();
                foreach (var item in newPartis)
                {
                    var newUser = new ApplicationUser
                    {
                        FirstName = item.FirstName,
                        LastName = item.SecondName,
                        MiddleName = item.ThirdName,
                        UserName = "user" + item.Id,
                        PasswordHash = "user" + item.Id,
                        CompetentionId = item.CompetentionId,
                        //Competention = uow.CompetentionRepository.GetEntity(item.CompetentionId),
                        RoleDtoId = item.RoleId,
                        //RoleDto = uow.RoleDtoRepository.GetEntity(item.RoleId),
                        TeamId = item.TeamId,
                        //Team = uow.TeamRepository.GetEntity(item.TeamId),
                        IsNew = true,
                    };
                    newAppUser.Add(newUser);
                    uow.UserRepository.Add(newUser);
                    uow.ParticipantDtoRepository.InsertOrUpdate(item, item.Id);
                }
                uow.Save();
            }
            
            /*using (uow)
            {
                if (newPartis.Count > 0)
                {
                    foreach (var item in newPartis)
                    {
                        var user = uow.UserRepository.GetAllApplicationUsers().FirstOrDefault(u => (u.FirstName == item.FirstName
                   && u.LastName == item.SecondName
                   && u.MiddleName == item.ThirdName)
                   && u.ParticipantDtoId == item.Id);

                        if (user == null)
                        {
                            newAppUser.Add(new ApplicationUser
                            {
                                FirstName = item.FirstName,
                                LastName = item.SecondName,
                                MiddleName = item.ThirdName,
                                UserName = "user" + item.Id,
                                PasswordHash = "user" + item.Id,
                                CompetentionId = item.CompetentionId,
                                Competention = uow.CompetentionRepository.GetEntity(item.CompetentionId),
                                RoleDtoId = item.RoleId,
                                RoleDto = uow.RoleDtoRepository.GetEntity(item.RoleId),
                                TeamId = item.TeamId,
                                Team = uow.TeamRepository.GetEntity(item.TeamId),
                            });
                        }
                        uow.ParticipantDtoRepository.InsertOrUpdate(item, item.Id);
                    }


                }
            }*/


            return true;
        }

        public List<CompetentionDto> GetApiCompetentions()
        {
            List<CompetentionDto> entities;
            string values = EndpointService.GetRequestFromEndpoint("competention", new List<string>().ToArray()).Result + "";

            if (!string.IsNullOrEmpty(values))
            {
                entities = JsonSerializer.Deserialize<List<CompetentionDto>>(values);
                return entities;
            }

            return new List<CompetentionDto>();
        }

        public List<TeamDto> GetApiTeams(int competentionId)
        {
            List<TeamDto> entities;
            string values = EndpointService.GetRequestFromEndpoint("competention", new List<string>() { $"{competentionId}", "team" }.ToArray()).Result + "";
            if (!string.IsNullOrEmpty(values))
            {
                entities = JsonSerializer.Deserialize<List<TeamDto>>(values);
                return entities;
            }

            return new List<TeamDto>();
        }

        public List<ParticipantDto> GetApiParticipants(int competentionId)
        {
            List<ParticipantDto> entities;
            string values = EndpointService.GetRequestFromEndpoint("competention", new List<string>() { $"{competentionId}", "participant" }.ToArray()).Result + "";

            if (!string.IsNullOrEmpty(values))
            {
                entities = JsonSerializer.Deserialize<List<ParticipantDto>>(values);
                return entities;
            }

            return new List<ParticipantDto>();
        }
    }
}
