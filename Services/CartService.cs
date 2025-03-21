using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using MongoDB.Bson;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	internal sealed class CartService : ICartService
	{
		public readonly IRepositoryManager _repositoryManager;
		public CartService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}
		public async Task<CartDTO> CreateAsync(CartDTO cart, CancellationToken cancellationToken = default)
		{
			var cartEntity = cart.Adapt<Cart>();
			_repositoryManager.CartRepository.Insert(cartEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return cartEntity.Adapt<CartDTO>();
		}

		public async Task DeleteAsync(ObjectId userId, CancellationToken cancellationToken = default)
		{
			var cartEntity = await _repositoryManager.CartRepository.GetByIdAsync(userId, cancellationToken);
			if (cartEntity == null)
			{
				throw new CartNotFoundException(userId.ToString());
			}
			_repositoryManager.CartRepository.Remove(cartEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<CartDTO>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var carts = await _repositoryManager.CartRepository.GetAllAsync(cancellationToken);
			return carts.Adapt<IEnumerable<CartDTO>>();
		}

		public async Task<CartDTO> GetByIdAsync(ObjectId userId, CancellationToken cancellationToken = default)
		{
			var cartEntity = await _repositoryManager.CartRepository.GetByIdAsync(userId, cancellationToken);
			if (cartEntity == null)
			{
				await CreateAsync(new CartDTO { userId = userId.ToString() }, cancellationToken);
				return null;
			}
			var cart = new CartDTO();
			cart.cartId = cartEntity.cartId.ToString();
			cart.userId = cartEntity.userId.ToString();
			cart.items = new();
			if (cartEntity.items != null)
				foreach (var item in cartEntity.items) 
				{ 
					var cartItem = new CartItemDTO();
					cartItem.productName = item.productName;
					cartItem.productId = item.productId.ToString();
					cartItem.quantity = item.quantity;
					cartItem.price = item.price;
					cartItem.imgUrl = item.imgUrl;
					cart.items.Add(cartItem);
				}
			return cart;
		}

		public async Task UpdateAsync(ObjectId userId, CartDTO cart, CancellationToken cancellationToken = default)
		{
			var cartEntity = await _repositoryManager.CartRepository.GetByIdAsync(userId, cancellationToken);
			if (cartEntity == null)
			{
				throw new CartNotFoundException(userId.ToString());
			}
			if(cartEntity.items == null)
				cartEntity.items = new();

			cartEntity.items.Clear();
			foreach(var item in cart.items)
			{
				cartEntity.items.Add(new CartItem
				{
					productId = ObjectId.Parse(item.productId),
					productName = item.productName,
					quantity = item.quantity,
					price = item.price,
					imgUrl = item.imgUrl
				});
			}
			cartEntity.total = cartEntity.items.Sum(x => x.price * x.quantity);

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}
	}
}
