using ECommerceWebsite.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Services.Abstractions;
using Shared;
using System.Collections;
using System.Security.Claims;

namespace ECommerceWebsite.Controllers
{
	public class ProductController : Controller
	{
		private readonly IServiceManager _serviceManager;
		private List<CartItemViewModel> _cart;
		public ProductController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;

		}

		[HttpGet]
		public async Task<IActionResult> ProductList(string query)
		{
			IEnumerable<ProductDTO> productList = null;
            AllProductsViewModel viewModel = new AllProductsViewModel();
            if (query == null)
			{
                productList = await _serviceManager.ProductService.GetAllAsync();
                viewModel.products = productList;
			}
			else
			{
                productList = await _serviceManager.ProductService.GetByQuery(query);
                viewModel.products = productList;
            }
			
			viewModel.countProduct = productList.Count();
			if (User.Identity.IsAuthenticated)
			{
				viewModel.cartViewModel = await getCart();
				viewModel.total = viewModel.cartViewModel.Sum(x => x.price * x.quantity);
			}
			return View("ProductList", viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> ProductDetails(ObjectId productId)
		{
			Console.WriteLine(productId);
			var product = await _serviceManager.ProductService.GetByIdAsync(productId);
			return View("ProductDetails", product.Adapt<ProductViewModel>());
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
			_cart = items;
			return items;
		}
	}
}
