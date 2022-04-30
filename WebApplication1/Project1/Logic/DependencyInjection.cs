using Microsoft.Extensions.DependencyInjection;
using ReportApp.Common;
using ReportApp.Logic.Repositories;
using ReportApp.Logic.Repositories.Interfacies;
using ReportApp.Logic.Services;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IManufactureRepository, ManufactureRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductQtyRepository, ProductQtyRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services.AddDbContext<BookStoreDbContext>(opt => opt
            //    .UseSqlServer("Server=localhost,1433; Database=BooksDB;User Id=sa; Password=password_01;"));
            return services;
        }
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IJsonService, JsonService>();
            services.AddTransient<IManufactureService, ManufactureService>();
            return services;
        }
    }
}
