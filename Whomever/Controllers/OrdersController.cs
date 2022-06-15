using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Data.Entities;
using Whomever.Models;

namespace Whomever.Controllers
{
    //authorize: only authorized applicationuser that have been identified can access orderscontroller
    //jwtbearer - not cookies = postman
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IApplicationRepository applicationRepository, ILogger<OrdersController> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        //usage: gets orders made by aspplicationuser
        //bool includeitems = optional parameter
        //return getallorders collection
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                //find the identified applicationuser to users made orders
                var applicationUserName = User.Identity.Name;
                var getUserOrder = _applicationRepository.GetOrderByUser(applicationUserName, includeItems);
                return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(getUserOrder));
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get orders: {e}");
            }
            return BadRequest($"Unable to get orders");
        }

        //usage: gets orders by orderid made by applicationuser (http://localhost:5500/api/orders/1)
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                var orderById = _applicationRepository.GetOrderById(User.Identity.Name, id);
                //take passed in order and return mapped version of orderviewmodel (map from order to orderviewmodel)
                if (orderById != null) return Ok(_mapper.Map<Order, OrderViewModel>(orderById));
                else return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get orders: {e}");
                //return BadRequest(e.Message);
            }
            return BadRequest("Unable to get orders");
        }

        //usage: enables the current and validated applicationuser to create new order and saves order to db
        //mapping orderviewmodel
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //reverse map neworder
                    //convert model to order
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    //spec orderdate to today bc not required
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Today;
                    }
                    //find validated user
                    var modelUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = modelUser;
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
            catch (Exception e)
            {
                _logger.LogError($"Unable to save new order: {e}");
                //return BadRequest(e.Message);
            }
            return BadRequest("Unable to save new order");
        }

        //returns view for httpdelete
        public IActionResult Delete()
        {
            return View();
        }

        //usage: deletes order with specific orderid from db (http://localhost:5500/api/orders/1)
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"Unable to find order id: {id}");
                }
                Order orderId = _applicationRepository.RemoveOrder(id);
                if (orderId == null)
                {
                    return BadRequest($"Unable to find order id: {id} in db");
                }
                _applicationRepository.RemoveEntity(orderId);
                if (_applicationRepository.SaveAll())
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to delete order id{id}: {ex}");
                //return BadRequest(ex.Message);
            }
            return BadRequest($"Unable to delete order id{id}");
        }
    }
}