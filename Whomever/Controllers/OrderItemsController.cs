using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Whomever.Data;
using Whomever.Data.Entities;
using Whomever.Models;

//view items from specific orderid (api uri)

namespace Whomever.Controllers
{
    [Route("api/orders/{orderId}/items")]
    [ApiController]
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

        //to get items from a specific order dep orderid
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int orderId)
        {
            var orderItem = _applicationRepository.GetOrderById(orderId);
            if (orderItem != null) return Ok(_mapper.Map<IEnumerable<OrderItemViewModel>>(orderItem.Items));
            return NotFound();
        }

        //to get orderitemid(=id)
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int orderId, int id)
        {
            var orderItem = _applicationRepository.GetOrderById(orderId);
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
    }
}