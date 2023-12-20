namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IBookRepository BookRepository { get; }
        IBookCategoryRepository BookCategoryRepository { get; }
        IRequestRepository RequestRepository { get; }
        IShoppingcartRepository ShoppingcartRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        void Save();
    }
}
