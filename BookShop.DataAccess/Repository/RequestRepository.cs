using FPTBookShop.Models;
using FPTBookShop.DataAccess;
using FPTBookShop.DataAccess.Repository.IRepository;

namespace FPTBookShop.DataAccess.Repository
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public RequestRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Update(Request request)
        {
            _dbContext.Update(request);
        }
    }
}
