using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Data.Entities;
using Whomever.Models;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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

        //mapping orderviewmodel
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            //add to db
            try
            {
                if (ModelState.IsValid)
                {
                    //convert model to order
                    var newOrder = new Order()
                    {
                        OrderDate = model.OrderDate,
                        OrderNumber = model.OrderNumber,
                        Id = model.OrderId
                    };
                    //spec orderdate to now bc not required
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    //add to entity neworder
                    _applicationRepository.AddEntity(newOrder);
                    if (_applicationRepository.SaveAll())
                    {
                        //then convert back to orderviewmodel to work with
                        var orderViewModel = new OrderViewModel
                        {
                            OrderId = newOrder.Id,
                            OrderDate = newOrder.OrderDate,
                            OrderNumber = newOrder.OrderNumber
                        };

                        return Created($"/api/orders/{orderViewModel.OrderId}", orderViewModel);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new order: {ex}");
                //return BadRequest(ex.Message);
            }
            return BadRequest("Failed to save new order");
        }
    }
}