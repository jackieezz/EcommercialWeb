using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public sealed class OrderNotFoundException : NotFoundException
	{
		public OrderNotFoundException(string orderId)
		: base($"Order with orderId {orderId} was not found.")
		{
		}
	}
}
