using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IRepositoryManager
	{
		ICartRepository CartRepository { get; }
		IProductRepository ProductRepository { get; }
		IPaymentRepository PaymentRepository { get; }
		IOrderRepository OrderRepository { get; }
		IUnitOfWork UnitOfWork { get; }
	}
}
