using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Data.Entities;
using Whomever.Models;

//[Route("api/orders/{orderId}/items")]
//[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

//view items from specific orderid (api url)

namespace Whomever.Controllers
{
    [Route("api/orders/{orderId}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IApplicationRepository applicationRepository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        //usage: get orderitems (items) from a specific order depending on orderid
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int orderId)
        {
            var orderItem = _applicationRepository.GetOrderById(User.Identity.Name, orderId);
            if (orderItem != null) return Ok(_mapper.Map<IEnumerable<OrderItemViewModel>>(orderItem.Items));
            return NotFound();
        }

        //usage: get orderitems from a specific order depending on orderitemid(=id)
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int orderId, int id)
        {
            var orderItem = _applicationRepository.GetOrderById(User.Identity.Name, id);
            if (orderItem != null)
            {
                var orderItemId = orderItem.Items.Where(i => i.Id == id).FirstOrDefault();
                if (orderItemId != null)
                {
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(orderItemId));
                }
            }
            return NotFound();
        }
        public IActionResult Order()
        {
            return View();
        }
    }
}