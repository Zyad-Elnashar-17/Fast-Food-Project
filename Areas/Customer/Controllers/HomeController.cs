using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Fast_Food_Delievery.ViewModel;
using Microsoft.AspNetCore.Authentication;
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
                .FirstOrDefaultAsync(x => x.Id == id);

                if (item == null)
                return NotFound();

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
        [Authorize]
        public async Task<IActionResult> AddToCart(int id, int count = 1)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return Challenge(new AuthenticationProperties
                {
                    RedirectUri = Url.Action("Details", new { id })
                }, "Identity.Application");
            }

            var userId = claim.Value;
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();

            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ItemId == id);

            if (cartItem == null)
            {
                cartItem = new Cart { UserId = userId, ItemId = id, Count = count };
                _context.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count += count;
            }

            await _context.SaveChangesAsync();

            var totalCount = await _context.Carts
                .Where(c => c.UserId == userId)
                .SumAsync(c => c.Count);

            return Json(new { count = totalCount });
        }



    }
}
