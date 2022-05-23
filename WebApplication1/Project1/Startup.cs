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
                Configuration.GetConnectionString("DefaultManufactureConnection");

            services.AddCors(options =>
            {
                // ����� �������� CORS, ����� ���� ���������� ���������� ����� ��������� ������ �� ������ API
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:5001")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddMvcCore();
            services.AddAuthorization(options =>
            // �������� ��������� �� �������� � Roles magic strings, ����������� ������������ ����� ����� �������
                options.AddPolicy("AdminsOnly", policyUser =>
                {
                    policyUser.RequireClaim("role", "admin");
                })
            );
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRepository();
            services.AddService();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "http://localhost:5006";
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "dotNetCore",
                    ValidAudience = "angular-client",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecret_secretkey!123")),
                    ClockSkew = TimeSpan.Zero
                };
            });

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

            app.UseAuthentication();
            app.UseMvc();
        }
    }
    public class AuthOptions
    {
        public const string ISSUER = "dotNetCore"; // �������� ������
        public const string AUDIENCE = "anonymous"; // ����������� ������
        const string KEY = "mysupersecret_secretkey!123";   // ���� ��� ��������
        public const int LIFETIME = 1; // ����� ����� ������ - 1 ������
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
