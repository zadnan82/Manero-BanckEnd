using Manero_BanckEnd.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Schemas
{
    public class ProductSchema
    {
        public string ArticleNumber { get; set; } = null!;
         
        public string Name { get; set; } = null!;
        public List<string> Size { get; set; } = new List<string>();
        public List<string> Color { get; set; } = new List<string>();

        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }

        //public DateTime Created {  get; set; } = DateTime.Now;
        //public DateTime Updated { get; set; } = DateTime.Now;
        public ProductType ProductType { get; set; }
        public bool IsFeatured { get; set; }
        public string ImageLink { get; set; }


    }
}
