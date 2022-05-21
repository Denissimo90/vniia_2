using Microsoft.EntityFrameworkCore;
using AuthService.Common;
using AuthService.Entities;
using AuthService.Logic.Repositories.Interfacies;
using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
