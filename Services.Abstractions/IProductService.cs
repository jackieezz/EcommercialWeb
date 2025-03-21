using MongoDB.Bson;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDTO>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<ProductDTO> GetByIdAsync(ObjectId productId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductDTO>> GetByQuery(string query, CancellationToken cancellationToken = default);
        Task<ProductDTO> CreateAsync(ProductDTO product, CancellationToken cancellationToken = default);
		Task UpdateAsync(ObjectId productId, ProductDTO product, CancellationToken cancellationToken = default);
		Task DeleteAsync(ObjectId productId, CancellationToken cancellationToken = default);
	}
}
