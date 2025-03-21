using Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IPaymentRepository
	{
		Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Payment> GetByIdAsync(ObjectId paymentId, CancellationToken cancellationToken = default);
		Task<Payment> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken = default);
		void Insert(Payment payment);
		void Remove(Payment payment);
	}
}
