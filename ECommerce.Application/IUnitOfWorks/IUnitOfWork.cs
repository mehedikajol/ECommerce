using ECommerce.Application.IRepositories;

namespace ECommerce.Application.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }

        Task CompleteAsync();
    }
}
