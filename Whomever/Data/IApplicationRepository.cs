using Whomever.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Whomever.Data
{
    //for testing mock data
    public interface IApplicationRepository
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetProductsByCategory(string Category);

        bool SaveAll();

        IEnumerable<Order> GetAllOrders();
    }
}