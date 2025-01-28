using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SultanSklepBackend.DAL;
using SultanSklepBackend.Models;
using SultanSklepBackend.ViewModels;

namespace SultanSklepBackend.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var productOperations = await _context.ProductOperations
                .Where(po => po.UserID == userId && po.InCart == true)
                .Include(po => po.Product)
                .ToListAsync();

            if (!productOperations.Any())
            {
                return View(new AllViewModels
                {
                    ProductsInCart = new List<ProductOperations>()
                });
            }

            AllViewModels cartViewModel = new AllViewModels
            {
                ProductsInCart = productOperations
            };

            return View(cartViewModel);
        }
    }
}
