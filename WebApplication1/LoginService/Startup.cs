using App.Entities;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace LoginService
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

            services.AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions()
                {
                    LogoutUrl = "/account/logout",
                    LoginUrl = "/account/login",
                    LoginReturnUrlParameter = "returnUrl"
                };
                options.IssuerUri = "https://localhost:5006";
            })
                .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
                .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
                .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
                //.AddTestUsers(InMemoryConfig.GetUsers())
                .AddInMemoryClients(InMemoryConfig.GetClients())
                .AddDeveloperSigningCredential()
                .AddCustomTokenRequestValidator<InMemoryConfig.CustomTokenRequestValidator>()
                    .AddProfileService<IdentityAuthority.Configs.IdentityProfileService>();
            //services.AddAuthentication("Bearer")
            //   .AddJwtBearer("Bearer", opt =>
            //   {
            //       opt.RequireHttpsMetadata = false;
            //       opt.Authority = "https://localhost:5005";
            //       opt.Audience = "companyApi";
            //   });
            //services.AddRazorPages();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            //.AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddCors(options =>
            {
                // задаём политику CORS, чтобы наше клиентское приложение могло отправить запрос на сервер API
                options.AddPolicy("default", policy =>
                {
                    policy
                    .WithOrigins("https://localhost:5006")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.Use((context, next) => { context.Request.Scheme = "https"; return next(); });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });

            IdentityModelEventSource.ShowPII = true;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
              new List<IdentityResource>
              {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile()
              };
        public static List<TestUser> GetUsers() =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                  Username = "Mick",
                  Password = "MickPassword",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Mick"),
                      new Claim("family_name", "Mining")
                  }
              },
              new TestUser
              {
                  SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                  Username = "Jane",
                  Password = "JanePassword",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Jane"),
                      new Claim("family_name", "Downing")
                  }
              }
          };
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
           new Client
           {
               ClientId = "U2EQlBHfcbuxUo",
               ClientSecrets = new [] { new Secret("TbXuRy7SSF5wzH".Sha256()) },
               ClientName = "WebUI",
               AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
               RequireConsent = false,
               RequireClientSecret = false,
               RequirePkce = false,
               RequireRequestObject = false,
               AllowOfflineAccess = true,
               AlwaysSendClientClaims = true,
               AlwaysIncludeUserClaimsInIdToken = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "companyApi",

                },
                           ClientUri = "https://localhost:5001",
                RedirectUris = new List<string>{ "https://localhost:5001/" },
                           PostLogoutRedirectUris = new List<string> { "https://localhost:5001/" },
            },
           new Client
{
    ClientName = "Angular-Client",
    ClientId = "angular-client",
    AllowedGrantTypes = GrantTypes.Implicit,
    RedirectUris = new List<string>{ "http://localhost:4200/", "http://localhost:4200/assets/silent-refresh.html" },
    RequirePkce = true,
    AllowAccessTokensViaBrowser = true,
                ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "companyApi"
    },
    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },
    AllowedCorsOrigins = { "http://localhost:4200" },
    RequireClientSecret = false,
    RequireConsent = false,
    AccessTokenLifetime = 600
}
        };
        public static IEnumerable<ApiScope> GetApiScopes() =>
    new List<ApiScope> { new ApiScope("companyApi", "CompanyEmployee API") };
        public static IEnumerable<ApiResource> GetApiResources() =>
    new List<ApiResource>
    {
        new ApiResource("companyApi", "CompanyEmployee API")
        {
            Scopes = { "companyApi" }
        }
    };

        public class CustomTokenRequestValidator : ICustomTokenRequestValidator
        {
            public Task ValidateAsync(CustomTokenRequestValidationContext context)
            {
                context.Result.CustomResponse =
                  new Dictionary<string, object> { { "preferred_username", "bob" },
                      {"name", "alice" },
                      {"employee_id", "" },
                      {"person_id", 1 },
                      {"department", "100" },
                      {"description", "" } };

                return Task.CompletedTask;
            }
        }
    }
}
