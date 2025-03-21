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
	internal sealed class PaymentService : IPaymentService
	{
		public readonly IRepositoryManager _repositoryManager;
		public PaymentService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}
		public async Task<PaymentDTO> CreateAsync(PaymentDTO payment, CancellationToken cancellationToken = default)
		{
			var paymentEntity = payment.Adapt<Payment>();
			_repositoryManager.PaymentRepository.Insert(paymentEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return paymentEntity.Adapt<PaymentDTO>();
		}

		public async Task DeleteAsync(ObjectId paymentId, CancellationToken cancellationToken = default)
		{
			var paymentEntity = await _repositoryManager.PaymentRepository.GetByIdAsync(paymentId, cancellationToken);
			if (paymentEntity == null)
			{
				throw new PaymentNotFoundException(paymentId.ToString());
			}
			_repositoryManager.PaymentRepository.Remove(paymentEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<PaymentDTO>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var payments = await _repositoryManager.PaymentRepository.GetAllAsync(cancellationToken);
			return payments.Adapt<IEnumerable<PaymentDTO>>();
		}

		public async Task<PaymentDTO> GetByIdAsync(ObjectId paymentId, CancellationToken cancellationToken = default)
		{
			var paymentEntity = await _repositoryManager.PaymentRepository.GetByIdAsync(paymentId, cancellationToken);
			if (paymentEntity == null)
			{
				throw new PaymentNotFoundException(paymentId.ToString());
			}
			return paymentEntity.Adapt<PaymentDTO>();
		}

		public async Task<PaymentDTO> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken = default)
		{
			var paymentEntity = await _repositoryManager.PaymentRepository.GetByUserIdAsync(userId, cancellationToken);
			if (paymentEntity == null)
			{
				throw new PaymentNotFoundException(userId.ToString());
			}
			return paymentEntity.Adapt<PaymentDTO>();
		}

		public async Task UpdateAsync(ObjectId paymentId, PaymentDTO payment, CancellationToken cancellationToken = default)
		{
			var paymentEntity = await _repositoryManager.PaymentRepository.GetByIdAsync(paymentId, cancellationToken);
			if (paymentEntity == null)
			{
				throw new PaymentNotFoundException(paymentId.ToString());
			}
			paymentEntity.method = payment.method;
			paymentEntity.nameOnCard = payment.nameOnCard;
			paymentEntity.cardNumber = payment.cardNumber;
			paymentEntity.expiration = payment.expiration;
			paymentEntity.CVV = payment.CVV;

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}
	}
}
