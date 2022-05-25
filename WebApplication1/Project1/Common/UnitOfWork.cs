﻿using App.Entities;
using ReportApp.Logic.Repositories.Interfacies;
using System;
using System.Threading.Tasks;

namespace ReportApp.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IManufactureRepository ManufactureRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IApplicationUserRepository UserRepository { get; }

        public IProductQtyRepository ProductQtyRepository { get; }

        public ICompetentionRepository CompetentionRepository { get; }

        public IParticipantRepository ParticipantRepository { get; }

        public IRoleDtoRepository RoleDtoRepository { get; }

        public ITeamRepository TeamRepository { get; }
        public ITeamDtoRepository TeamDtoRepository { get; }

        public ICompetentionDtoRepository CompetentionDtoRepository { get; }

        public IParticipantDtoRepository ParticipantDtoRepository { get; }

        public IGroupWorkplaceRepository GroupWorkplaceRepository { get; }

        public IWorkplaceRepository WorkplaceRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext,
            IManufactureRepository manufactureRepository,
            IProductRepository productRepository, IApplicationUserRepository userRepository,
            IProductQtyRepository productQtyRepository , ICompetentionRepository competentionRepository,
            IParticipantRepository participantRepository,
            IRoleDtoRepository roleDtoRepository, ITeamRepository teamRepository, ITeamDtoRepository actionDtoRepository,
            ICompetentionDtoRepository competentionDtoRepository, IParticipantDtoRepository participantDtoRepository,
            IWorkplaceRepository workplaceRepository, IGroupWorkplaceRepository groupWorkplaceRepository)
        {
            this._context = dbContext;
            ProductQtyRepository = productQtyRepository;
            ManufactureRepository = manufactureRepository;
            ProductRepository = productRepository;
            UserRepository = userRepository;
            RoleDtoRepository = roleDtoRepository;

            TeamRepository = teamRepository;
            ParticipantRepository = participantRepository;
            CompetentionRepository = competentionRepository;
            GroupWorkplaceRepository = groupWorkplaceRepository;
            WorkplaceRepository = workplaceRepository;
            ParticipantDtoRepository = participantDtoRepository;
            CompetentionDtoRepository = competentionDtoRepository;
            TeamDtoRepository = actionDtoRepository;
        }
        /* public int Save()
         {
             return _context.SaveChanges();
         }*/
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        /*public async Task Save()
        {
           await _context.SaveChangesAsync();
        }*/

        //private readonly ApplicationDbContext context;
        //private bool disposed;
        //private Dictionary<string, object> repositories;

        //public UnitOfWork(ApplicationDbContext context)
        //{
        //    this.context = context;
        //}

        //public UnitOfWork()
        //{
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        public int Save()
        {
           return _context.SaveChanges();
        }

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            context.Dispose();
        //        }
        //    }
        //    disposed = true;
        //}

        //public BaseRepository<T> Repository<T>() where T : class
        //{
        //    if (repositories == null)
        //    {
        //        repositories = new Dictionary<string, object>();
        //    }

        //    var type = typeof(T).Name;

        //    if (!repositories.ContainsKey(type))
        //    {
        //        var repositoryType = typeof(BaseRepository<>);
        //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
        //        repositories.Add(type, repositoryInstance);
        //    }
        //    return (BaseRepository<T>)repositories[type];
        //}
    }
}
