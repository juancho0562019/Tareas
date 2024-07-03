namespace Domain.Common
{
    public abstract class BaseAuditableEntity<T> : BaseEntity<T> where T : notnull
    {
        public DateTimeOffset Created { get; set; }


        public DateTimeOffset LastModified { get; set; }

        public bool State { get; set; }
    }
}