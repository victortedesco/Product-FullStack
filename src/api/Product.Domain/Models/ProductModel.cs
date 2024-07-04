namespace Product.Domain.Models
{
    public class ProductModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string? ImageUrl { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Price { get; set; }
        public uint Discount { get; set; }

        public static bool IsValidProduct(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name)) return false;
            if (string.IsNullOrWhiteSpace(product.Description)) return false;
            if (product.Price < 0) return false;
            if (product.Discount > 80) return false;
            return true;
        }
    }
}
