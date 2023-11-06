using Manero_BanckEnd.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Schemas
{
    public class ProductSchema
    {
        public string ArticleNumber { get; set; } = null!;
         
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Color { get; set; } = null!;

        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }

        //public DateTime Created {  get; set; } = DateTime.Now;
        //public DateTime Updated { get; set; } = DateTime.Now;
        public ProductType ProductType { get; set; }


    }
}
