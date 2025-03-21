using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        [BsonId]
        public ObjectId orderId { get; set; }
		public DateTime OrderDate { get; set; }
		public ObjectId userId { get; set; }
		public ObjectId paymentId { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string phoneNumber { get; set; }
		public string email { get; set; }
		public decimal total { get; set; }
        public List<CartItem> items { get; set; }
    }
}
