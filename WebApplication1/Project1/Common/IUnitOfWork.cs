using ReportApp.Logic.Repositories.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IManufactureRepository ManufactureRepository { get; }
        IProductQtyRepository ProductQtyRepository { get; }
        IProductRepository ProductRepository{ get; }
        IApplicationUserRepository UserRepository { get; }
        ICompetentionDtoRepository CompetentionDtoRepository { get; }
        IParticipantDtoRepository ParticipantDtoRepository { get; }
        IRoleDtoRepository RoleDtoRepository { get; }
        ITeamDtoRepository TeamDtoRepository { get; }
        //Task Save();
        int Save();
    }
}
