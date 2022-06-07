using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Models;

namespace Whomever.Controllers
{
    public class HomeController : Controller
    {
        private readonly Areas.ContactUsService.IContactUsService _contactUsService;
        private readonly IApplicationRepository _applicationRepository;

        public HomeController(Areas.ContactUsService.IContactUsService contactUsService, IApplicationRepository applicationRepository)
        {
            _contactUsService = contactUsService;
            _applicationRepository = applicationRepository;
        }

        //IActionResult to map logic to view
        public IActionResult Index()
        {
            //var showProducts = _applicationDbContext.Products.ToList();
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

        public IActionResult Webshop()
        {
            //passing data from db to view
            var productResults = _applicationRepository.GetAllProducts();
            return View(productResults);
        }

        //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //    public IActionResult Error()
        //    {
        //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }
    }
}