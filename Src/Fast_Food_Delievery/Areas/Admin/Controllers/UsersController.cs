using Fast_Food_Delievery.Data;
using Fast_Food_Delievery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

namespace Fast_Food_Delievery.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var users = _context.Users.Where(u => u.Id != claim.Value).ToList();
            return View(users);

        }

        [HttpGet]
        public IActionResult Create()
        {
            ModelState.Clear();
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Create(User model, string Password, string Role)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newUser = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                City = model.City,
                Address = model.Address,
                PostalCode = model.PostalCode
            };

            var result = await _userManager.CreateAsync(newUser, Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role));
                }
                await _userManager.AddToRoleAsync(newUser, Role);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            TempData["success"] = "User created successfully!";
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User model, string newPassword)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.Email = model.Email;
            user.UserName = model.Email;
            user.FullName = model.FullName;
            user.City = model.City;
            user.Address = model.Address;
            user.PostalCode = model.PostalCode;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(newPassword))
            {
                var removeResult = await _userManager.RemovePasswordAsync(user);
                if (removeResult.Succeeded)
                {
                    var addPassResult = await _userManager.AddPasswordAsync(user, newPassword);
                    if (!addPassResult.Succeeded)
                    {
                        foreach (var error in addPassResult.Errors)
                            ModelState.AddModelError("", error.Description);
                        return View(model);
                    }
                }
                else
                {
                    foreach (var error in removeResult.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }
            [HttpGet]
            public async Task<IActionResult> Delete(string id)
            {
                if (id == null) return NotFound();

                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();

                return View(user);
            }

            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();

                await _userManager.DeleteAsync(user);

                TempData["success"] = "User deleted successfully!";
                return RedirectToAction("Index");
            }

        }
    } 

