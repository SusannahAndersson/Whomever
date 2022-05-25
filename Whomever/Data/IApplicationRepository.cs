using Whomever.Data.Entities;

namespace Whomever.Data
{
    //for testing mock data
    public interface IApplicationRepository
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetProductsByCategory(string Category);

        IEnumerable<Order> GetAllOrders();

        Order GetOrderById(int id);

        void AddEntity(object model);

        bool SaveAll();
    }
}