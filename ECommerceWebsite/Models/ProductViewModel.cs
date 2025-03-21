namespace ECommerceWebsite.Models
{
    public class ProductViewModel : ViewModelBase
    {
		public string productId { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string category { get; set; }
		public decimal price { get; set; }
		public string imageUrl { get; set; }
		public DateTime timestamp { get; set; }
		public ProductViewModel() { }

		public ProductViewModel(string productId, string name, string description, string category, decimal price, string imageUrl, DateTime timestamp)
		{
			this.productId = productId;
			this.name = name;
			this.description = description;
			this.category = category;
			this.price = price;
			this.imageUrl = imageUrl;
			this.timestamp = timestamp;
		}
	}
}
