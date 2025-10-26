namespace EventsService.Data;

public class UserEventRequest
{
    public int Id { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class PaymentEventRequest
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class MovieEventRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
}
