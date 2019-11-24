using ChuanGoing.Application;
using ChuanGoing.Application.ViewModels;
using ChuanGoing.Web.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChuanGoing.Web.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _service;

        public OrderController(ILogger<OrderController> logger, IOrderService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Permission("ABC")]
        public void Add([FromBody]OrderViewModel order)
        {
            _service.Add(order);
        }

        [HttpGet]
        [Permission("XYZ")]
        public OrderViewResult Get([FromQuery]string sn)
        {
            return _service.Get(sn);
        }
    }
}
