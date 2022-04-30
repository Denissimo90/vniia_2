﻿using Microsoft.EntityFrameworkCore;
using ReportApp.Common;
using ReportApp.Data;
using ReportApp.Logic.Repositories.Interfacies;
using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}