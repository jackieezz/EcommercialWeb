using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public sealed class PaymentNotFoundException : NotFoundException
	{
		public PaymentNotFoundException(string paymentId)
		: base($"Payment with paymentId {paymentId} was not found.")
		{
		}
	}
}
