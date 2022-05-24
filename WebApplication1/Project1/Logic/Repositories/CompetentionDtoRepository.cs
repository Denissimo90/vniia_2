using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using App.Entities.Dto;

namespace ReportApp.Logic.Repositories
{
    public class CompetentionDtoRepository : BaseRepository<CompetentionDto>, ICompetentionDtoRepository
    {
        public CompetentionDtoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}