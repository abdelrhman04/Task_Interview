using CORE.DAL;
using Microsoft.AspNetCore.Identity;

namespace BLL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyContext context;
        ICacheService cacheService;
        public UnitOfWork(MyContext _context, ICacheService cacheService)
        {
            context = _context;
            this.cacheService = cacheService;
        }

        private IRepository<Product> Product;
        public IRepository<Product> _Product
        {
            get { return Product ?? (Product = new Repository<Product>(context, cacheService)); }
        }

        private IRepository<Category> Category;
        public IRepository<Category> _Category
        {
            get { return Category ?? (Category = new Repository<Category>(context, cacheService)); }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
            System.GC.SuppressFinalize(this);
        }
    }
}
