namespace EcommerceAPI.Core.Entities;
using System.Text.Json.Serialization;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
