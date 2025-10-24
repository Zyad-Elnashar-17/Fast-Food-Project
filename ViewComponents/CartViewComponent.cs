using Fast_Food_Delievery.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fast_Food_Delievery.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public CartViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ClaimsIdentity = (ClaimsIdentity)this.User.Identity;
            var Claim = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int cartCount = 0;

            if (Claim != null)
            {
                cartCount = _context.Carts.Where(c => c.UserId == Claim.Value).Count();
                HttpContext.Session.SetInt32("SessionCart", cartCount);
            }
            else
            {
                if (HttpContext.Session.GetInt32("SessionCart") != null)
                {
                    cartCount = (int)HttpContext.Session.GetInt32("SessionCart");
                }
            }

            return View(cartCount);

        }
    }
}
