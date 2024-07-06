using Products.Domain.DTOs;
using Products.Domain.Interfaces;

namespace Products.Domain.Entities;

public class Category : IEntity
{
    private Category(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

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
