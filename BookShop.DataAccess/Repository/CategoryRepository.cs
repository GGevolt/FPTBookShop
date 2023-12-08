using FPTBookShopWeb.Data;
using FPTBookShopWeb.Models;
using FPTBookShopWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShopWeb.Repository
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryRepository(ApplicationDBContext dBContext):base(dBContext)
        {
            _dbContext = dBContext;       
        }

        public void Update(Category category)
        {
            _dbContext.Update(category);
        }
	}
}
