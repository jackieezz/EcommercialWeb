using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Services.Abstractions;
using System.Diagnostics;
using System.Security.Claims;

namespace ECommerceWebsite.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IServiceManager _serviceManager;


		public HomeController(ILogger<HomeController> logger, IServiceManager serviceManager)
		{
			_logger = logger;
			_serviceManager = serviceManager;
		}
		public async Task<IActionResult> Index()
		{
			HomeViewModel viewModel = new HomeViewModel();
			if (User.Identity.IsAuthenticated)
				viewModel.cartViewModel = await getCart();
			return View(viewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private async Task<List<CartItemViewModel>> getCart()
		{
			var cart = await _serviceManager.CartService.GetByIdAsync(ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
			List<CartItemViewModel> items = new List<CartItemViewModel>();
			if (cart != null)
				foreach (var item in cart.items)
				{
					items.Add(new CartItemViewModel()
					{
						productId = item.productId,
						quantity = item.quantity,
						price = item.price,
						name = item.productName,
						imgUrl = item.imgUrl
					});
				}
			return items;
		}
	}
}
