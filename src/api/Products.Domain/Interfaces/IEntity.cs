namespace Products.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get; init; }
    DateTime CreatedAt { get; init; }
    DateTime UpdatedAt { get; }

    // Guid CreatedById { get; }
    // User CreatedBy { get; }

    // Guid UpdatedById { get; }
    // User UpdatedBy { get; }
}
