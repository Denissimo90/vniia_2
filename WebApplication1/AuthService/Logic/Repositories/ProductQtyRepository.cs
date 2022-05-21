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
    public class ProductQtyRepository : BaseRepository<ProductQty>, IProductQtyRepository
    {
        public ProductQtyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
