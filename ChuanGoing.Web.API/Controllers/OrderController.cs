using ChuanGoing.Application;
using ChuanGoing.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChuanGoing.Web.API.Controllers
{
    [Route("api/[controller]")]
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
        public void Add([FromBody]OrderViewModel order)
        {
            _logger.LogInformation("Order Add Start...");
            _service.Add(order);
            _logger.LogInformation("Order Add End...");
        }

        [HttpGet]
        public OrderViewResult Get([FromQuery]string sn)
        {
            return _service.Get(sn);
        }
    }
}
