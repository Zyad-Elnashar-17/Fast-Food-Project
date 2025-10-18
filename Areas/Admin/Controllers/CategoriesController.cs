using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Fast_Food_Delievery.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Fast_Food_Delievery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var listFromDb = _context.Categories.ToList().Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Title = c.Title
            }).ToList();
            return View(listFromDb);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CategoryViewModel Category = new CategoryViewModel();
            return View(Category);
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel VM)
        {
            Category Model = new Category();

            {
                Model.Title = VM.Title;
                _context.Categories.Add(Model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _context.Categories.Where(c => c.Id == id)
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Title = c.Title
            }).FirstOrDefault();
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel VM)
        {
            if (!ModelState.IsValid)
            return View(VM);
            var categoryFromDb = _context.Categories.FirstOrDefault(c => c.Id == VM.Id);
                if (categoryFromDb!= null)
                {
                    categoryFromDb.Title = VM.Title;
                    _context.Categories.Update(categoryFromDb);
                    _context.SaveChanges();
                }  
                return RedirectToAction("Index");
            }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }







    }

}

