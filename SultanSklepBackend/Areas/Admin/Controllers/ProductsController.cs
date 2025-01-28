using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SultanSklepBackend.DAL;
using SultanSklepBackend.Models;
using SultanSklepBackend.Utilities;
using SultanSklepBackend.ViewModels;

namespace SultanSklepBackend.Areas.Admin.Controllers
{

    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // Məhsulların siyahısı
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return View(products);
        }

        // Məhsul əlavə etmək formu
        public IActionResult Create()
        {
            var categories = _context.Categories
                .Where(c => !c.IsDeleted)
                .ToList();

            ViewBag.Categories = categories;

            return View();
        }

        // Məhsulu əlavə etmək (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AllViewModels model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories
                    .Where(c => !c.IsDeleted)
                    .ToList();

                return View(model);
            }

            var product = new Product
            {
                Name = model.ProductName,
                Description = model.Description,
                ProductCount = model.Count,
                Price = model.Price,
                CategoryId = model.CategoryId,
                IsInStock = model.IsInStock,
                IsDeleted = false
            };

            if (model.Photo != null)
            {
                // Şəkilin yüklənməsi
                var imagePath = await FileService.SaveFileAsync(model.Photo, "images/products");
                product.Image = imagePath;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Məhsul redaktəsi formu
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.IsDeleted)
            {
                return NotFound();
            }

            var categories = _context.Categories
                .Where(c => !c.IsDeleted)
                .ToList();

            ViewBag.Categories = categories;

            var viewModel = new AllViewModels
            {
                Id = product.Id,
                ProductName = product.Name,
                Description = product.Description,
                Count = product.ProductCount,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Image = product.Image,
                IsInStock = product.IsInStock

            };

            return View(viewModel);
        }

        // Məhsulu redaktə etmək (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AllViewModels model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories
                    .Where(c => !c.IsDeleted)
                    .ToList();

                return View(model);
            }

            var product = await _context.Products.FindAsync(model.Id);

            if (product == null || product.IsDeleted)
            {
                return NotFound();
            }

            product.Name = model.ProductName;
            product.Description = model.Description;
            product.ProductCount = model.Count;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.IsInStock = model.IsInStock;

            if (model.Photo != null)
            {
                // Şəkilin yenilənməsi
                var imagePath = await FileService.SaveFileAsync(model.Photo, "images/products");
                product.Image = imagePath;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Məhsulu silmək (soft delete)
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.IsDeleted)
            {
                return NotFound();
            }

            product.IsDeleted = true; // Məhsulu silinmiş kimi işarələyirik
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
