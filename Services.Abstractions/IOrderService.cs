using MongoDB.Bson;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IOrderService
	{
		Task<IEnumerable<OrderDTO>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<OrderDTO> GetByIdAsync(ObjectId orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDTO>> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken = default);
        Task<OrderDTO> CreateAsync(OrderDTO order, CancellationToken cancellationToken = default);
		Task UpdateAsync(ObjectId orderId, OrderDTO order, CancellationToken cancellationToken = default);
		Task DeleteAsync(ObjectId orderId, CancellationToken cancellationToken = default);
	}
}
