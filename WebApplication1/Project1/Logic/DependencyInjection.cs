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
            services.AddScoped<IManufactureRepository, ManufactureRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductQtyRepository, ProductQtyRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<ICompetentionDtoRepository, CompetentionDtoRepository>();
            services.AddScoped<IParticipantDtoRepository, ParticipantDtoRepository>();
            services.AddScoped<IRoleDtoRepository, RoleDtoRepository>();
            services.AddScoped<ITeamDtoRepository, TeamDtoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddDbContext<BookStoreDbContext>(opt => opt
            //    .UseSqlServer("Server=localhost,1433; Database=BooksDB;User Id=sa; Password=password_01;"));
            return services;
        }
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IJsonService, JsonService>();
            services.AddScoped<IManufactureService, ManufactureService>();
            services.AddScoped<IRestApiListnerService, RestApiListnerService>();
            return services;
        }
    }
}
