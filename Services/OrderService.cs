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
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	internal sealed class OrderService : IOrderService
	{
		public readonly IRepositoryManager _repositoryManager;
		public OrderService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}
		public async Task<OrderDTO> CreateAsync(OrderDTO order, CancellationToken cancellationToken = default)
		{
			var orderEntity = order.Adapt<Order>();
			_repositoryManager.OrderRepository.Insert(orderEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return orderEntity.Adapt<OrderDTO>();
		}

		public async Task DeleteAsync(ObjectId orderId, CancellationToken cancellationToken = default)
		{
			var orderEntity = await _repositoryManager.OrderRepository.GetByIdAsync(orderId, cancellationToken);
			if (orderEntity == null)
			{
				throw new OrderNotFoundException(orderId.ToString());
			}
			_repositoryManager.OrderRepository.Remove(orderEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<OrderDTO>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var orders = await _repositoryManager.OrderRepository.GetAllAsync(cancellationToken);
			return orders.Adapt<IEnumerable<OrderDTO>>();
		}

		public async Task<OrderDTO> GetByIdAsync(ObjectId orderId, CancellationToken cancellationToken = default)
		{
			var orderEntity = await _repositoryManager.OrderRepository.GetByIdAsync(orderId, cancellationToken);
			if (orderEntity == null)
			{
				throw new OrderNotFoundException(orderId.ToString());
			}
			return orderEntity.Adapt<OrderDTO>();
		}
        public async Task<IEnumerable<OrderDTO>> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken = default)
        {
            var orderEntity = await _repositoryManager.OrderRepository.GetByUserIdAsync(userId, cancellationToken);
            if (orderEntity == null)
            {
                throw new OrderNotFoundException(userId.ToString());
            }
            return orderEntity.Adapt<IEnumerable<OrderDTO>>();
        }
        public async Task UpdateAsync(ObjectId orderId, OrderDTO order, CancellationToken cancellationToken = default)
		{
			var orderEntity = await _repositoryManager.OrderRepository.GetByIdAsync(orderId, cancellationToken);
			if (orderEntity == null)
			{
				throw new OrderNotFoundException(orderId.ToString());
			}
			orderEntity.OrderDate = order.OrderDate;
			orderEntity.name = order.name;
			orderEntity.address = order.address;
			orderEntity.phoneNumber = order.phoneNumber;
			orderEntity.email = order.email;

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}
	}
}
