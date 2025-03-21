using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IServiceManager
	{
		IProductService ProductService { get; }
		ICartService CartService { get; }
		IOrderService OrderService { get; }
		IPaymentService PaymentService { get; }
		IVnPayService VnPayService { get; }
	}
}
