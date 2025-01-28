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

namespace SultanSklepBackend.Controllers
{
    public class ProductOperationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductOperationsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        #region Cart Operations

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, string ReturnUrl)
        {

            var userId = _userManager.GetUserId(HttpContext.User);



            var productOperation = new ProductOperations()
            {
                ProductID = id,
                UserID = userId,
                InCart = true
            };

            await _context.ProductOperations.AddAsync(productOperation);
            await _context.SaveChangesAsync();

            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Cart", "View");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int id)
        {

            var userId = _userManager.GetUserId(HttpContext.User);



            //var dbProductOperation =
            //    await _context.productOperationsRepository.Get(po =>
            //        po.ProductId == id && po.ApplicationUserId == userId && po.InCart == true);

            //ProductOperations dbProductOperation = await _context.ProductOperations.FindAsync(id);
            ProductOperations dbProductOperation = await _context.ProductOperations
                .FirstOrDefaultAsync(po => po.ProductID == id && po.UserID == userId && po.InCart == true);


            dbProductOperation.InCart = false;

                _context.ProductOperations.Update(dbProductOperation);
                await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "View");
        }

        #endregion

        #region Order Operations
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Order()
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            List<ProductOperations> dbProductOperation = await _context.ProductOperations.ToListAsync();

            foreach (var item in  dbProductOperation)
            {
                if (item.UserID == userId && item.InCart == true)
                {
                    item.IsOrdered = true;
                    item.InCart = false;
                    item.OperationNumber = "ON_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                }

                _context.ProductOperations.Update(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("User", "Orders");




            //return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteFromOrders(int id, string operationNumber)
        {

            ProductOperations dbProductOperation = await _context.ProductOperations
                .FirstOrDefaultAsync(po => po.ProductID == id && po.OperationNumber == operationNumber && po.IsOrdered == true);


            var userId = dbProductOperation.UserID;


            dbProductOperation.IsOrdered = false;
            dbProductOperation.IsDeleted = true;

            _context.ProductOperations.Update(dbProductOperation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "View");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UnDeleteFromOrders(int id, string operationNumber)
        {
            ProductOperations dbProductOperation = await _context.ProductOperations
                .FirstOrDefaultAsync(po => po.ProductID == id && po.OperationNumber == operationNumber && po.IsOrdered == false && po.IsDeleted == true);


            var userId = dbProductOperation.UserID;


            dbProductOperation.IsOrdered = true;
            dbProductOperation.IsDeleted = false;

            _context.ProductOperations.Update(dbProductOperation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "View");
        }

        #endregion

        #region Pending Method

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Pending(string operationNumber)
        {


            List<ProductOperations> dbProductOperation = await _context.ProductOperations.ToListAsync();

            foreach (var item in dbProductOperation.Where(po=>po.OperationNumber==operationNumber))
            {
                var userId = item.UserID;

                if (item.UserID == userId && item.IsOrdered == true)
                {
                    item.IsPending = true;
                    item.IsOrdered = false;
                }

                _context.ProductOperations.Update(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Products", "PendingProducts", new { area = "Admin" });





            //return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteFromPendings(string operationNumber)
        {

            ProductOperations dbProductOperation = await _context.ProductOperations
                .FirstOrDefaultAsync(po => po.OperationNumber == operationNumber && po.IsPending == true);


            var userId = dbProductOperation.UserID;


            dbProductOperation.IsPending = false;
            dbProductOperation.IsOrdered = true;

            _context.ProductOperations.Update(dbProductOperation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "View");
        }

        #endregion

        #region Send Method

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Send(string operationNumber)
        {

            List<ProductOperations> dbProductOperation = await _context.ProductOperations.ToListAsync();

            foreach (var item in dbProductOperation.Where(po => po.OperationNumber == operationNumber))
            {
                var userId = item.UserID;
                if (item.UserID == userId && item.IsPending == true)
                {
                    item.IsOnTheWay = true;
                    item.IsPending = false;
                }

                _context.ProductOperations.Update(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Products", "SendedProducts", new { area = "Admin" });





            //return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteFromSendeds(string operationNumber)
        {

            ProductOperations dbProductOperation = await _context.ProductOperations
                .FirstOrDefaultAsync(po => po.OperationNumber == operationNumber && po.IsOnTheWay == true);


            var userId = dbProductOperation.UserID;


            dbProductOperation.IsOnTheWay = false;
            dbProductOperation.IsPending = true;

            _context.ProductOperations.Update(dbProductOperation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "View");
        }
        #endregion

        #region Complete Method

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Complete(string operationNumber)
        {

            List<ProductOperations> dbProductOperation = await _context.ProductOperations.ToListAsync();

            foreach (var item in dbProductOperation.Where(po=>po.OperationNumber==operationNumber))
            {
                var userId = item.UserID;

                if (item.UserID == userId && item.IsOnTheWay == true)
                {
                    item.IsCompleted = true;
                    item.IsOnTheWay = false;
                }

                _context.ProductOperations.Update(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Products", "CompletedProducts", new { area = "Admin" });





            //return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> NotCompleted(string operationNumber)
        {

            ProductOperations dbProductOperation = await _context.ProductOperations
                .FirstOrDefaultAsync(po => po.OperationNumber == operationNumber && po.IsCompleted == true);


            var userId = dbProductOperation.UserID;


            dbProductOperation.IsCompleted = false;
            dbProductOperation.IsOnTheWay = true;

            _context.ProductOperations.Update(dbProductOperation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "View");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCompleted(string operationNumber)
        {
            ProductOperations dbProductOperation = await _context.ProductOperations
                .FirstOrDefaultAsync(po => po.OperationNumber == operationNumber && po.IsCompleted == true);


            _context.ProductOperations.Remove(dbProductOperation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "View");
        }

        #endregion
    }
}
