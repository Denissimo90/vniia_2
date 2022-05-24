using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("entities")]
    public class EntityController : ControllerBase
    {
        [HttpGet, Route("extensions")]
        public List<string> GetExtensions()
        {
            return new List<string>(); //_userService.GetUsers();
        }

        [HttpGet, Route("configs")]
        public List<string> GetConfigs()
        {
            return new List<string>(); //_userService.GetUsers();
        }
    }
}
