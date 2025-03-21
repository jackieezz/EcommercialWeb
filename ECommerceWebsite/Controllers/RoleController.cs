using Domain.Entities;
using ECommerceWebsite.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.Controllers
{
	public class RoleController : Controller
	{
		private RoleManager<ApplicationRole> roleManager;
		public RoleController(RoleManager<ApplicationRole> roleManager)
		{
			this.roleManager = roleManager;
		}
		private void Errors(IdentityResult result)
		{
			foreach (IdentityError error in result.Errors)
				ModelState.AddModelError("", error.Description);
		}
		public IActionResult Index()
		{
			List<RoleViewModel> model = new();
			foreach (var role in roleManager.Roles)
			{
				model.Add(new RoleViewModel() { Id = role.Id, Name = role.Name });
			}
			return View(model);
		}
		public IActionResult Create(string id) => View(id);
		[HttpPost]
		public async Task<IActionResult> Create([Required] RoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				IdentityResult result = await roleManager.CreateAsync(new ApplicationRole() { Name = model.Name });
				if (result.Succeeded)
					ViewBag.Message = "Role Created Successfully";
				else
					Errors(result);
			}
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(string id)
		{
			var role = await roleManager.FindByIdAsync(id);
			if (role != null)
			{
				RoleViewModel model = new RoleViewModel() { Id = role.Id, Name = role.Name };
				return View(model);
			}
			else
				return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, string name)
		{
			var role = await roleManager.FindByIdAsync(id);
			if (role != null)
			{
				if (!string.IsNullOrEmpty(name))
					ModelState.AddModelError("", "Name cannot be empty");
				role.Name = name;
				IdentityResult result = await roleManager.UpdateAsync(role);
				if (result.Succeeded)
					return RedirectToAction("Index");
				else
					Errors(result);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await roleManager.FindByIdAsync(id);
			if (role != null)
			{
				IdentityResult result = await roleManager.DeleteAsync(role);
				if (result.Succeeded)
					return RedirectToAction("Index");
				else
					Errors(result);
			}
			else
				ModelState.AddModelError("", "No role found");
			return View("Index", roleManager.Roles);
		}
	}
}
