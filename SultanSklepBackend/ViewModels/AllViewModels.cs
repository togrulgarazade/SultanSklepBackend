using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SultanSklepBackend.Models;

namespace SultanSklepBackend.ViewModels
{
    public class AllViewModels
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; } // Tək şəkil üçün
        public List<IFormFile> Photos { get; set; } // Çoxlu şəkil üçün
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Category> Categories { get; set; }

        public decimal Price { get; set; }
        public bool IsInStock { get; set; } // Məhsulun mövcud olub olmadığını göstərir
        public List<Product> Product { get; set; }
        public List<ProductOperations> ProductsInCart { get; set; }
    }
}
