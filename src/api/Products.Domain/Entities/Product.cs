using Products.Domain.DTOs;
using Products.Domain.Interfaces;

namespace Products.Domain.Entities;

public class Product : IEntity
{
    private Product(string imageUrl, string name, string description, double price, uint discount)
    {
        Id = Guid.NewGuid();
        ImageUrl = imageUrl;
        Name = name;
        Description = description;
        Price = price;
        Discount = discount;
    }

    public Guid Id { get; private set; }

    public string ImageUrl { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public uint Discount { get; private set; }

    public static Product FromDTO(ProductDTO productDTO)
    {
        return new Product(productDTO.ImageUrl, productDTO.Name, productDTO.Description, productDTO.Price, productDTO.Discount);
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
