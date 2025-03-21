using Domain.Entities;
using ECommerceWebsite.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Services;
using Services.Abstractions;
using Shared;
using System.Security.Claims;

namespace ECommerceWebsite.Controllers
{
	public class OrderController : Controller
	{
		private readonly IServiceManager serviceManager;
		private UserManager<ApplicationUser> userManager;
		public OrderController(IServiceManager serviceManager, UserManager<ApplicationUser> userManager)
		{
			this.serviceManager = serviceManager;
			this.userManager = userManager;
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Index()
		{
			var order = await serviceManager.OrderService.GetAllAsync();
            var viewmodel = new OrderDetailViewModel();
            viewmodel.orders = order.Adapt<List<OrderViewModel>>();
            return View(viewmodel);
        }

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var itemList = await getCart();
            var viewmodel = new OrderViewModel();
            var user = await userManager.GetUserAsync(User);

            viewmodel.OrderDate = DateTime.Now.ToString("dd/MM/yyyy");
            viewmodel.userId = userId;
            viewmodel.name = user.UserName;
            viewmodel.phoneNumber = user.PhoneNumber;
            viewmodel.email = user.Email;
            viewmodel.address = "";
            viewmodel.items = itemList;
            viewmodel.total = itemList.Sum(x => x.price * x.quantity);
            viewmodel.cartViewModel = itemList;

            return View(viewmodel);
        }
        [HttpPost]
		public async Task<IActionResult> Create(string userId ,string name, string address,string phoneNumber, string email)
        {
			//var orderEntity = vm.Adapt<OrderDTO>();
			var orderEntity = new OrderDTO();
			orderEntity.OrderDate = DateTime.Now;
			orderEntity.userId = userId;
			orderEntity.name = name;
			orderEntity.address = address;
			if(address == null)
			{
				orderEntity.address = "N/A";
			}
			orderEntity.phoneNumber = phoneNumber;
			orderEntity.email = email;
			var cart = await getCart();
			orderEntity.items = new List<CartItemDTO>();
			foreach(var item in cart)
			{
				orderEntity.items.Add(new CartItemDTO()
				{
					productName = item.name,
					productId = item.productId,
					quantity = item.quantity,
					price = item.price,
					imgUrl = item.imgUrl
				});
			}
			decimal total = cart.Sum(x => x.price * x.quantity)*24000;
			orderEntity.total = total;

            await serviceManager.OrderService.CreateAsync(orderEntity);
			await serviceManager.CartService.DeleteAsync(ObjectId.Parse(userId));
            string paymentUrl = serviceManager.VnPayService.CreatePaymentUrl(HttpContext, Convert.ToInt64(total));
            return Redirect(paymentUrl);
        }

		public async Task<IActionResult> History()
		{
			var order = await serviceManager.OrderService.GetByUserIdAsync(ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
			var viewmodel = new OrderDetailViewModel();
			var cart = await getCart();
			viewmodel.orders = order.Adapt<List<OrderViewModel>>();
			viewmodel.cartViewModel = cart;
			return View(viewmodel);
		}

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
			var order = await serviceManager.OrderService.GetByIdAsync(ObjectId.Parse(id));
            var itemList = await getCart();
            var viewmodel = new OrderViewModel();

			viewmodel.OrderDate = order.OrderDate.ToString();
            viewmodel.userId = order.userId;
            viewmodel.name = order.name;
            viewmodel.phoneNumber = order.phoneNumber;
            viewmodel.email = order.email;
            viewmodel.address = order.address;
			viewmodel.items = new();
            foreach (var item in order.items)
            {
                viewmodel.items.Add(new CartItemViewModel()
                {
                    productId = item.productId,
                    quantity = item.quantity,
                    price = item.price,
                    name = item.productName,
                    imgUrl = item.imgUrl
                });
            }
			viewmodel.total = order.total;
            viewmodel.cartViewModel = itemList;

            return View(viewmodel);
        }

        private async Task<List<CartItemViewModel>> getCart()
		{
			var cart = await serviceManager.CartService.GetByIdAsync(ObjectId.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
			List<CartItemViewModel> items = new List<CartItemViewModel>();
			if(cart != null)
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

        public async Task<IActionResult> PaymentSuccess()
        {
            return View();
        }
    }
}
