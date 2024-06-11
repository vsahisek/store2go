using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using store2go.Models;
using store2go.Services;
using System.Configuration;
using System;

namespace store2go.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Product> Products { get; set; } = new List<Product>();
        public string NameSort { get; set; }
        public string ProducerSort { get; set; }
        public string PriceSort { get; set; }
        public string CategorySort { get; set; }
        public string StoredSort { get; set; }
        public string MinStoredSort { get; set; }
        public string CreatedSort { get; set; }
        public string BarcodeSort { get; set; }
        public string IdSort { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = "Name";
            ProducerSort = "Producer";
            PriceSort = "Price";
            CategorySort = "Category";
            StoredSort = "Stored";
            MinStoredSort = "MinStored";
            CreatedSort = "Created";
            BarcodeSort = "Barcode";
            IdSort = "Id";
            IQueryable<Product> products = from s in _context.Products
                                             select s;
            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(s => s.Name);
                    break;
                case "Producer":
                    products = products.OrderBy(s => s.Producer);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "Category":
                    products = products.OrderBy(s => s.Category);
                    break;
                case "Stored":
                    products = products.OrderBy(s => s.Stored);
                    break;
                case "MinStored":
                    products = products.OrderBy(s => s.MinStored);
                    break;
                case "Created":
                    products = products.OrderBy(s => s.CreatedAt);
                    break;
                case "Barcode":
                    products = products.OrderBy(s => s.Barcode);
                    break;
                case "Id":
                    products = products.OrderBy(s => s.Id);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            Products = await products.AsNoTracking().ToListAsync();
        }
    }
}
