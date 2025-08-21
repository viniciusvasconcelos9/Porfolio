namespace EcommerceAPI.Core.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Relacionamento
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
