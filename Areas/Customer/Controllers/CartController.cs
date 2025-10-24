using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Fast_Food_Delievery.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Fast_Food_Delievery.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public CartOrderViewModel details { get; set; }
        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            details = new CartOrderViewModel()
            {
                OrderHeader = new Fast_Food_Delievery.Models.OrderHeader()
            };

            var ClaimsIdentity = (ClaimsIdentity)this.User.Identity;
            var Claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (Claim == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Customer" });
            }

            details.ListOfCart = _context.Carts
                .Where(c => c.UserId == Claim.Value)
                .Include(c => c.Item)
                .ToList();
            if (details.ListOfCart != null)
            {
                foreach (var cart in details.ListOfCart)
                {
                    if (cart.Item != null)
                    {
                        details.OrderHeader.OrderTotal += (cart.Item.Price * cart.Count);
                    }
                }
            }

          
            return View(details);
        }
    }
}
