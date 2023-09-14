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
       
        //private IRepository<User> User;
        //public IRepository<User> _User
        //{
        //    get { return User ?? (User = new Repository<User>(context, cacheService)); }
        //}

        //private IRepository<IdentityRole> identityRole;
        //public IRepository<IdentityRole> _IdentityRole
        //{
        //    get { return identityRole ?? (identityRole = new Repository<IdentityRole>(context, cacheService)); }
        //}
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
