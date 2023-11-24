using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities
{
    public class SizeEntity
    {
        [Key]
        public int SizeId { get; set; }
        public string Size { get; set; }
    }
}
