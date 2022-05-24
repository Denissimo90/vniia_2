using Microsoft.EntityFrameworkCore;
using ReportApp.Common;
using App.Entities;
using ReportApp.Logic.Repositories.Interfacies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Repositories
{
    public class ActionDtoRepository : BaseRepository<ActionDto>, IActionDtoRepository
    {
        public ActionDtoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
              
    }
}