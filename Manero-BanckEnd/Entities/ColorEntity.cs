using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities
{
    public class ColorEntity
    {
        [Key]
        public int ColorId { get; set; }
        public string Color { get; set; }
    }
}
