using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace store2go.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Producer { get; set; } = "";
        [MaxLength(100)]
        public string Category { get; set; } = "";
        [Precision(16, 2)]
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        [MaxLength(30)]
        public string Barcode { get; set; } = "";
        public int Stored { get; set; }
        public int MinStored { get; set; }
        [MaxLength(100)]
        public string ImageFileName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
