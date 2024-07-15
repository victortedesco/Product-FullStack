using Products.Domain.Entities;

namespace Products.API.ViewModels;

public class ProductViewModel
{
    private ProductViewModel(Guid id, DateTime createdAt, DateTime updatedAt, string imageUrl, string name, string description, double price, uint discount, Category category)
    {
        Id = id;
        ImageUrl = imageUrl;
        Name = name;
        Description = description;
        Price = price;
        Discount = discount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Category = CategoryViewModel.FromModel(category);
    }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public string ImageUrl { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double Price { get; init; }
    public uint Discount { get; init; }
    public CategoryViewModel Category { get; init; }

    public static ProductViewModel FromModel(Product model)
    {
        return new ProductViewModel(model.Id, model.CreatedAt, model.UpdatedAt, model.ImageUrl, model.Name, model.Description, model.Price, model.Discount, model.Category);
    }
}
