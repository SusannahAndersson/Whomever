using Microsoft.AspNetCore.Mvc;
using Whomever.Data;

namespace Whomever.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IApplicationRepository applicationRepository, ILogger<OrdersController> logger)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        //public actionResult<IEnumerable<Product>>
        {
            try
            {//200
                return Ok(_applicationRepository.GetAllOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                //400
                //return BadRequest(ex.Message);
                return BadRequest("Failed to get orders");

                //public IEnumerable<Orders> Get()
                //{
                //    try
                //    //{
                //    //    return ok(_applicationRepository.GetAllOrders());
                //        return ok(_applicationRepository.GetAllOrders());
                //    }
                //    catch (Exception ex)
                //    {
                //        _logger.LogError($"Failed to get orders: {ex}");
                //        return BadRequest("Failed to get orders");
                //    }
                //}
            }
        }
    }
}