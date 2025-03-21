using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public sealed class ProductNotFoundException : NotFoundException
	{
		public ProductNotFoundException(string productId)
		: base($"Product with ID {productId} was not found.")
		{
		}
	}
}
