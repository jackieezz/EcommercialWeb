using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared
{
	public class PaymentDTO
	{
		public string paymentId { get; set; }
		public string userId { get; set; }
		public string method { get; set; }
		public string? nameOnCard { get; set; }
		public int? cardNumber { get; set; }
		public DateTime expiration { get; set; }
		public string? CVV { get; set; }
	}
}
