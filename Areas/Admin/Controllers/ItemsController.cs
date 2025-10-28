using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Fast_Food_Delievery.Settings;
using Fast_Food_Delievery.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fast_Food_Delievery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.Items
                .Include(x => x.Category)
                .Include(x => x.SubCategory)
                .ToList();

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.SubCategories = new SelectList(_context.SubCategories, "Id", "Title");
            return View(new ItemViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "Id", "Title");
                ViewBag.SubCategories = new SelectList(_context.SubCategories, "Id", "Title");
                return View(vm);
            }

            string? imageName = null;

            if (vm.ImageFile != null)
            {
                var ext = Path.GetExtension(vm.ImageFile.FileName);
                var allowed = FileSettings.AllowedExtensions.Split(',');
                if (!allowed.Contains(ext, StringComparer.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Image", $"Only {FileSettings.AllowedExtensions} are allowed");
                    return View(vm);
                }

                if (vm.ImageFile.Length > FileSettings.MaxFileSizeInBytes)
                {
                    ModelState.AddModelError("Image", $"Max size is {FileSettings.MaxFileSizeInMB} MB");
                    return View(vm);
                }

                imageName = await SaveImageAsync(vm.ImageFile);
            }

            var item = new Item
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = (double)vm.Price,
                CategoryId = vm.CategoryId,
                SubCategoryId = vm.SubCategoryId,
                ImageUrl = imageName != null ? $"{FileSettings.ImagesPath}/{imageName}" : null
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            var vm = new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = (decimal)item.Price,
                CategoryId = item.CategoryId,
                SubCategoryId = item.SubCategoryId,
                ExistingImageUrl = item.ImageUrl
            };

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Title", item.CategoryId);
            ViewBag.SubCategories = new SelectList(_context.SubCategories, "Id", "Title", item.SubCategoryId);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "Id", "Title", vm.CategoryId);
                ViewBag.SubCategories = new SelectList(_context.SubCategories, "Id", "Title", vm.SubCategoryId);
                return View(vm);
            }

            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (item == null)
                return NotFound();

            item.Name = vm.Name;
            item.Description = vm.Description;
            item.Price = (double)vm.Price;
            item.CategoryId = vm.CategoryId;
            item.SubCategoryId = vm.SubCategoryId;

            if (vm.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, item.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var imageName = await SaveImageAsync(vm.ImageFile);
                item.ImageUrl = $"{FileSettings.ImagesPath}/{imageName}";
            }

            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _context.Items
                .Include(x => x.Category)
                .Include(x => x.SubCategory)
                .FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();

            if (!string.IsNullOrEmpty(item.ImageUrl))
            {
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, item.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSettings.ImagesPath.TrimStart('/'));

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(uploadPath, imageName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return imageName;
        }
    }
}
