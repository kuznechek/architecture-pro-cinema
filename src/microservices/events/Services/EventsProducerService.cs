using Confluent.Kafka;
using System.Text.Json;
using EventsService.Data;

namespace EventsService.Services;

public class EventsProducerService
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<EventsProducerService> _logger;

    public EventsProducerService(IConfiguration configuration, ILogger<EventsProducerService> logger)
    {
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = configuration["KAFKA_BROKERS"] ?? "localhost:9092",
            MessageTimeoutMs = 5000,
            RetryBackoffMs = 1000,
            EnableIdempotence = true
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceUserEventAsync(UserEvent userEvent)
    {
        await ProduceEventAsync("user-events", userEvent);
    }

    public async Task ProducePaymentEventAsync(PaymentEvent paymentEvent)
    {
        await ProduceEventAsync("payment-events", paymentEvent);
    }

    public async Task ProduceMovieEventAsync(MovieEvent movieEvent)
    {
        await ProduceEventAsync("movie-events", movieEvent);
    }

    private async Task ProduceEventAsync<T>(string topic, T eventData) where T : Event
    {
        try
        {
            var message = JsonSerializer.Serialize(eventData);
            var result = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

            _logger.LogInformation("Event sent to {1}: {2}", topic, message);
        }
        catch (ProduceException<Null, string> ex)
        {
            _logger.LogError(ex, "Failed to process message to {1}: {2}", topic, ex.Error.Reason);
            throw;
        }
    }

    public void Dispose()
    {
        _producer?.Dispose();
    }
}
