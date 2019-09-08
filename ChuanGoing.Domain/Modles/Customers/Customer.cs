using ChuanGoing.Base.Domain;
using System;

namespace ChuanGoing.Domain.Modles.Customers
{
    public class Customer : Entity<Guid>
    {
        public Customer()
            : this(Guid.NewGuid(), null)
        { }

        public Customer(string name)
            : this(Guid.NewGuid(), name) { }

        public Customer(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
