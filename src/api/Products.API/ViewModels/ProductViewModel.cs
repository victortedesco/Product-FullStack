using Products.Domain.Entities;

namespace Products.API.ViewModels;

public class ProductViewModel
{
    private ProductViewModel(Guid id, string imageUrl, string name, string description, double price, uint discount)
    {
        Id = id;
        ImageUrl = imageUrl;
        Name = name;
        Description = description;
        Price = price;
        Discount = discount;
    }

    public Guid Id { get; init; }
    public string ImageUrl { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double Price { get; init; }
    public uint Discount { get; init; }

    public static ProductViewModel FromModel(Product product)
    {
        return new ProductViewModel(product.Id, product.ImageUrl, product.Name, product.Description, product.Price, product.Discount);
    }
}
