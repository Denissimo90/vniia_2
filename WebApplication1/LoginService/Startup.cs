using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            services.AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions()
                {
                    LogoutUrl = "/account/logout",
                    LoginUrl = "/account/login",
                    LoginReturnUrlParameter = "returnUrl"
                };
            })
                .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
                .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
                .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
                .AddTestUsers(InMemoryConfig.GetUsers())
                .AddInMemoryClients(InMemoryConfig.GetClients())
                .AddDeveloperSigningCredential();
            //services.AddAuthentication("Bearer")
            //   .AddJwtBearer("Bearer", opt =>
            //   {
            //       opt.RequireHttpsMetadata = false;
            //       opt.Authority = "https://localhost:5005";
            //       opt.Audience = "companyApi";
            //   });
            //services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddCors(options =>
            {
                // ����� �������� CORS, ����� ���� ���������� ���������� ����� ��������� ������ �� ������ API
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5006")
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
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
           /*new Client
           {
                ClientId = "company-employee",
                ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "companyApi" }
            },*/
           new Client
{
    ClientName = "Angular-Client",
    ClientId = "angular-client",
    AllowedGrantTypes = GrantTypes.Implicit,
    RedirectUris = new List<string>{ "http://localhost:4200/", "http://localhost:4200/assets/silent-refresh.html" },
    RequirePkce = true,
    AllowAccessTokensViaBrowser = true,
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
    }
}
