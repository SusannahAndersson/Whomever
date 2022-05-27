using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Whomever.Data.Entities;

namespace Whomever.Data
{
    public class ApplicationSeeder
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationSeeder(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)

        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        //seed db
        public async Task SeedAsync()
        {
            //seeddb
            _applicationDbContext.Database.EnsureCreated();
            //with user
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync("susannah@whomever.com");
            if (applicationUser == null)
            {
                applicationUser = new ApplicationUser()
                {
                    FirstName = "Susannah",
                    SurName = "Andersson",
                    Email = "susannah@whomever.com",
                    UserName = "susannah@whomever.com"
                };
                var createUser = await _userManager.CreateAsync(applicationUser, "Passw0rd!");
                if (createUser != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Unable to add new user");
                }
            }
            if (!_applicationDbContext.Products.Any())
            {
                //create seed data from whproduct.json but combine string path
                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Data/whproduct.json");
                var readFile = File.ReadAllText(filePath);
                var addProducts = JsonSerializer.Deserialize<IEnumerable<Product>>(readFile);
                _applicationDbContext.Products.AddRange(addProducts);
                //adds newly created applicationUser to order w id=1 from dbcontext builder
                var dbOrder = _applicationDbContext.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (dbOrder != null)
                {
                    dbOrder.User = applicationUser;
                    //adds orderitems to order
                    dbOrder.Items = new List<OrderItem>()
          {
            new OrderItem()
            {
              Product = addProducts.First(),
              Quantity = 1,
              UnitPrice = addProducts.First().Price
            }
          };
                }
                _applicationDbContext.SaveChanges();
            }
        }
    }
}