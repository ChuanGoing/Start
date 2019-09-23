using System;

namespace ChuanGoing.Domain.Modles.Customers
{
    public class Customer : DomainEntity<Guid>
    {
        public Customer(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
