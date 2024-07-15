using FluentResults;
using Products.Domain.DTOs;
using Products.Domain.Interfaces;

namespace Products.Domain.Entities;

public class Category : IEntity
{
    private Category(string name, string description)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = CreatedAt;
        Name = name;
        Description = description;
        Products = [];
    }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<Product> Products { get; private set; }

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

    public void UpdateUpdatedAt()
    {
        UpdatedAt = DateTime.Now;
    }

    public static Category FromDTO(CategoryDTO categoryDTO)
    {
        return new Category(categoryDTO.Name, categoryDTO.Description);
    }

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }

    public static bool IsValidDescription(string description)
    {
        return !string.IsNullOrWhiteSpace(description);
    }
}

public static class CategoryErrors
{
    public static readonly Error DoesNotExist = new("This category does not exist.");
    public static readonly Error AlreadyExists = new("This category already exists.");

    public static readonly Error InvalidName = new("The name must not be empty.");
    public static readonly Error InvalidDescription = new("The description must not be empty.");
}
