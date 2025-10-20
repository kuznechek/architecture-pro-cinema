namespace EventsService.Models;

public abstract class BaseEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string EventType { get; set; } = string.Empty;
}

public class UserEvent : BaseEvent
{
    public int UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class PaymentEvent : BaseEvent
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string Status { get; set; } = string.Empty;
}

public class MovieEvent : BaseEvent
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public double? Rating { get; set; }
}
