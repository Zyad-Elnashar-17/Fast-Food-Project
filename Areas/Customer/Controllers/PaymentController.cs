using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Fast_Food_Delievery.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult ProcessCashPayment(double amount)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            var cartItems = _context.Carts
                .Include(c => c.Item)
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            var orderHeader = new OrderHeader
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TimeOfPick = DateTime.Now.AddMinutes(15),
                TimeOfDelivery = DateTime.Now.AddHours(1),
                SubTotal = amount,
                OrderTotal = amount,
                TransactionId = Guid.NewGuid().ToString(),
                OrderStatus = "Pending",
                PaymentStatus = "Pending",
                PaymentMethod = "Cash",
                Name = "Unknown",
                PhoneNumber = "0000000000"
            };
            _context.SaveChanges();

            _context.OrderHeaders.Add(orderHeader);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetails
                {
                    OrderHeaderId = orderHeader.Id,
                    ItemId = item.ItemId,
                    Count = item.Count,
                    Price = item.Item.Price,
                    Name = item.Item.Name,
                    Description = item.Item.Description
                };
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
