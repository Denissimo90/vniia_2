using App.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;

namespace ReportApp.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("user")]
    public class ApplicationUserController : ControllerBase
    {

        private readonly ILogger<ApplicationUserController> _logger;
        private readonly IApplicationUserService _userService;

        public ApplicationUserController(ILogger<ApplicationUserController> logger, IApplicationUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ApplicationUser GetUser([FromRoute] int id)
        {
            return _userService.GetUserById(id);
        }

        [HttpPost, Route("insert-or-update")]
        public void InsertOrUpdate([FromBody] ApplicationUser user)
        {
            try
            {
                _userService.InsertOrUpdate(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Не удалось удалить пользователя.");
            }
        }

        [HttpDelete("{id}")]
        public void DeleteUser([FromRoute] int id)
        {
            try
            {
                _userService.Delete(id);
                Console.WriteLine("User deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось удалить пользователя.");
            }
            
        }

        [HttpGet, Route("all")]
        public List<ApplicationUser> GetUsers()
        {
            return  _userService.GetUsers();
        }

        [HttpGet, Route("all2")]
        public List<ApplicationUser> GetUsers2()
        {
            return _userService.GetUsers();
        }

        [HttpGet, Route("employments")]
        public List<ApplicationUser> GetEmployments()
        {
            return new List<ApplicationUser>(); //_userService.GetUsers();
        }

    }
}
