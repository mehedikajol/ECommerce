using ECommerce.Application.IGenericRepositories;
using ECommerce.Core.Entities;

namespace ECommerce.Application.IRepositories;

public interface IStockRepository : IGenericRepository<Stock, Guid>
{
}
