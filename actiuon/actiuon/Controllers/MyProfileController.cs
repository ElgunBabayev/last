using actiuon.DAL;
using actiuon.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly IWebHostEnvironment env;
        public MyProfileController(AppDbContext _dbContext, UserManager<AppUser> _userManager, IWebHostEnvironment _env)
        {
            dbContext = _dbContext;
            userManager = _userManager;
            env = _env;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
