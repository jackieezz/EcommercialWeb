using MongoDB.Bson;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IPaymentService
	{
		Task<IEnumerable<PaymentDTO>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<PaymentDTO> GetByIdAsync(ObjectId paymentId, CancellationToken cancellationToken = default);
		Task<PaymentDTO> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken = default);
		Task<PaymentDTO> CreateAsync(PaymentDTO payment, CancellationToken cancellationToken = default);
		Task UpdateAsync(ObjectId paymentId, PaymentDTO payment, CancellationToken cancellationToken = default);
		Task DeleteAsync(ObjectId paymentId, CancellationToken cancellationToken = default);
	}
}
