
namespace Domain.Entities
{
    public class Task : BaseAuditableEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public List<Time> Times { get; set; } = null!;
    }
}
