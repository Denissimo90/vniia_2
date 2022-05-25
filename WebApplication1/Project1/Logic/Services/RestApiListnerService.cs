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
            using (uow)
            {
                var existingRole = uow.RoleDtoRepository.GetEntities().ToList();

                foreach (var roleDto in rolesDto.Where(r => !existingRole.Any(e => e.Name == r.Name)))
                {
                    uow.RoleDtoRepository.InsertOrUpdate(roleDto, roleDto.Id);
                }
                var existingCompetention = uow.CompetentionDtoRepository.GetEntities().ToList();
                foreach (var item in competentionDto.Where(r => !existingCompetention.Any(e => e.Title == r.Title)))
                {
                    uow.CompetentionDtoRepository.InsertOrUpdate(item, item.Id);
                }
                var existingTeam = uow.TeamDtoRepository.GetEntities().ToList();
                foreach (var item in teamDto.Where(r => !existingTeam.Any(e => e.Name == r.Name && e.CompetentionId == r.CompetentionId)))
                {
                    uow.TeamDtoRepository.InsertOrUpdate(item, item.Id);
                }
                var existingPart = uow.ParticipantDtoRepository.GetEntities().ToList();
                foreach (var item in participantDto.Where(r => !existingPart.Any(e => e.Id == r.Id && e.FirstName == r.FirstName
                && e.SecondName == r.SecondName
                && e.ThirdName == r.ThirdName)))
                {
                    uow.ParticipantDtoRepository.InsertOrUpdate(item, item.Id);
                }
                uow.Save();
            }
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
