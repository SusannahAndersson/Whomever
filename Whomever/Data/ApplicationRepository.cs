using Whomever.Data.Entities;

namespace Whomever.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ApplicationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //default
        public IEnumerable<Product> GetAllProducts()
        {
            return _applicationDbContext.Products
                .OrderBy(p => p.Title)
                .ToList();
        }

        //category
        public IEnumerable<Product> GetProductsByCategory(string Category)
        {
            return _applicationDbContext.Products
                .Where(p => p.Category == Category)
                //.OrderByDescending(p => p.Title)
                .ToList();
        }

        public bool SaveAll()
        {
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}