using ChuanGoing.Base.Interface.Ioc;
using ChuanGoing.Domain.Modles.Customers;
using System;
using System.Collections.Generic;

namespace ChuanGoing.Application
{
    public interface ICustomerSrv: IScopeInstance
    {
        Customer Get(Guid id);
        IEnumerable<Customer> GetAll();
        void AddCustomer(string name);
    }
}
