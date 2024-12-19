namespace API.Models
{
    public class Product
    {
		public Guid Id { get; set; } = Ulid.NewUlid().ToGuid();
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public decimal Price { get; set; }
    }

	public record ProductDto(string Name, string? Description, decimal Price);
}