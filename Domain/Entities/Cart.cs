using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Cart
	{
		[BsonId]
		public ObjectId cartId { get; set; }
		public ObjectId userId { get; set; }
		public decimal total { get; set; }
		public List<CartItem> items { get; set; }
	}
}
