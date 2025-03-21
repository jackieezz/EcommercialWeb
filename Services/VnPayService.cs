using Amazon.Runtime.Internal.Util;
using Domain.Entities;
using Domain.Repositories;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
	public class VnPayService : IVnPayService
	{
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public readonly IRepositoryManager _repositoryManager;
		public VnPayService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}

		public string CreatePaymentUrl(HttpContext context, long amount)
		{
			// Get Config Info
			string vnp_Returnurl = "https://localhost:44346/Order/PaymentSuccess";//ConfigurationManager.AppSettings["vnp_Returnurl"]; // URL for results return
			string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //ConfigurationManager.AppSettings["vnp_Url"]; // VNPAY payment URL
			string vnp_TmnCode = "V9M9GBAP";  //ConfigurationManager.AppSettings["vnp_TmnCode"]; // Merchant site code
			string vnp_HashSecret = "O76TAXIIFBGM19W73S7QHKW6U4GLRVAM";  //ConfigurationManager.AppSettings["vnp_HashSecret"]; // Secret key

			// Create a payment order
			VnPayment order = new VnPayment
			{
				OrderId = DateTime.Now.Ticks, // Unique transaction ID
				Amount = amount, // Example amount: 100,000 VND
				Status = "0", // Pending payment
				CreatedDate = DateTime.Now
			};

			// Build VNPAY URL
			VnPayLibrary vnpay = new VnPayLibrary();
			vnpay.AddRequestData("vnp_Version", "2.1.0");
			vnpay.AddRequestData("vnp_Command", "pay");
			vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
			vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
			vnpay.AddRequestData("vnp_BankCode", "VNBANK");
			vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
			vnpay.AddRequestData("vnp_CurrCode", "VND");
			vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
			vnpay.AddRequestData("vnp_Locale", "vn");
			vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
			vnpay.AddRequestData("vnp_OrderType", "other");
			vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
			vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());


			// Generate payment URL
			string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
			log.InfoFormat("VNPAY URL: {0}", paymentUrl);

			return paymentUrl;
		}
	}
}
