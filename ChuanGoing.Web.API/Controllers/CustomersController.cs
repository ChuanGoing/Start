using ChuanGoing.Application;
using ChuanGoing.Domain.Modles.Customers.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChuanGoing.Web.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerSrv _service;

        public CustomersController(ICustomerSrv service)
        {
            _service = service;
        }


        // 获取指定ID的客户信息
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await Task.Run(() => _service.Get(id));
            return Ok(customer);
        }

        public async Task<IActionResult> GetAll()
        {
            var customers = await Task.Run(() => _service.GetAll());
            return Ok(customers);
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

            await Task.Run(() => _service.AddCustomer(name));

            return Ok();
        }
    }
}
