using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using store2go.Models;
using store2go.Services;

namespace store2go.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;
        [BindProperty]
        public Product Product { get; set; } = default!;
        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
 


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }
            var product = await context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                string imageFullPath = environment.WebRootPath + "/Data/Images/" + product.ImageFileName;
                System.IO.File.Delete(imageFullPath);
                context.Products.Remove(Product);
                await context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
