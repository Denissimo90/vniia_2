using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportApp.Entities;
using ReportApp.Entities.Models;
using ReportApp.Logic.Services.Interfacies;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReportApp.Controllers
{
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

                    await Authenticate(registerUser.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(registerUser);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
