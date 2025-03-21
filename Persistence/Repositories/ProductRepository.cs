using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
	internal sealed class ProductRepository : IProductRepository
	{
		private readonly RepositoryDbContext _dbContext;
		public ProductRepository(RepositoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Products.OrderBy(p => p.productId).AsNoTracking().ToListAsync(cancellationToken);
		}

		public async Task<Product> GetByIdAsync(ObjectId productId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Products.FirstOrDefaultAsync(x => x.productId == productId, cancellationToken);
		}

        public async Task<IEnumerable<Product>> GetByQuery(string query, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products.Where(x => x.name.Contains(query)).AsNoTracking().ToListAsync(cancellationToken);
        }

        public void Insert(Product product)
		{
			_dbContext.Products.Add(product);
		}

		public void Remove(Product product)
		{
			_dbContext.Products.Remove(product);
		}
	}
}
