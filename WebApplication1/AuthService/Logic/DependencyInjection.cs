using Microsoft.Extensions.DependencyInjection;
using AuthService.Common;
using AuthService.Logic.Repositories;
using AuthService.Logic.Repositories.Interfacies;
using AuthService.Logic.Services;
using AuthService.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IManufactureRepository, ManufactureRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductQtyRepository, ProductQtyRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services.AddDbContext<BookStoreDbContext>(opt => opt
            //    .UseSqlServer("Server=localhost,1433; Database=BooksDB;User Id=sa; Password=password_01;"));
            return services;
        }
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IJsonService, JsonService>();
            services.AddTransient<IManufactureService, ManufactureService>();
            return services;
        }
    }
}
