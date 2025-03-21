using Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface ICartRepository
	{
		Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Cart> GetByIdAsync(ObjectId userId, CancellationToken cancellationToken = default);
		void Insert(Cart cart);
		void Remove(Cart cart);
	}
}
