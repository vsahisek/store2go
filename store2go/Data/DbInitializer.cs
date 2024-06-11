using store2go.Services;
using store2go.Models;

namespace store2go.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }
            var products = new Product[]
            {
                new Product{Name="Chléb kmínový 500g",Producer="Penam",Category="Pečivo",Price=19,Description="Pečený s láskou",Barcode="280000000000123",Stored=1,MinStored=2,ImageFileName="chleb.jpg",CreatedAt=DateTime.Now},
                new Product{Name="Baterie AA 1,5V",Producer="GP",Category="Elektronika",Price=11,Description="Alkalická baterie",Barcode="280000000000133",Stored=5,MinStored=2,ImageFileName="baterie.jpg",CreatedAt=DateTime.Now},
                new Product{Name="Marlboro Red",Producer="Marlboro",Category="Tabák",Price=130,Description="Pouze osobám mladším 18 let.",Barcode="240000000000223",ImageFileName="marlboro.jpg",Stored=5,MinStored=1,CreatedAt=DateTime.Now},
                new Product{Name="Jablka 1kg",Producer="Store2go",Category="Ovoce",Price=34,Description="Čerstvě natrhaná jablka od našich farmářů, cena za 1kg.",Barcode="280000000004333",Stored=1,MinStored=1,ImageFileName="jablka.jpg",CreatedAt=DateTime.Now},
                new Product{Name="Mrkev 1kg",Producer="Store2go",Category="Zelenina",Price=26,Description="Čerstvě natrhaná mrkev od našich farmářů, cena za 1kg.",Barcode="280000000005333",Stored=5,MinStored=3,ImageFileName="mrkev.jpg",CreatedAt=DateTime.Now},
                new Product{Name="Mléko 1l",Producer="Madeta",Category="Mléčné výrobky",Price=22,Description="Spotřebujte do: 21.6.2024",Barcode="280000000000533",Stored=5,MinStored=2,ImageFileName="mleko.jpg",CreatedAt=DateTime.Now},
                new Product{Name="Krkovice 1kg",Producer="Store2go",Category="Maso",Price=22,Description="Spotřebujte do: 23.6.2024",Barcode="280000000000131",Stored=4,MinStored=2,ImageFileName="krkovice.jpg",CreatedAt=DateTime.Now},
                new Product{Name="Mattoni 1l",Producer="Mattoni",Category="Nápoje",Price=23,Description="Spotřebujte do: 11.11.2025",Barcode="280000000000333",Stored=5,MinStored=2,ImageFileName="mattoni.jpg",CreatedAt=DateTime.Now},
                new Product{Name="Pardál 0,5l",Producer="Pardál",Category="Alkohol",Price=17,Description="Spotřebujte do: 1.1.2028",Barcode="280000000000423",Stored=5,MinStored=2,ImageFileName="pardal.jpg",CreatedAt=DateTime.Now}
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
