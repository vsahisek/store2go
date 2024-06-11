using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using store2go.Models;
using store2go.Services;
using static System.Collections.Specialized.BitVector32;

namespace store2go.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();
        public Product Product { get; set; } = new Product();
        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet()
        {
        }
        public string errorMessage = "";
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Prosím vyplòte všechna dùležitá pole.";
                return Page();
            }
            // Ulozit obrazek
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            if (ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "Soubor obrázku je nezbytný.");
                errorMessage = "Prosím vyplòte všechna dùležitá pole.";
                return Page();
            } else
            {

                newFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);

                string imageFullPath = environment.WebRootPath + "/Data/Images/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductDto.ImageFile.CopyTo(stream);
                }
            }
            if (context.Products.Where(x => x.Name.Equals(ProductDto.Name)).Any())
            {
                errorMessage = "Toto jméno je již použito: " + ProductDto.Name;
                return Page();
            }
            if (context.Products.Where(x => x.Barcode.Equals(ProductDto.Barcode)).Any())
            {
                errorMessage = "Tento kód je již použit: " + ProductDto.Barcode;
                return Page();
            }
            // Ulozit DB zaznam
            Product product = new Product()
            {
                Name = ProductDto.Name,
                Producer = ProductDto.Producer,
                Category = ProductDto.Category,
                Price = ProductDto.Price,
                Description = ProductDto.Description ?? "",
                Barcode = ProductDto.Barcode ?? "",
                Stored = ProductDto.Stored,
                MinStored = ProductDto.MinStored,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Vycistit formular
            ProductDto.Name = "";
            ProductDto.Producer = "";
            ProductDto.Category = "";
            ProductDto.Price = 0;
            ProductDto.Description = "";
            ProductDto.Barcode = "";
            ProductDto.Stored = 0;
            ProductDto.MinStored = 0;
            ProductDto.ImageFile = null;

            ModelState.Clear();

            return RedirectToPage("./Index");
        }
    }
}
