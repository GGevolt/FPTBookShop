using FPTBookShop.Models;

namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface IRequestRepository : IRepository<Request>
    {
        void Update(Request request);
    }
}
