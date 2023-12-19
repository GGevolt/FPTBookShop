using FPTBookShop.DataAccess;
using FPTBookShop.Models;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShop.DataAccess.Repository
{
    public class ApplicationUserRepository:Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ApplicationUserRepository(ApplicationDBContext dBContext):base(dBContext)
        {
            _dbContext = dBContext;       
        }
	}
}
