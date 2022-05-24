using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using App.Entities.Dto;

namespace ReportApp.Logic.Repositories
{
    public class CompetentionRepository : BaseRepository<Competention>, ICompetentionRepository
    {
        public CompetentionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}