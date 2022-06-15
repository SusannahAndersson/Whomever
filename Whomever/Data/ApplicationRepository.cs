using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Whomever.Data.Entities;

namespace Whomever.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<ApplicationRepository> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationRepository(ApplicationDbContext applicationDbContext, ILogger<ApplicationRepository> logger, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _userManager = userManager;
        }

        public void AddEntity(object model)
        {
            _applicationDbContext.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _applicationDbContext.Orders
                  .Include(o => o.Items)
                  .ThenInclude(i => i.Product)
                  .ToList();
            }
            else
            {
                return _applicationDbContext.Orders
                  .ToList();
            }
        }

        //default
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Log information: ApplicationRepository/GetAllProducts was called");
                return _applicationDbContext.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Log Error: Unable to call ApplicationRepository/GetAllProducts {ex}");
                return Enumerable.Empty<Product>();
            }
        }

        public Order GetOrderById(string applicationUserName, int id)
        {
            try
            {
                _logger.LogInformation("Log information: ApplicationRepository/GetOrderById was called");
                return _applicationDbContext.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .Where(i => i.Id == id && i.User.UserName == applicationUserName)
                    .OrderBy(o => o.OrderDate)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Log Error: Unable to call ApplicationRepository/GetOrderById {ex}");
                return null;
            }
        }

        public IEnumerable<Order> GetOrderByUser(string applicationUserName, bool includeItems)
        {
            if (includeItems)
            {
                return _applicationDbContext.Orders
                  .Where(u => u.User.UserName == applicationUserName)
                  .Include(o => o.Items)
                  .ThenInclude(i => i.Product)
                  .ToList();
            }
            else
            {
                return _applicationDbContext.Orders
                  .ToList();
            }
        }

        //category
        public IEnumerable<Product> GetProductsByCategory(string Category)
        {
            try
            {
                _logger.LogInformation("Log information: ApplicationRepository/GetProductsByCategory was called");
                return _applicationDbContext.Products
                    .Where(p => p.Category == Category)
                    //.OrderByDescending(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Log Error: Unable to call ApplicationRepository/GetProductsByCategory {ex}");
                return null;
            }
        }

        public void RemoveEntity(object model)
        {
            _applicationDbContext.Remove(model);
        }

        public Order RemoveOrder(int id)
        {
            try
            {
                _logger.LogInformation("Log information: ApplicationRepository/RemoveOrder was called");
                return _applicationDbContext.Orders.FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Log Error: Unable to call ApplicationRepository/RemoveOrder {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}