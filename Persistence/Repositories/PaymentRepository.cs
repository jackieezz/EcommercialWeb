using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
	public class PaymentRepository : IPaymentRepository
	{
		private readonly RepositoryDbContext _dbContext;
		public PaymentRepository(RepositoryDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Payments.OrderBy(p => p.userId).AsNoTracking().ToListAsync(cancellationToken);
		}

		public async Task<Payment> GetByIdAsync(ObjectId paymentId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Payments.FirstOrDefaultAsync(x => x.paymentId == paymentId, cancellationToken);
		}

		public async Task<Payment> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Payments.FirstOrDefaultAsync(x => x.userId == userId, cancellationToken);
		}

		public void Insert(Payment payment)
		{
			_dbContext.Payments.Add(payment);
		}

		public void Remove(Payment payment)
		{
			_dbContext.Payments.Remove(payment);
		}
	}
}
