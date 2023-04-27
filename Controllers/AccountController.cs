using Microsoft.AspNetCore.Identity;
using PBugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using PBugTracker.Data;

namespace PBugTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager, 
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false).Result;
                if(result.Succeeded)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    ViewBag.Error = "Invalid login attempt";
                    return View();
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
           return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstNameLastName = model.Name
                };

               var result = _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    //var res = _userManager.AddToRoleAsync(user, model.Role).Result;
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = "Invalid login attempt";
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Marketing"));
            await _roleManager.CreateAsync(new IdentityRole("Finance"));
            await _roleManager.CreateAsync(new IdentityRole("HR"));
            return RedirectToAction("Index");
        }
    }
}
