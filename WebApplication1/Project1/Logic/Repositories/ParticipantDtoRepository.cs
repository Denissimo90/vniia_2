using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using App.Entities.Dto;

namespace ReportApp.Logic.Repositories
{
    public class ParticipantDtoRepository : BaseRepository<ParticipantDto>, IParticipantDtoRepository
    {
        public ParticipantDtoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}