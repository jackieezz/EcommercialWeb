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
	internal sealed class OrderRepository : IOrderRepository
	{
		private readonly RepositoryDbContext _dbContext;
		public OrderRepository(RepositoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Orders.OrderBy(p => p.userId).AsNoTracking().ToListAsync(cancellationToken);
		}

		public async Task<Order> GetByIdAsync(ObjectId orderId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Orders.FirstOrDefaultAsync(x => x.orderId == orderId, cancellationToken);
		}
        public async Task<IEnumerable<Order>> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders.Where(x => x.userId == userId).AsNoTracking().ToListAsync(cancellationToken);
        }
        public void Insert(Order order)
		{
			_dbContext.Orders.Add(order);
		}

		public void Remove(Order order)
		{
			_dbContext.Orders.Remove(order);
		}
	}
}
