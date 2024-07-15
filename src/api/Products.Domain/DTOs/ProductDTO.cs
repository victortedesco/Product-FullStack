using Products.Domain.Entities;
using System.Text.Json.Serialization;

namespace Products.Domain.DTOs;

public class ProductDTO
{
    [JsonConstructor]
    public ProductDTO(string imageUrl, string name, string description, double price, uint discount, Guid? categoryId)
    {
        ImageUrl = imageUrl;
        Name = name;
        Description = description;
        Price = price;
        Discount = discount;
        CategoryId = categoryId;
    }

    public string ImageUrl { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double Price { get; init; }
    public uint Discount { get; init; }
    public Guid? CategoryId { get; init; }

    public static ProductDTO FromModel(Product model)
    {
        return new ProductDTO(model.ImageUrl, model.Name, model.Description, model.Price, model.Discount, model.CategoryId);
    }
}
