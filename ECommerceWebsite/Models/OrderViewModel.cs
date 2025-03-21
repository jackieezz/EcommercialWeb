using Domain.Entities;
using MongoDB.Bson;
using Shared;

namespace ECommerceWebsite.Models
{
	public class OrderViewModel : ViewModelBase
	{
		public string OrderDate { get; set; }
		public string userId { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string phoneNumber { get; set; }
		public string email { get; set; }
		public decimal total { get; set; }
		public List<CartItemViewModel> items { get; set; }
		public string orderId { get; set; }
	}
}
