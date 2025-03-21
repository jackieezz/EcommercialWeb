namespace Shared
{
	public class CartDTO
	{
		public string? cartId { get; set; }
		public string? userId { get; set; }
		public decimal total { get; set; }
		public List<CartItemDTO> items { get; set; }

		public CartDTO() { }
		public CartDTO(string? userId, List<CartItemDTO>? items, decimal total)
		{
			this.userId = userId;
			this.total = total;
			this.items = items;
		}
	}
}
