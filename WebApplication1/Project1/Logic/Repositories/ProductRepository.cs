using Microsoft.EntityFrameworkCore;
using ReportApp.Common;
using App.Entities;
using ReportApp.Logic.Repositories.Interfacies;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
