using App.Entities;
using App.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ReportApp.Logic.Services.Interfacies
{
    public interface IExternalDataService
    {
        public List<RoleDto> GetRoles();
        public List<CompetentionDto> GetCompitetions();
        public List<TeamDto> GetTeams();
        public List<ParticipantDto> GetParticipants();
        public RoleDto GetRole(int? id);
        public CompetentionDto GetCompitetion(int? id);
        public TeamDto GetTeam(int? id);
        public ParticipantDto GetParticipant(int? id);
        public void RemoveParticipant(ParticipantDto participant);
        public void RemoveTeam(TeamDto team);
        public void RemoveAction(ActionDto action);
        public void RemoveCompetetion(CompetentionDto competention);
        public void InsertOrUpdateCompetetion(CompetentionDto competention);
        public void InsertOrUpdateAction(ActionDto action);
        public void InsertOrUpdateTeam(TeamDto team);
        public void InsertOrUpdateParticipant(ParticipantDto participant);

    }
}
