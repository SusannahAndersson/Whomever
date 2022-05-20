using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Models;

namespace Whomever.Controllers
{
    public class HomeController : Controller
    {
        private readonly Areas.ContactUsService.IContactUsService _contactUsService;
        private readonly ApplicationDbContext applicationDbContext;

        public HomeController(Areas.ContactUsService.IContactUsService contactUsService, ApplicationDbContext applicationDbContext)
        {
            _contactUsService = contactUsService;
            this.applicationDbContext = applicationDbContext;
        }

        //IActionResult to map logic to view
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send message thr Contact Us
                _contactUsService.SendMessage("susannah@whomever.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Message sent";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult WebShop()
        {
            //passing data from db to view
            var results = applicationDbContext.Products
                .OrderBy(p => p.Category)
                .ToList();
            return View(results.ToList());
        }

        //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //    public IActionResult Error()
        //    {
        //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }
    }
}