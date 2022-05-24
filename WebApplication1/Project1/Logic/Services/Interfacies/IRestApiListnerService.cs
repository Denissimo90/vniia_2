using App.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ReportApp.Logic.Services.Interfacies
{
    public interface IRestApiListnerService
    {
        bool UpsertDataFromApi(int competentionId);
        List<RoleDto> GetApiRoles();

        List<ParticipantDto> GetApiParticipants(int competentionId);
        List<TeamDto> GetApiTeams(int competentionId);
        List<CompetentionDto> GetApiCompetentions();
    }
}
