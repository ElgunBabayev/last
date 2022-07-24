using actiuon.DAL;
using actiuon.Models;
using actiuon.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext dbContext;

        public HomeController(AppDbContext db) => dbContext = db;

        public IActionResult Index()
        {
            HomeViewModel hvm = new HomeViewModel
            {
                Banner = dbContext.Banners.ToList(),
                Products = dbContext.Products.Include("AppUser").Where(x =>x.EndDate>DateTime.Now).Take(6).ToList()
            };
            return View(hvm);
        }

    }
}
