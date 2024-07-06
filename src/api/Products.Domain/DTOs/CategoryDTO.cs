using Products.Domain.Entities;
using System.Text.Json.Serialization;

namespace Products.Domain.DTOs;

public class CategoryDTO
{
    [JsonConstructor]
    public CategoryDTO(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; init; }
    public string Description { get; init; }

    public static CategoryDTO FromModel(Category model)
    {
        return new CategoryDTO(model.Name, model.Description);
    }
}
