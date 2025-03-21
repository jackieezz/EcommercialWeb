using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared
{
	public class OrderDTO
	{
		public string orderId { get; set; }
		public DateTime OrderDate { get; set; }
		public string userId { get; set; }
		public string paymentId { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string phoneNumber { get; set; }
		public string email { get; set; }
		public decimal total { get; set; }
		public List<CartItemDTO> items { get; set; }
	}
}
