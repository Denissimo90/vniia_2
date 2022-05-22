using AuthService.Common;
using AuthService.Entities;
using AuthService.Logic.Repositories;
using AuthService.Logic.Repositories.Interfacies;
using AuthService.Logic.Services;
using AuthService.Logic.Services.Interfacies;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            // ����������, ����� scopes ����� �������� IdentityServer
            return new List<IdentityResource>
            {
                // "sub" claim
                new IdentityResources.OpenId(),
                // ����������� claims � ������������ � profile scope
                // http://openid.net/specs/openid-connect-core-1_0.html#ScopeClaims
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            // claims ���� scopes ����� �������� � access_token
            return new List<ApiResource>
            {
                // ���������� scope "api1" ��� IdentityServer
                new ApiResource("api1", "API 1", 
                    // ��� claims ������ � scope api1
                    new[] {"name", "role" })
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            // claims ���� scopes ����� �������� � access_token
            return new List<ApiScope>
            {
                // ���������� scope "api1" ��� IdentityServer
                new ApiScope("api1", "API 1", 
                    // ��� claims ������ � scope api1
                    new[] {"name", "role" })
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    // ������������ ��������, ��� ������ client_id ������ ��������� ���������� ���������� 
                    ClientId = "anonymous",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowAccessTokensViaBrowser = true,
                    // �� ���� ��������� ������� ������ ������, 
                    // ��� false ����� �������� ����������� ���������� ����� UserInfo endpoint
                    AlwaysIncludeUserClaimsInIdToken = true,
                    // ����� ������ ������� �� ������� ���������� ���������� ����� ���������
                    // ������������� User Agent, ����� ��� ������������
                    RedirectUris = {
                        // ����� ��������������� ����� ������
                        "http://localhost:4200/",
                        // ����� ��������������� ��� �������������� ���������� access_token ����� iframe
                        "http://localhost:4200/assets/silent-refresh.html"
                    },
                    PostLogoutRedirectUris = { "http://localhost:4200/" },
                    // ����� ����������� ����������, ������ ������ ���������� ������ CORS-���������
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    // ������ scopes, ����������� ������ ��� ������� ����������� ����������
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AccessTokenLifetime = 3600, // ������, ��� �������� �� ���������
                    IdentityTokenLifetime = 300, // ������, ��� �������� �� ���������

                    // ��������� �� ��������� refresh-������� ����� �������� scope offline_access
                    AllowOfflineAccess = false,
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com"),
                        new Claim("role", "user"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com"),
                        new Claim("role", "admin"),
                    }
                }
            };
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));

            Logic.Services.EndpointService.ConnectionString =
                Configuration.GetConnectionString("DefaultManufactureConnection");

            services.AddMvc();
            services.AddIdentityServer(options =>
            {
                // http://docs.identityserver.io/en/release/reference/options.html#refoptions
                options.Endpoints = new EndpointsOptions
                {
                    // � Implicit Flow ������������ ��� ��������� �������
                    EnableAuthorizeEndpoint = true,
                    // ��� ��������� ������� ������
                    EnableCheckSessionEndpoint = true,
                    // ��� ������� �� ���������� ������������
                    EnableEndSessionEndpoint = true,
                    // ��� ��������� claims �������������������� ������������ 
                    // http://openid.net/specs/openid-connect-core-1_0.html#UserInfo
                    EnableUserInfoEndpoint = true,
                    // ������������ OpenId Connect ��� ��������� ����������
                    EnableDiscoveryEndpoint = true,

                    // ��� ��������� ���������� � �������, �� �� ����������
                    EnableIntrospectionEndpoint = false,
                    // ��� �� ����� �.�. � Implicit Flow access_token �������� ����� authorization_endpoint
                    EnableTokenEndpoint = false,
                    // �� �� ���������� refresh � reference tokens 
                    // http://docs.identityserver.io/en/release/topics/reference_tokens.html
                    EnableTokenRevocationEndpoint = false
                };

                // IdentitySever ���������� cookie ��� �������� ����� ������
                options.Authentication = new IdentityServer4.Configuration.AuthenticationOptions
                {
                    CookieLifetime = TimeSpan.FromDays(1)
                };

            })
                // �������� x509-����������, IdentityServer ���������� RS256 ��� ������� JWT
                .AddDeveloperSigningCredential()
                // ��� �������� � id_token
                .AddInMemoryIdentityResources(GetIdentityResources())
                // ��� �������� � access_token
                .AddInMemoryApiResources(GetApiResources())
                .AddInMemoryApiScopes(GetApiScopes())
                // ��������� ���������� ����������
                .AddInMemoryClients(GetClients())
                // �������� ������������
                .AddTestUsers(GetUsers());

            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IManufactureRepository, ManufactureRepository>();
            services.AddScoped<IProductQtyRepository, ProductQtyRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddCors(options =>
            {
                // ����� �������� CORS, ����� ���� ���������� ���������� ����� ��������� ������ �� ������ API
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            // ���������� middleware IdentityServer
            app.UseIdentityServer();
            app.UseCors(def => def.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // ��� 2 ������� �����, ����� ��������� �������������� �������� ������
            app.UseStaticFiles();
            app.UseRouting();
            // ���������� ����������� mvc � ����������� ��������
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "index.html");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "index.html");
            });
            //app.UseMvcWithDefaultRoute();
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
}