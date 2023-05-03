using ECommerce.Application.IRepositories;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories;

namespace ECommerce.Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Categories = new CategoryRepository(_context);
        SubCategories = new SubCategoryRepository(_context);
        Products = new ProductRepository(_context);
        Stocks = new StockRepository(_context);
    }

    public ICategoryRepository Categories { get; private set; }
    public ISubCategoryRepository SubCategories { get; private set; }
    public IProductRepository Products { get; private set; }
    public IStockRepository Stocks { get; private set; }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
