using Microsoft.EntityFrameworkCore;
using AuthService.Common;
using AuthService.Logic.Repositories.Interfacies;
using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Repositories
{
    public class ManufactureRepository : BaseRepository<Manufacture>, IManufactureRepository
    {
        public ManufactureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}