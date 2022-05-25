using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using App.Entities.Dto;

namespace ReportApp.Logic.Repositories
{
    public class GroupWorkplaceRepository : BaseRepository<GroupWorkplace>, IGroupWorkplaceRepository
    {
        public GroupWorkplaceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}