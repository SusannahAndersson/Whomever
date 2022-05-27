using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Data.Entities;

namespace Whomever.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller//Base
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IApplicationRepository applicationRepository, ILogger<ProductsController> logger)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
        }

        //get seed data db products
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        //public actionResult<IEnumerable<Product>>
        {
            try
            {//200
                return Ok(_applicationRepository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get products: {ex}");
                //400
                //return BadRequest(ex.Message);
                return BadRequest("Unable to get products");
            }
        }
    }
}