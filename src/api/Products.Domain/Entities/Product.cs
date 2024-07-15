using FluentResults;
using Products.Domain.DTOs;
using Products.Domain.Interfaces;

namespace Products.Domain.Entities;

public class Product : IEntity
{
    private Product(string imageUrl, string name, string description, double price, uint discount, Guid? categoryId)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = CreatedAt;
        ImageUrl = imageUrl;
        Name = name;
        Description = description;
        Price = price;
        Discount = discount;
        CategoryId = categoryId;
    }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    public string ImageUrl { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public uint Discount { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public static Product FromDTO(ProductDTO dto)
    {
        return new Product(dto.ImageUrl, dto.Name, dto.Description, dto.Price, dto.Discount, dto.CategoryId);
    }

    public void UpdateImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
    }

    public bool UpdateName(string name)
    {
        if (!IsValidName(name))
        {
            return false;
        }
        Name = name;
        return true;
    }

    public bool UpdateDescription(string description)
    {
        if (!IsValidDescription(description))
        {
            return false;
        }
        Description = description;
        return true;
    }

    public bool UpdatePrice(double price)
    {
        if (!IsValidPrice(price))
        {
            return false;
        }
        Price = price;
        return true;
    }

    public bool UpdateDiscount(uint discount)
    {
        if (!IsValidDiscount(discount))
        {
            return false;
        }
        Discount = discount;
        return true;
    }

    public void UpdateUpdatedAt()
    {
        UpdatedAt = DateTime.Now;
    }

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }

    public static bool IsValidDescription(string description)
    {
        return !string.IsNullOrWhiteSpace(description);
    }

    public static bool IsValidPrice(double price)
    {
        return price > 0;
    }

    public static bool IsValidDiscount(uint discount)
    {
        return discount < 80;
    }
}

public static class ProductErrors
{
    public static readonly Error DoesNotExist = new("This category does not exist.");
    public static readonly Error AlreadyExists = new("This category already exists.");

    public static readonly Error InvalidName = new("The name must not be empty.");
    public static readonly Error InvalidDescription = new("The description must not be empty.");
    public static readonly Error InvalidPrice = new("The price must be more than 0.");
    public static readonly Error InvalidDiscount = new("The discount must be between 0% and 80%.");
}