namespace Domain.Entities
{
    public class Time
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Task Task { get; set; } = null!;
    }
}
