using actiuon.DAL;
using actiuon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext dbContext;
        public CategoryController(AppDbContext db) => dbContext = db;
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Categories.ToListAsync());
        }
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category categorydelete = await dbContext.Categories.FindAsync(id);
            dbContext.Categories.Remove(categorydelete);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }
    }
}
