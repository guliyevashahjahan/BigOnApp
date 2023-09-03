namespace BigOn.Infrastructure.Commons.Concrates
{
    public abstract class BaseEntity<Tkey> : AuditableEntity
        where Tkey : struct
    {
        public Tkey Id { get; set; }

    }
}
