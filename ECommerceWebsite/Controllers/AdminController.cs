using Domain.Entities;
using ECommerceWebsite.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MongoDB.Bson;
using Services.Abstractions;
using Shared;

namespace ECommerceWebsite.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IServiceManager _serviceManager;

		public AdminController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> ProductList(string query)
		{
			IEnumerable<ProductDTO> productList = null;
			if(query == null)
			{
                productList = await _serviceManager.ProductService.GetAllAsync();
            }
			else
			{
				productList = await _serviceManager.ProductService.GetByQuery(query);
			}
            AdminProductViewModel viewModel = new AdminProductViewModel();
			viewModel._products = productList;
			return View("ProductList",viewModel);
		}
		[HttpGet]
		public IActionResult ProductAdd()
		{
			Console.WriteLine("get productAdd");
			return View("ProductAdd");
		}

		[HttpPost]
		public async Task<IActionResult> ProductAdd(ProductViewModel product, IFormFile Image)
		{
			var entity = new ProductDTO(
				product.name,
				product.description,
				product.category,
				product.price,
				//"product.imageUrl",
				"",
				product.timestamp
			);
			if(Image != null)
			{
				MemoryStream memoryStream = new MemoryStream();
				Image.OpenReadStream().CopyTo(memoryStream);
                entity.imageUrl = Convert.ToBase64String(memoryStream.ToArray());
            }
			
			//Console.WriteLine(product.imageUrl.ToString());
			await _serviceManager.ProductService.CreateAsync(entity);
			return RedirectToAction("ProductList","Admin");
		}

        [HttpGet]
        public async Task<IActionResult> ProductEdit(ObjectId productId)
        {
			var product = await _serviceManager.ProductService.GetByIdAsync(productId);
			return View("ProductEdit", product.Adapt<ProductViewModel>());
		}

        [HttpPost]
        public async Task<IActionResult> ProductEdit(string id, ProductViewModel product, IFormFile Image)
		{

			if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out ObjectId objectId))
			{
				Console.WriteLine("error");
				return BadRequest("Invalid product ID.");
			}
			var entity = await _serviceManager.ProductService.GetByIdAsync(ObjectId.Parse(id));

			entity.name = product.name;
			entity.description = product.description;
			entity.category = product.category;
			entity.price = product.price;
			entity.timestamp = product.timestamp;


            if (Image != null && Image.Length > 0) // Ensure the image is valid
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream); // Safely copy image data
                    entity.imageUrl = Convert.ToBase64String(memoryStream.ToArray());
                }
            }


            await _serviceManager.ProductService.UpdateAsync(ObjectId.Parse(id), entity);
			return RedirectToAction("ProductList", "Admin");
        }

		[HttpPost]
		public async Task<IActionResult> ProductDelete(string id)
		{
			await _serviceManager.ProductService.DeleteAsync(ObjectId.Parse(id));
			return RedirectToAction("ProductList", "Admin");
		}
	}
}
