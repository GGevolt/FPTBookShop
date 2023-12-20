using FPTBookShop.DataAccess;
using FPTBookShop.Models;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShop.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public OrderDetailRepository(ApplicationDBContext dBContext):base(dBContext)
        {
            _dbContext = dBContext;       
        }

        public void Update(OrderDetail orderDetail)
        {
            _dbContext.Update(orderDetail);
        }
	}
}
