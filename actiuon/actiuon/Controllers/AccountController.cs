using actiuon.Models;
using actiuon.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment env;
        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _env)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            env = _env;
        }
        public async Task<IActionResult> Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Content("Not Logged In");
            }
            AppUser loggedUser = await userManager.FindByNameAsync(User.Identity.Name);
            return View(loggedUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (!ModelState.IsValid) return View();
            AppUser loggingUser = await userManager.FindByEmailAsync(lvm.Email);
            if (loggingUser == null) {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View(lvm);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(loggingUser, lvm.Password, true, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "You are locked out!");
                return View(lvm);
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong!");
                return View(lvm);
            }

            if ((await userManager.GetRolesAsync(loggingUser))[0] == "Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Home");
            
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task <IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid) return View();
            if(rvm.ProfImg != null)
            {
                if (!rvm.ProfImg.IsImage())
                {
                    ModelState.AddModelError("Image", "Fayl sekil olmalidir");

                    return View();
                }
                if (!rvm.ProfImg.IsValidSize(500))
                {
                    ModelState.AddModelError("Image", "Fayl maximum 500kb ola biler.");

                    return View();
                }
            }
            

            AppUser newUser = new AppUser
            {
                FirstName = rvm.FirstName, 
                Email = rvm.Email,
                LastName= rvm.LastName,
                UserName=rvm.UserName,
                PhoneNumber=rvm.PhoneNumber,
                IsActive = true,
            };
            if(rvm.ProfImg != null)
            {
                newUser.ProfilePhoto = await rvm.ProfImg.Upload(env.WebRootPath, @"img\");

                //burda imgden sonra olmlidir drop evvel olanda basdan goturur c diskinnen
            }

            IdentityResult identityResult = await userManager.CreateAsync (newUser, rvm.Password);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(rvm);
            }

            await signInManager.SignInAsync(newUser, true);
            await userManager.AddToRoleAsync(newUser, "Member");

            return RedirectToAction("Login", "Account");
        }
        //public async Task<IActionResult> InitRoles()
        //{
        //    await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await roleManager.CreateAsync(new IdentityRole("Member"));


        //    return Content("okay");
        //}
    }
}
