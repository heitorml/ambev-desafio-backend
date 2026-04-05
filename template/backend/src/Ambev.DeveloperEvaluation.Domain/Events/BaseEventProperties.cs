namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class BaseEventProperties 
    {
        public Guid IdEvent { get; set; } = Guid.NewGuid();

        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
    }
}
