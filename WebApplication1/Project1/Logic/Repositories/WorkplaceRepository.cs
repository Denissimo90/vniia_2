using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using App.Entities.Dto;

namespace ReportApp.Logic.Repositories
{
    public class WorkplaceRepository : BaseRepository<Workplace>, IWorkplaceRepository
    {
        public WorkplaceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}