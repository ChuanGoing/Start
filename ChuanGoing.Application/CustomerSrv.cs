using ChuanGoing.Base.Interface.Db;
using ChuanGoing.Domain.Modles.Customers;
using System;
using System.Collections.Generic;

namespace ChuanGoing.Application
{
    public class CustomerSrv : ICustomerSrv
    {

        private readonly IRepository<Customer, Guid> _repository;
        //private readonly IEventBus _eventBus;

        public CustomerSrv(IRepository<Customer, Guid> repository)
        {
            _repository = repository;
        }

        public Customer Get(Guid id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public void AddCustomer(string name)
        {
            var customer = new Customer(name);

            _repository.Insert(customer);
        }
    }
}
