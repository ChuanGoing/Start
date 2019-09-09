using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Interface.Event;
using ChuanGoing.Domain.Modles.Customers;
using ChuanGoing.Domain.Modles.Customers.Dtos;
using ChuanGoing.Domain.Modles.Customers.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChuanGoing.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer, Guid> _repository;
        private readonly IEventBus _eventBus;

        public CustomersController(IEventBus eventBus, IRepository<Customer, Guid> repository)
        {
            _repository = repository;
            _eventBus = eventBus;
        }


        // 获取指定ID的客户信息
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _repository.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // 创建新的客户信息
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDto model)
        {
            var name = model.Name;
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var customer = new Customer(name);
            var result = await _repository.InsertAsync(customer);
            await _eventBus.PublishAsync(new CustomerCreatedEvent(name));

            return Created(Url.Action("Get", new { id = customer.Id }), customer.Id);
        }
    }
}
