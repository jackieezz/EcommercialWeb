using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
	public interface IVnPayService
	{
		string CreatePaymentUrl(HttpContext context,long amount);
		/*string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
		PaymentResponseModel PaymentExecute(IQueryCollection collections);*/
	}
}
