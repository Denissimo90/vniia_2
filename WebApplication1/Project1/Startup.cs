using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using App.Entities;
using ReportApp.Logic;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using System.Net.Http;
using System.Security.Authentication;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Linq;

namespace ReportApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("App.Entities")
                    ));

            Logic.Services.EndpointService.ConnectionString =
                Configuration.GetConnectionString("DefaultRestApiConnection");

            services.AddCors(options =>
            {
                // задаём политику CORS, чтобы наше клиентское приложение могло отправить запрос на сервер API
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration.GetValue<string>("AppServiceUrl:Host") + @"/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddMvcCore();
            /*services.AddAuthorization(options =>
            // политики позволяют не работать с Roles magic strings, содержащими перечисления ролей через запятую
                options.AddPolicy("AdminsOnly", policyUser =>
                {
                    policyUser.RequireClaim("role", "admin");
                })
            );*/
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRepository();
            services.AddService();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = Configuration.GetValue<string>("LoginServiceUrl:Host");
                options.RequireHttpsMetadata = true;
                options.ApiSecret = "Q&tGrEQMypEk.XxPU:%bWDZMdpZeJiyMwpLv4F7d**w9x:7KuJ#fy,E8KPHpKz++";
                options.ApiName = "companyApi";
                options.JwtBackChannelHandler = GetHandler();
            });

            services.AddAuthorization(options =>
            // политики позволяют не работать с Roles magic strings, содержащими перечисления ролей через запятую
                options.AddPolicy("AdminsOnly", policyUser =>
                {
                    policyUser.RequireClaim("role", "admin");
                })
            );

            IdentityModelEventSource.ShowPII = true;

            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
            app.UseCors(def => def.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();
            app.Use((context, next) => { context.Request.Scheme = "https"; return next(); });

            app.UseAuthorization();
            app.UseMvc();
        }
        private static HttpClientHandler GetHandler()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
            handler.SslProtocols = SslProtocols.Tls12;
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            return handler;
        }
    }
    public class AuthOptions
    {
        public const string ISSUER = "dotNetCore"; // издатель токена
        public const string AUDIENCE = "anonymous"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }

}
