﻿using Microsoft.EntityFrameworkCore;
using ReportApp.Common;
using ReportApp.Entities;
using ReportApp.Logic.Repositories.Interfacies;
using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Repositories
{
    public class ProductQtyRepository : BaseRepository<ProductQty>, IProductQtyRepository
    {
        public ProductQtyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
