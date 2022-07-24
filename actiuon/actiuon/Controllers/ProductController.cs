using actiuon.DAL;
using actiuon.Models;
using actiuon.ViewModel;
using actiuon.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace actiuon.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext dbContext;

        private readonly UserManager<AppUser> userManager;
        private readonly IWebHostEnvironment env;

        public ProductController(AppDbContext db, UserManager<AppUser> _userManager, IWebHostEnvironment _env)
        {
            userManager = _userManager;
            env = _env;
            dbContext = db;
        }

        [Route("Products")]
        public IActionResult Index()
        {
            ProductCategoryViewModel pvm = new ProductCategoryViewModel
            {
                Categories = dbContext.Categories.ToList(),
                Products = dbContext.Products.Include("AppUser").Include("Category").Where(x => x.EndDate > DateTime.Now).Take(9).ToList()
            };
            return View(pvm);
        }

        [Route("Product/{Id}")]
        public IActionResult Get(int Id)
        {
            return View(dbContext.Products.FirstOrDefault(x => x.Id == Id));
        }

        [HttpPost]
        [Route("Product/Add")]

        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
         
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid) return View();

            if (product.ProdImg.IsImage())
            {
                if (!product.ProdImg.IsValidSize(500))
                {
                    ModelState.AddModelError("Image", "Fayl maksimum 500kb ola biler.");
                    return View();
                }

                product.Image = await product.ProdImg.Upload(env.WebRootPath, @"img\");
                product.AppUserId = (await userManager.FindByNameAsync(User.Identity.Name)).Id;
                product.StatusId = 1;
                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "MyDashboard");
            }
            return RedirectToAction("Index", "MyDashboard");

        }







    }
}
