using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Entities;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [MaxLength(100)]
    public string? Category { get; set; }
}
