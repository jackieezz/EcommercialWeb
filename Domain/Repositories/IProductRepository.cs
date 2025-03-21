using Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Product> GetByIdAsync(ObjectId productId, CancellationToken cancellationToken = default);
		Task<IEnumerable<Product>> GetByQuery(string query, CancellationToken cancellationToken = default);
		void Insert(Product product);
		void Remove(Product product);
	}
}
