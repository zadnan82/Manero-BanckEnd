using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities;

public class ReviewEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public UserEntity User { get; set; }    

    public int ProductId { get; set; }

    public ProductEntity Product { get; set; }  

    public string Comment { get; set; } = null!;

    public int Rating { get; set; }

    public DateTime Created { get; set; }
}
