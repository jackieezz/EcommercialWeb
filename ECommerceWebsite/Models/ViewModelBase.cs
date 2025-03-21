using MongoDB.Bson;
using Services.Abstractions;
using System.Security.Claims;

namespace ECommerceWebsite.Models
{
	public abstract class ViewModelBase
	{
		public List<CartItemViewModel> cartViewModel { get; set; }
		public decimal total { get; set; }
	}
}
