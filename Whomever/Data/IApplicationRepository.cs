using Whomever.Data.Entities;

namespace Whomever.Data
{
    //for testing mock data
    public interface IApplicationRepository
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetProductsByCategory(string Category);

        //bool includeitems = enable to return api uri w/wo itemsarray
        IEnumerable<Order> GetAllOrders(bool includeItems);

        Order GetOrderById(string applicationUserName, int id);

        IEnumerable<Order> GetOrderByUser(string applicationUserName, bool includeItems);

        void AddEntity(object model);

        bool SaveAll();
    }
}