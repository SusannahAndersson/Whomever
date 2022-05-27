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
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get orders: {ex}");
                return BadRequest($"Unable to get orders");
            }
        }

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
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get orders: {ex}");
                //return BadRequest(ex.Message);
                return BadRequest("Unable to get orders");
            }
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
            catch (Exception ex)
            {
                _logger.LogError($"Unable to save new order: {ex}");
                //return BadRequest(ex.Message);
            }
            return BadRequest("Unable to save new order");
        }
    }
}