using CORE.DAL;
using Microsoft.AspNetCore.Identity;

namespace BLL
{
    public interface IUnitOfWork
    {
        //IRepository<User> _User { get; }
        //IRepository<IdentityRole> _IdentityRole { get; }
        void Save();
    }
}
