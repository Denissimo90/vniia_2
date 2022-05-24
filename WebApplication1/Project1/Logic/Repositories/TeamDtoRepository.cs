using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using App.Entities.Dto;

namespace ReportApp.Logic.Repositories
{
    public class TeamDtoRepository : BaseRepository<TeamDto>, ITeamDtoRepository
    {
        public TeamDtoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}