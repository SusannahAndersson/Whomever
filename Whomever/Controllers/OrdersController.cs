using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Data.Entities;

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

        //return getallorders collection
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_applicationRepository.GetAllOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                //return BadRequest(ex.Message);
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var orderById = _applicationRepository.GetOrderById(id);
                if (orderById != null) return Ok(orderById);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                //return BadRequest(ex.Message);
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order model)
        {
            //add to db
            try
            {
                _applicationRepository.AddEntity(model);
                if (_applicationRepository.SaveAll())
                {
                    return Created($"/api/orders/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new orders: {ex}");
                //return BadRequest(ex.Message);
            }
            return BadRequest("Failed to save new order");
        }
    }
}