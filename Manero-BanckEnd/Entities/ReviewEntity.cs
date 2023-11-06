using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities;

public class ReviewEntity
{
    public int Id { get; set; }
    public string  UserId { get; set; } = null!;

    public UserEntity User { get; set; } = null!;

    public string ArticleNumberId { get; set; } = null!;

    public ProductEntity Product { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public int Rating { get; set; }

    public DateTime Created { get; set; }
}
