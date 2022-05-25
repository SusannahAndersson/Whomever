using AutoMapper;
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
        private readonly IMapper _mapper;

        public OrdersController(IApplicationRepository applicationRepository, ILogger<OrdersController> logger, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        //bool includeitems = optional parameter
        //return getallorders collection
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var includeItemsResults = _applicationRepository.GetAllOrders(includeItems);

                return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(includeItemsResults));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest($"Failed to get orders");
            }
        }

        //[HttpGet]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //public IActionResult Get(bool includeItems = true)
        //{
        //    try
        //    {
        //        //mapping
        //        var includeItemsResult = _applicationRepository.GetAllOrders(includeItems);
        //        return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(includeItemsResult));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Failed to get orders: {ex}");
        //        //return BadRequest(ex.Message);
        //        return BadRequest("Failed to get orders");
        //    }
        //}

        //[HttpGet]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //public IActionResult Get(bool includeItems = true)
        //{
        //    try
        //    {
        //        //mapping
        //        var mapperGetAllOrders = _applicationRepository.GetAllOrders(includeItems);
        //        return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(mapperGetAllOrders));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Failed to get orders: {ex}");
        //        //return BadRequest(ex.Message);
        //        return BadRequest("Failed to get orders");
        //    }
        //}

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                var orderById = _applicationRepository.GetOrderById(id);
                //take passed in order and return mapped version of orderviewmodel (map from order to orderviewmodel)
                if (orderById != null) return Ok(_mapper.Map<Order, OrderViewModel>(orderById));
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
                    //reverse map neworder
                    ////convert model to order
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                    //spec orderdate to now bc not required
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    //add to entity neworder
                    _applicationRepository.AddEntity(newOrder);
                    if (_applicationRepository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
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