using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Payment
	{
		[BsonId]
		public ObjectId paymentId { get; set; }
		public ObjectId userId { get; set; }
		public string method { get; set; }
		public string? nameOnCard { get; set; }
		public int? cardNumber { get; set; }
		public DateTime expiration { get; set; }
		public string? CVV { get; set; }
	}
}
