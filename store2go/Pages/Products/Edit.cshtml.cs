using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using store2go.Models;
using store2go.Services;

namespace store2go.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;
        [BindProperty]
        public ProductDto ProductDto {  get; set; } = new ProductDto();
        public Product Product { get; set; } = new Product();
        public string errorMessage = "";
        public string successMessage = "";
        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Products/Index");
                return;
            }
            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Products/Index");
                return;
            }
            ProductDto.Name = product.Name;
            ProductDto.Producer = product.Producer;
            ProductDto.Category = product.Category;
            ProductDto.Description = product.Description;
            ProductDto.Barcode = product.Barcode;
            ProductDto.Stored = product.Stored;
            ProductDto.MinStored = product.MinStored;
            ProductDto.Price = product.Price;

            Product = product;

        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var product = await context.Products.FindAsync(id);
            if (id == null || context.Products == null)
            {
                errorMessage = "Nastala chyba, není co upravit";
                return Page();
            }
            if (!ModelState.IsValid)
            {
                Product = product;
                errorMessage = "Prosím vyplòte všechna dùležitá pole";
                return Page();
            }

            if (product == null)
            {
                errorMessage = "Produkt nenalezen";
                return Page();
            }

            // Unikatni jmeno
            if (context.Products.Where(x => x.Name.Equals(ProductDto.Name)).Any() && product.Name != ProductDto.Name)
            {
                errorMessage = "Toto jméno je již použito: " + ProductDto.Name;
                return Page();
            }

            // Unikatni barcode
            if (context.Products.Where(x => x.Barcode.Equals(ProductDto.Barcode)).Any() && product.Barcode != ProductDto.Barcode)
            {
                errorMessage = "Tento kód je již použit: " + ProductDto.Barcode;
                return Page();
            }

            // upravit obrazek kdyz je novy
            string newFileName = product.ImageFileName;

            if (ProductDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(ProductDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/Data/Images/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductDto.ImageFile.CopyTo(stream);
                }
                // vymazat starý
                string oldImageFullPath = environment.WebRootPath + "/Data/Images/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            // upravit db
            product.Name = ProductDto.Name;
            product.Producer = ProductDto.Producer;
            product.Category = ProductDto.Category;
            product.Price = ProductDto.Price;
            product.Description = ProductDto.Description ?? "";
            product.Barcode = ProductDto.Barcode ?? "";
            product.Stored = ProductDto.Stored;
            product.MinStored = ProductDto.MinStored;
            product.ImageFileName = newFileName;

            await context.SaveChangesAsync();


            Product = product;
            successMessage = "Produkt upraven!";
            return Page();
        }
    }
}
