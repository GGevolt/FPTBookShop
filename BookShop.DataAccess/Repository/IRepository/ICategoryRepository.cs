using FPTBookShop.Models;
                                                
namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category);
    }
}
