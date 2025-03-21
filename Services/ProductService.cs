using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using MongoDB.Bson;
using Services.Abstractions;
using Shared;

namespace Services
{
	internal sealed class ProductService : IProductService
	{
		public readonly IRepositoryManager _repositoryManager;
		public ProductService(IRepositoryManager repositoryManager)
		{
			_repositoryManager = repositoryManager;
		}
		public async Task<IEnumerable<ProductDTO>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var products = await _repositoryManager.ProductRepository.GetAllAsync(cancellationToken);
			return products.Adapt<IEnumerable<ProductDTO>>();
		}
		public async Task<ProductDTO> GetByIdAsync(ObjectId productId, CancellationToken cancellationToken = default)
		{
			var productEntity = await _repositoryManager.ProductRepository.GetByIdAsync(productId, cancellationToken);
			if (productEntity == null)
			{
				throw new ProductNotFoundException(productId.ToString());
			}
			return productEntity.Adapt<ProductDTO>();
		}
        public async Task<IEnumerable<ProductDTO>> GetByQuery(string query, CancellationToken cancellationToken = default)
        {
            var products = await _repositoryManager.ProductRepository.GetByQuery(query, cancellationToken);
            return products.Adapt<IEnumerable<ProductDTO>>();
        }
        public async Task<ProductDTO> CreateAsync(ProductDTO product, CancellationToken cancellationToken = default)
		{
			var productEntity = product.Adapt<Product>();
			_repositoryManager.ProductRepository.Insert(productEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
			return productEntity.Adapt<ProductDTO>();
		}
		public async Task UpdateAsync(ObjectId productId, ProductDTO product, CancellationToken cancellationToken = default)
		{
			var productEntity = await _repositoryManager.ProductRepository.GetByIdAsync(productId, cancellationToken);
			if (productEntity == null)
			{
				throw new ProductNotFoundException(productId.ToString());
			}
			productEntity.name = product.name;
			productEntity.price = product.price;
			productEntity.description = product.description;
			productEntity.timestamp = product.timestamp;
			productEntity.category = product.category;
			productEntity.imageUrl = product.imageUrl;

			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}
		public async Task DeleteAsync(ObjectId productId, CancellationToken cancellationToken = default)
		{
			var productEntity = await _repositoryManager.ProductRepository.GetByIdAsync(productId, cancellationToken);
			if (productEntity == null)
			{
				throw new ProductNotFoundException(productId.ToString());
			}
			_repositoryManager.ProductRepository.Remove(productEntity);
			await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
		}
	}
}
