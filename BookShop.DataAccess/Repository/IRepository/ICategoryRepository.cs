using FPTBookShopWeb.Models;

namespace FPTBookShopWeb.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category);
    }
}
