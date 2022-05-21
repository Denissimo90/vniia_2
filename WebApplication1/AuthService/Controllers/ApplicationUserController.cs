﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthService.Entities;
using AuthService.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
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
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpGet, Route("all2")]
        public async Task<List<ApplicationUser>> GetUsers2()
        {
            return await _userService.GetUsers();
        }
    }
}
