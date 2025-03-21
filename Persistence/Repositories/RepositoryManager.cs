using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
	public sealed class RepositoryManager : IRepositoryManager
	{
		private readonly Lazy<IProductRepository> _lazyProductRepository;
		private readonly Lazy<ICartRepository> _lazyCartRepository;
		private readonly Lazy<IOrderRepository> _lazyOrderRepository;
		private readonly Lazy<IPaymentRepository> _lazyPaymentRepository;
		private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

		public RepositoryManager(RepositoryDbContext dbContext)
		{
			_lazyCartRepository = new Lazy<ICartRepository>(() => new CartRepository(dbContext));
			_lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(dbContext));
			_lazyOrderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(dbContext));
			_lazyPaymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(dbContext));
			_lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
		}
		public IProductRepository ProductRepository => _lazyProductRepository.Value;
		public ICartRepository CartRepository => _lazyCartRepository.Value;
		public IOrderRepository OrderRepository => _lazyOrderRepository.Value;
		public IPaymentRepository PaymentRepository => _lazyPaymentRepository.Value;
		public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
	}
}
