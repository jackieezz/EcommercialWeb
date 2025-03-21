namespace Shared
{
	public class ProductDTO
	{
		public string? productId { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string category { get; set; }
		public decimal price { get; set; }
		public string imageUrl { get; set; }
		public DateTime timestamp { get; set; }

		public ProductDTO() { }

		public ProductDTO(string name, string description, string category, decimal price, string imageUrl, DateTime timestamp)
		{
			this.name = name;
			this.description = description;
			this.category = category;
			this.price = price;
			this.imageUrl = imageUrl;
			this.timestamp = timestamp;
		}
	}
}
