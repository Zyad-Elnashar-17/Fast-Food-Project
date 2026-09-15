using FastFood.Web.Data;
using FastFood.Web.Models;
using FastFood.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public SubCategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var SubCategories = _context.SubCategories.Include(x => x.Category).ToList();
            return View(SubCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SubCategoryViewModel vm = new SubCategoryViewModel();
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Title");
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(SubCategoryViewModel vm)
        {
            SubCategory model = new SubCategory();
            if (ModelState.IsValid)
            {

            model.Title = vm.Title;
            model.CategoryId = vm.CategoryId;
            _context.SubCategories.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SubCategoryViewModel vm = new SubCategoryViewModel();
            var subCategory = _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();
            if (subCategory != null)
            {
                vm.Id = subCategory.Id;
                vm.Title = subCategory.Title;
                ViewBag.Categories = new SelectList(_context.Categories, "Id", "Title", subCategory.CategoryId);
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(SubCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var subCategory = _context.SubCategories.FirstOrDefault(x => x.Id == vm.Id);
                if (subCategory != null)
                {
                    subCategory.Title = vm.Title;
                    subCategory.CategoryId = vm.CategoryId;
                    _context.SubCategories.Update(subCategory);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Title", vm.CategoryId);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var subCategory = _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }




    }
}
