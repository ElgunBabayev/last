using actiuon.DAL;
using actiuon.Models;
using actiuon.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BannerController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IWebHostEnvironment env;
        public BannerController(AppDbContext db, IWebHostEnvironment _env)
        {
            dbContext = db;
            env = _env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Banners.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Banner banner)
        {
            if (!ModelState.IsValid) return View();
            //Banner duplicate = await dbContext.Banners.FirstOrDefaultAsync(x => x.Title == banner.Title);
            //if (duplicate != null)
            //{
            //    ModelState.AddModelError("Title", "Title unique olmalidir.");
            //    return View();
            //}

            if (!banner.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Fayl sekil olmalidir");

                return View();
            }
            if (!banner.Image.IsValidSize (500))
            {
                ModelState.AddModelError("Image", "Fayl maximum 500kb ola biler.");

                return View();
            }
            //if (!ModelState.IsValid)
            //{
            //    return View(banner);
            //}
            banner.Img = await banner.Image.Upload(env.WebRootPath, @"img");

            await dbContext.Banners.AddAsync(banner);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Banner");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            Banner bannerdelete = await dbContext.Banners.FindAsync(id);
            if (bannerdelete == null) return RedirectToAction("Index", "Banner");
            
       
            string filePath = Path.Combine(env.WebRootPath, @"img", bannerdelete.Img);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            dbContext.Banners.Remove(bannerdelete);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("Index", "Banner");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            Banner banner = await dbContext.Banners.FindAsync(id);
            if (banner == null) return RedirectToAction("Index", "Banner");

            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Banner banner)
        {
            if (!ModelState.IsValid) return View();
            if (banner.Image != null)
            {
                if (!banner.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Fayl sekil olmalidir");

                    return View();
                }
                if (!banner.Image.IsValidSize(500))
                {
                    ModelState.AddModelError("Image", "Fayl maximum 500kb ola biler.");

                    return View();
                }
                string filePath = Path.Combine(env.WebRootPath, @"img", banner.Img);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                banner.Img = await banner.Image.Upload(env.WebRootPath, @"img");
            }
            
                 dbContext.Banners.Update(banner);
                await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Banner");
        }
    }
}
