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
	internal sealed class CartRepository : ICartRepository
	{
		private readonly RepositoryDbContext _dbContext;
		public CartRepository(RepositoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Carts.OrderBy(p => p.userId).AsNoTracking().ToListAsync(cancellationToken);
		}

		public async Task<Cart> GetByIdAsync(ObjectId userId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Carts.FirstOrDefaultAsync(x => x.userId == userId, cancellationToken);
		}

		public void Insert(Cart cart)
		{
			_dbContext.Carts.Add(cart);
		}

		public void Remove(Cart cart)
		{
			_dbContext.Carts.Remove(cart);
		}
	}
}
