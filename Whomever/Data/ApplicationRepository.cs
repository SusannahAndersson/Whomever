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

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                _logger.LogInformation("GetAllOrders was called");
                return _applicationDbContext.Orders
                    .OrderBy(p => p.OrderDate)
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