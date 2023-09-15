using CORE.DAL;
using Microsoft.AspNetCore.Identity;

namespace BLL
{
    public interface IUnitOfWork
    {
        IRepository<Product> _Product { get; }
        IRepository<Category> _Category { get; }
        void Save();
    }
}
