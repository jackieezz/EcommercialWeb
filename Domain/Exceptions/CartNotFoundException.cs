using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
	public sealed class CartNotFoundException : NotFoundException
	{
		public CartNotFoundException(string userId)
		: base($"Cart with userID {userId} was not found.")
		{
		}
	}
}
