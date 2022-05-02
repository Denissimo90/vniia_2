using Microsoft.EntityFrameworkCore;
using ReportApp.Common;
using ReportApp.Logic.Repositories.Interfacies;
using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Repositories
{
    public class ManufactureRepository : BaseRepository<Manufacture>, IManufactureRepository
    {
        public ManufactureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}