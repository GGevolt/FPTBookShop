using FPTBookShop.Models;
                                                
namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail detail);
    }
}
