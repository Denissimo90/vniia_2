using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportApp.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ReportApp.Logic.Services.Interfacies;
using ReportApp.Entities.Models;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;

namespace ReportApp.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : Controller
    {
        private ILogger<AccountController> _logger;
        private IApplicationUserService _service;

        //private ApplicationDbContext _dbContext;

        public AccountController(ILogger<AccountController> logger, IApplicationUserService userService)
        {
            _logger = logger;
            _service = userService;
            //_dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _service.GetUserByLogin(loginUser.Login);
                if (user is null) return NotFound();

                return Token(user.Username);
            }
            return View(loginUser);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser existedUser = await _service.GetUserByLogin(registerUser.Email);
                if (existedUser == null)
                {
                    // добавляем пользователя в бд
                    _service.InsertOrUpdate(new ApplicationUser
                    {
                        FirstName = registerUser.FirstName,
                        Email = registerUser.Email,
                        Password = registerUser.Password,
                        LastName = registerUser.LastName,
                        MiddleName = registerUser.MiddleName,
                        Username = registerUser.Username
                    });

                    return Token(registerUser.Username); // аутентификация
}
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(registerUser);
        }
        [HttpPost("/token/{username}")]
        public IActionResult Token(string username)
        {
            var identity = GetIdentityClaims(username);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }
        
        private ClaimsIdentity GetIdentityClaims(string userName)
        {
            ApplicationUser user = _service.GetUserByLogin(userName).Result;
            if (user is null) return null;
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, userName)//,
                //TODO: Когда добавлю роли, раскомментить
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token"/*, ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType*/);
            return claimsIdentity;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
