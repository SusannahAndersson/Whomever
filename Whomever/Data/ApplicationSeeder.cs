using System.Text.Json;
using Whomever.Data.Entities;

namespace Whomever.Data
{
    public class ApplicationSeeder
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationSeeder(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Seed()
        {
            _applicationDbContext.Database.EnsureCreated();

            if (!_applicationDbContext.Products.Any())
            {
                // Need to create the Sample Data
                var file = Path.Combine(_webHostEnvironment.ContentRootPath, "Data/whproduct.json");
                var json = File.ReadAllText(file);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                _applicationDbContext.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "5000",

                    Items = new List<OrderItem>()
                                {
                                    new OrderItem()
                                    {
                                        Product = products.First(),
                                        Quantity = 1,
                                        UnitPrice = products.First().Price
                                    }
                                }
                };
                _applicationDbContext.Orders.Add(order);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}