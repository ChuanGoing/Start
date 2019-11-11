using ChuanGoing.Application;
using ChuanGoing.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChuanGoing.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add([FromBody]OrderViewModel order)
        {
            _service.Add(order);
        }

        [HttpGet]
        public OrderViewResult Get([FromQuery]string sn)
        {
            return _service.Get(sn);
        }
    }
}
