using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Fast_Food_Delievery.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Fast_Food_Delievery.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ItemListViewModel vm = new ItemListViewModel()
            {
                Items = await _context.Items.Include(X => X.Category).Include(y => y.SubCategory).ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                Vouchers = await _context.Vouchers.Where(c => c.IsActive == true).ToListAsync()
            };
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Items
                .Include(c => c.Category)
                .Include(s => s.SubCategory)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
           var cart = new Cart()
            {
                Item = item,
                ItemId = item.Id,
                Count = 1
           }
            ;
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(Cart cart)
        {
            if (ModelState.IsValid)
            {
                var ClaimsIdentity = (ClaimsIdentity)this.User.Identity;
                var Claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cart.UserId = Claim.Value;



                var cartFromDb = await _context.Carts
                    .Where(c => c.UserId == cart.UserId && c.ItemId == cart.ItemId)
                    .FirstOrDefaultAsync();

                if (cartFromDb == null)
                {
                    _context.Carts.Add(cart);
                }
                else
                {
                    cartFromDb.Count += cart.Count;
                }
                 _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ItemId == id);

            if (cartItem == null)
            {
                cartItem = new Cart { UserId = userId, ItemId = id, Count = 1 };
                _context.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count += 1;
            }

            await _context.SaveChangesAsync();

            var count = await _context.Carts.Where(c => c.UserId == userId).SumAsync(c => c.Count);

            return Json(new { count });
        }

    }
}
