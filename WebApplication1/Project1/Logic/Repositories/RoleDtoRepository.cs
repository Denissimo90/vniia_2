﻿using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using App.Entities.Dto;

namespace ReportApp.Logic.Repositories
{
    public class RoleDtoRepository : BaseRepository<RoleApiDto>, IRoleDtoRepository
    {
        public RoleDtoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}