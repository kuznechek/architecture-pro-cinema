namespace EventsService.Data;

public abstract class Event
{
    public Guid EventId { get; set; } = Guid.NewGuid();
}

public class UserEvent : Event
{
    public int Id { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class PaymentEvent : Event
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class MovieEvent : Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public double? Rating { get; set; }
}
