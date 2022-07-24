using actiuon.DAL;
using actiuon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext dbContext;
        public UserController(UserManager<AppUser> _userManager, AppDbContext _dbContext)
        {
            userManager = _userManager;
            dbContext = _dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await userManager.Users.ToListAsync();
            List<User> dto = new List<User>();
            foreach (AppUser item in users)
            {
                User user = new User
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNum=item.PhoneNumber,
                    ProfilePhoto = item.ProfilePhoto,
                    IsActive = item.IsActive,
                    Role = (await userManager.GetRolesAsync(item))[0]
                };
                dto.Add(user);
            }

            return View(dto);
        }
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Home");
            AppUser user = await userManager.FindByIdAsync(id);
            User _user = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNum = user.PhoneNumber,
                ProfilePhoto = user.ProfilePhoto,
                IsActive = user.IsActive,
                Role = (await userManager.GetRolesAsync(user))[0]
            };
            return View(_user);
        }

        public async Task<IActionResult> ChangeStatusConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "User");
            AppUser user = await userManager.FindByIdAsync(id);
            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "User");
            AppUser user = await userManager.FindByIdAsync(id);
            User _user = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNum = user.PhoneNumber,
                ProfilePhoto = user.ProfilePhoto,
                IsActive = user.IsActive,
                Role = (await userManager.GetRolesAsync(user))[0]
            };
            return View(_user);
        }
        public async Task<IActionResult> ChangePasswordConfirmed(string id, string password, string repeatPassword)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "User");
            if (string.IsNullOrEmpty(password)) return RedirectToAction("Index", "User");
            if (string.IsNullOrEmpty(repeatPassword)) return RedirectToAction("Index", "User");
            if (password != repeatPassword) return RedirectToAction("Index", "User");
            AppUser user = await userManager.FindByIdAsync(id);

            string token = await userManager.GeneratePasswordResetTokenAsync(user);

           await userManager.ResetPasswordAsync(user,token,password);
            return RedirectToAction("Index", "User"); 
        }
    }
}
