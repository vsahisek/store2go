using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace store2go.Models
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Jméno je nezbytné"), MaxLength(100)]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Výrobce je nezbytný"), MaxLength(100)]
        public string Producer { get; set; } = "";
        [Required(ErrorMessage = "Kategorie je nutná"), MaxLength(100)]
        public string Category { get; set; } = "";
        [Required(ErrorMessage = "Taková cena není možná")]
        public decimal Price { get; set; }
        public string? Description { get; set; } = "";
        [MaxLength(30)]
        public string? Barcode { get; set; } = "";
        [Required(ErrorMessage = "Skladová hodnota je nezbytná")]
        public int Stored { get; set; }
        [Required(ErrorMessage = "Minimální skladová hodnota je nezbytná")]
        public int MinStored { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
