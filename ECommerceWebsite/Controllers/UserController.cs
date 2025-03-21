using Domain.Entities;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.Controllers
{
	public class UserController : Controller
	{

		private UserManager<ApplicationUser> userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}

		private void Errors(IdentityResult result)
		{
			foreach (IdentityError error in result.Errors)
				ModelState.AddModelError("", error.Description);
		}
		[Authorize(Roles="Admin")]
		public IActionResult Index()
		{
			List<UserViewModel> model = new();
			foreach (var user in userManager.Users)
			{
				model.Add(new UserViewModel() { Id = user.Id, Name = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber });
			}
			return View(model);
		}


		public async Task<IActionResult> Edit(string id)
		{
			var user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				UserViewModel model = new UserViewModel() { Id = user.Id, Name = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber };
				return View(model);
			}
			else
				return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, string name, string email, string password, string phoneNumber)
		{
			var user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				if (!string.IsNullOrEmpty(name))
					user.UserName = name;
				else
					ModelState.AddModelError("", "Name cannot be empty");

				if (!string.IsNullOrEmpty(email))
					user.Email = email;
				else
					ModelState.AddModelError("", "Email cannot be empty");

				if (!string.IsNullOrEmpty(password))
					user.PasswordHash = userManager.PasswordHasher.HashPassword(user, password);
				else
					ModelState.AddModelError("", "Password cannot be empty");

				if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
				{
					IdentityResult result = await userManager.UpdateAsync(user);
					if (result.Succeeded)
						return RedirectToAction("Index");
					else
						Errors(result);
				}
			}
			else
				ModelState.AddModelError("", "User Not Found");
			return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await userManager.DeleteAsync(user);
				if (result.Succeeded)
					return RedirectToAction("Index");
				else
					Errors(result);
			}
			else
				ModelState.AddModelError("", "User Not Found");
			return View("Index", userManager.Users);
		}
	}
}
