using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportApp.Entities;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportApp.Controllers
{
    [ApiController]
    [Route("users")]
    public class ApplicationUserController : ControllerBase
    {

        private readonly ILogger<ApplicationUserController> _logger;
        private readonly IUserService _userService;

        public ApplicationUserController(ILogger<ApplicationUserController> logger, IUserService userService)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось удалить пользователя.");
            }
        }

        [HttpGet, Route("all")]
        public List<ApplicationUser> GetUsers()
        {
            var list =  _userService.GetUsers();
            return _userService.GetUsers();
        }
    }
}
