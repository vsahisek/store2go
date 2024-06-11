using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using store2go.Models;

namespace store2go.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
