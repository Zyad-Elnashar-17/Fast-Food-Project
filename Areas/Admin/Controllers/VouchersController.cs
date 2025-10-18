using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fast_Food_Delievery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VouchersController : Controller
    {
        private readonly AppDbContext _context;

        public VouchersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vouchers = _context.Vouchers.ToList();
            return View(vouchers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Voucher vouchers)
        {
            if (ModelState.IsValid)
            {
                _context.Vouchers.Add(vouchers);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vouchers);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var voucher = _context.Vouchers.Where(x => x.Id == id).FirstOrDefault();
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }

        [HttpPost]
        public IActionResult Edit(Voucher voucher)
        {
            if (!ModelState.IsValid)
            {
                return View(voucher);
            }
            var dbVoucher = _context.Vouchers.FirstOrDefault(x => x.Id == voucher.Id);
            if (dbVoucher == null)
            {
                return NotFound();
            }
            dbVoucher.Title = voucher.Title;
            dbVoucher.Type = voucher.Type;
            dbVoucher.Discount = voucher.Discount;
            dbVoucher.code = voucher.code;
            dbVoucher.MinAmount = voucher.MinAmount;
            dbVoucher.IsActive = voucher.IsActive;

            _context.SaveChanges();

            return RedirectToAction("Index");
        
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var voucher = _context.Vouchers.Where(x => x.Id == id).FirstOrDefault();
            if (voucher == null)
            {
                return NotFound();
            }
            _context.Vouchers.Remove(voucher);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }



    }
}
