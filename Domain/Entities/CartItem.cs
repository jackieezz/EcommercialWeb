using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class CartItem
	{
		public ObjectId productId { get; set; }
		public string productName { get; set; }
		public int quantity { get; set; }
		public decimal price { get; set; }
		public string imgUrl { get; set; }
	}
}
