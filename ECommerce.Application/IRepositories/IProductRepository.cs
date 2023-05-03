using ECommerce.Application.IGenericRepositories;
using ECommerce.Core.Entities;

namespace ECommerce.Application.IRepositories;

public interface IProductRepository : IGenericRepository<Product, Guid>
{
}
