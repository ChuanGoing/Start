using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Base.Interface.Event;
using ChuanGoing.Web.API.Events;
using ChuanGoing.Web.API.Model;
using Dapper;
using EdaSample.Services.Customer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChuanGoing.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository _repository;
        private readonly IEventBus _eventBus;

        public CustomersController(IEventBus eventBus, IRepository repository)
        {
            _repository = repository;
            _eventBus = eventBus;
        }


        // 获取指定ID的客户信息
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            //const string sql = "SELECT `CustomerId` AS Id, `CustomerName` AS Name FROM `Customer` WHERE `CustomerId`=@CustomerId";

            //using (var connection = _repository.DbConnection)
            //{
            //    var customer = await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { CustomerId = id });
            //    if (customer == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(customer);
            //}

            //Docker test
            Customer customer = new Customer(Guid.NewGuid(),"Jack");
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

            //const string sql = "INSERT INTO `Customer` (`CustomerId`, `CustomerName`) VALUES (@Id, @Name)";
            //using (var connection = _repository.DbConnection)
            //{
            //    var customer = new Customer(name);

            //    await connection.ExecuteAsync(sql, customer);

            //    await _eventBus.PublishAsync(new CustomerCreatedEvent(name));

            //    return Created(Url.Action("Get", new { id = customer.Id }), customer.Id);
            //}
            //Docker test
            var customer = new Customer(name);
            return Created(Url.Action("Get", new { id = customer.Id }), customer.Id);
        }
    }
}
