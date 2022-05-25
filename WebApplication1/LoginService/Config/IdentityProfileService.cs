using IdentityServer4.Services;
using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Extensions;
using System.Linq;
using App.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json;

namespace IdentityAuthority.Configs
{
    public class IdentityProfileService : IProfileService
    {

        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, UserManager<ApplicationUser> userManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                throw new ArgumentException("");
            }

            var principal = await _claimsFactory.CreateAsync(user);
            var claims = new List<Claim>();// principal.Claims.ToList();
            claims.Add(new Claim("preferred_username", user.UserName));
            claims.Add(new Claim("name", user.FirstName));
            claims.Add(new Claim("employee_id", user.Id));
            claims.Add(new Claim("person_id", user.PersonId + "", "Integer"));
            claims.Add(new Claim("department", JsonConvert.SerializeObject(user)));
            claims.Add(new Claim("description", "test"));
            claims.Add(new Claim("realm_access", "test"));
            claims.Add(new Claim("change_password", user.IsNew ? (true).ToString() : (false).ToString()));
            claims.Add(new Claim("roles", "admin,user"));

            //Add more claims like this
            //claims.Add(new System.Security.Claims.Claim("MyProfileID", user.Id));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }

}
