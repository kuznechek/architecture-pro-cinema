using EventsService.Models;
using EventsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventProducerService _eventProducer;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IEventProducerService eventProducer, ILogger<EventsController> logger)
    {
        _eventProducer = eventProducer;
        _logger = logger;
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateUserEvent([FromBody] UserEventRequest request)
    {
        var userEvent = new UserEvent
        {
            UserId = request.UserId,
            Action = request.Action,
            Email = request.Email
        };

        await _eventProducer.ProduceUserEventAsync(userEvent);

        return Created("/", new
        {
            EventId = userEvent.EventId,
            Message = "User event created successfully",
            Status = "success"
        });
    }

    [HttpPost("payment")]
    public async Task<IActionResult> CreatePaymentEvent([FromBody] PaymentEventRequest request)
    {
        var paymentEvent = new PaymentEvent
        {
            PaymentId = request.PaymentId,
            Amount = request.Amount,
            Currency = request.Currency,
            Status = request.Status
        };

        await _eventProducer.ProducePaymentEventAsync(paymentEvent);

        return Created("/", new
        {
            EventId = paymentEvent.EventId,
            Message = "Payment event created successfully",
            Status = "success"
        });
    }

    [HttpPost("movie")]
    public async Task<IActionResult> CreateMovieEvent([FromBody] MovieEventRequest request)
    {
        var movieEvent = new MovieEvent
        {
            MovieId = request.MovieId,
            Title = request.Title,
            Action = request.Action,
            Rating = request.Rating
        };

        await _eventProducer.ProduceMovieEventAsync(movieEvent);

        return Created("/", new
        {
            EventId = movieEvent.EventId,
            Message = "Movie event created successfully",
            Status = "success"
        });
    }
}

// DTO классы для запросов
public class UserEventRequest
{
    public int UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class PaymentEventRequest
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string Status { get; set; } = string.Empty;
}

public class MovieEventRequest
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public double? Rating { get; set; }
}