using Domain.Entities;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Services.Abstractions;
using Shared;
using System.Security.Claims;

namespace ECommerceWebsite.Controllers
{
	[Authorize(Roles = "Customer")]
	public class CartController : Controller
	{
		private readonly IServiceManager _serviceManager;
		public CartController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		[HttpPost]
		public async Task<IActionResult> Add(string productId, int quantity)
		{
			var userId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var userCart = await _serviceManager.CartService.GetByIdAsync(userId);
			var product = await _serviceManager.ProductService.GetByIdAsync(ObjectId.Parse(productId));
			if (userCart == null)
			{
				var entity = new CartDTO(
					User.FindFirstValue(ClaimTypes.NameIdentifier),
					new List<CartItemDTO>
					{
					new CartItemDTO(
						productId,
						product.name,
						quantity,
						product.price,
						product.imageUrl
					)
					},
					product.price * quantity
				);
				await _serviceManager.CartService.CreateAsync(entity);
			}
			else
			{
				if (userCart.items.Any(x => x.productId == productId))
				{
					var item = userCart.items.First(x => x.productId == productId);
					item.quantity += quantity;
				}
				else
				{
					userCart.items.Add(new CartItemDTO(
						productId,
						product.name,
						quantity,
						product.price,
						product.imageUrl
					));
				}
				await _serviceManager.CartService.UpdateAsync(userId, userCart);
			}
			return RedirectToAction("ProductList", "Product");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteItem([FromBody] string id)
		{
			var userId = ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var userCart = await _serviceManager.CartService.GetByIdAsync(userId);
			if (userCart.items.Any(x => x.productId == id))
			{
				var item = userCart.items.First(x => x.productId == id);
				userCart.items.Remove(item);
				await _serviceManager.CartService.UpdateAsync(userId, userCart);
			}
            return RedirectToAction("ProductList", "Product");
        }

		[HttpPost]
		public async Task<IActionResult> ClearCart()
		{
			await _serviceManager.CartService.DeleteAsync(ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
			return RedirectToAction("Index", "Home");
		}
	}
}
