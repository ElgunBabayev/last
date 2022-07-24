using actiuon.DAL;
using actiuon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Controllers
{
    public class MyDashboardController : Controller
    {
        private readonly AppDbContext dbContext;
        readonly UserManager<AppUser> _userManager;
        public MyDashboardController(AppDbContext db, UserManager<AppUser> userManager)
        {
            dbContext = db;
            _userManager = userManager;
        }
        public async Task <IActionResult> Index()
        {

            MyDashboardViewModel mvm = new MyDashboardViewModel();
            mvm.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            string UserId = dbContext.Users.First(x => x.UserName == User.Identity.Name).Id;
            mvm.Products = dbContext.Products.Where(x => x.AppUserId == UserId).Include("Category").Include("Status").ToList();
            mvm.Categories = dbContext.Categories.ToList();
            mvm.Status = dbContext.Statuses.ToList();
            return View(mvm);
            
        }


    }
}
