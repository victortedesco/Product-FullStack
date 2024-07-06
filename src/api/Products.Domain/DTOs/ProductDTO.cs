using System.Text.Json.Serialization;

namespace Products.Domain.DTOs;

public class ProductDTO
{
    [JsonConstructor]
    public ProductDTO(string imageUrl, string name, string description, double price, uint discount)
    {
        ImageUrl = imageUrl;
        Name = name;
        Description = description;
        Price = price;
        Discount = discount;
    }

    public string ImageUrl { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double Price { get; init; }
    public uint Discount { get; init; }

    public static ProductDTO FromModel(ProductDTO model)
    {
        return new ProductDTO(model.ImageUrl, model.Name, model.Description, model.Price, model.Discount);
    }
}
