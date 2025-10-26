using Microsoft.AspNetCore.Mvc;
using EventsService.Services;
using EventsService.Data;

namespace EventsService.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly ILogger<EventsController> _logger;
    
    private readonly EventsProducerService _eventProducerService;

    public EventsController(EventsProducerService eventProducerService, ILogger<EventsController> logger)
    {
        _eventProducerService = eventProducerService;
        _logger = logger;
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateUserEvent([FromBody] UserEventRequest request)
    {
        var userEvent = new UserEvent
        {
            Id = request.Id,
            Action = request.Action,
            Email = request.Email
        };

        await _eventProducerService.ProduceUserEventAsync(userEvent);

        return Created("/", new
        {
            EventId = userEvent.EventId,
            Status = "success"
        });
    }

    [HttpPost("movie")]
    public async Task<IActionResult> CreateMovieEvent([FromBody] MovieEventRequest request)
    {
        var movieEvent = new MovieEvent
        {
            Id = request.Id,
            Title = request.Title,
            Action = request.Action
        };

        await _eventProducerService.ProduceMovieEventAsync(movieEvent);

        return Created("/", new
        {
            EventId = movieEvent.EventId,
            Status = "success"
        });
    }

    [HttpPost("payment")]
    public async Task<IActionResult> CreatePaymentEvent([FromBody] PaymentEventRequest request)
    {
        var paymentEvent = new PaymentEvent
        {
            Id = request.Id,
            Amount = request.Amount,
            Status = request.Status
        };

        await _eventProducerService.ProducePaymentEventAsync(paymentEvent);

        return Created("/", new
        {
            EventId = paymentEvent.EventId,
            Status = "success"
        });
    }
}
