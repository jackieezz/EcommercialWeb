using MongoDB.Bson;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface ICartService
	{
		Task<IEnumerable<CartDTO>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<CartDTO> GetByIdAsync(ObjectId cartId, CancellationToken cancellationToken = default);
		Task<CartDTO> CreateAsync(CartDTO cart, CancellationToken cancellationToken = default);
		Task UpdateAsync(ObjectId userId, CartDTO cart, CancellationToken cancellationToken = default);
		Task DeleteAsync(ObjectId userId, CancellationToken cancellationToken = default);
	}
}
