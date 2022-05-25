using Microsoft.EntityFrameworkCore;
using Whomever.Data.Entities;

namespace Whomever.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<ApplicationRepository> _logger;

        public ApplicationRepository(ApplicationDbContext applicationDbContext, ILogger<ApplicationRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _applicationDbContext.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                _logger.LogInformation("GetAllOrders was called");
                return _applicationDbContext.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .OrderBy(o => o.OrderDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to call GetAllOrders: {ex}");
                return Enumerable.Empty<Order>();
            }
        }

        //default
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                return _applicationDbContext.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to call GetAllProducts: {ex}");
                return Enumerable.Empty<Product>();
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                _logger.LogInformation("GetOrderById was called");
                return _applicationDbContext.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .Where(i => i.Id == id)
                    .OrderBy(o => o.OrderDate)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to call GetOrderById: {ex}");
                return null;
            }
        }

        //category
        public IEnumerable<Product> GetProductsByCategory(string Category)
        {
            try
            {
                _logger.LogInformation("GetProductsByCategory successfully");
                return _applicationDbContext.Products
                    .Where(p => p.Category == Category)
                    //.OrderByDescending(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to call GetProductsByCategory: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}