using App.Entities;
using App.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ReportApp.Logic.Services.Interfacies
{
    public interface IExternalDataService
    {
        public List<RoleApiDto> GetRoles();
        public List<Competention> GetCompitetions();
        public List<Team> GetTeams();
        public List<ApplicationUser> GetParticipants();
        public RoleApiDto GetRole(int? id);
        public Competention GetCompitetion(int? id);
        public Team GetTeam(int? id);
        public ApplicationUser GetParticipant(int? id);
        public void RemoveParticipant(Participant participant);
        public void RemoveTeam(Team team);
        public void RemoveCompetetion(Competention competention);
        public void InsertOrUpdateCompetetion(Competention competention);
        public void InsertOrUpdateTeam(Team team);

    }
}
