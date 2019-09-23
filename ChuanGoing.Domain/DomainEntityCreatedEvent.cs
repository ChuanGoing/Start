namespace ChuanGoing.Domain
{
    public  class DomainEntityCreatedEvent<TPrimaryKey>: DomainEvent<TPrimaryKey>
    {
        public DomainEntityCreatedEvent(TPrimaryKey newId)
        {
            this.NewId = newId;
        }

        public TPrimaryKey NewId { get; set; }
    }
}
