﻿using App.Entities;
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
        private readonly IExternalDataService _extDataService;

        public ApplicationUserController(ILogger<ApplicationUserController> logger, IApplicationUserService userService,
            IExternalDataService externalDataService)
        {
            _logger = logger;
            _userService = userService;
            _extDataService = externalDataService;
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
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось удалить пользователя.");
            }
        }


        public class UserPassword{
            public string Id { get; set; }
            public string Password { get; set; }
        }

        [HttpPost, Route("change-password")]
        public void ChangePassword([FromBody] UserPassword value)
        {
            try
            {
                ApplicationUser user = _userService.GetUserById(value.Id);
                user.PasswordHash = value.Password;
                user.IsNew = false;
                _userService.InsertOrUpdate(user);
            }
            catch (Exception ex)
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

        [HttpGet, Route("users-by-competention/{competentionId}")]
        public List<ApplicationUser> UsersByCompetentionId( int compId)
        {
            return _userService.GetUsersByCompetentionId(compId);
        }

        [HttpGet, Route("employments")]
        public List<ApplicationUser> GetEmployments()
        {
            return new List<ApplicationUser>(); //_userService.GetUsers();
        }

    }
}
