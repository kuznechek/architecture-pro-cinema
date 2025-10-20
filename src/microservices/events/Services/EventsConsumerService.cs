using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using EventsService.Data;

namespace EventsService.Services;

public class EventConsumerService : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<EventConsumerService> _logger;

    public EventConsumerService(IConfiguration configuration, ILogger<EventConsumerService> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "kafka:9092",
            GroupId = "cinemaabyss-events-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            EnableAutoOffsetStore = false
        };
        
        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        _consumer.Subscribe(new[] { "user-events", "payment-events", "movie-events" });
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(1000));
                
                if(consumeResult is null || consumeResult.Topic is null || consumeResult.Message.Value is null)
                {
                    await Task.Delay(100);
                        continue;
                };
                _logger.LogInformation(consumeResult.Message.Value);
                await ProcessMessageAsync(consumeResult.Topic, consumeResult.Message.Value);

                _consumer.StoreOffset(consumeResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming message.");
            }
        }

        _consumer.Close();
    }

    private async Task ProcessMessageAsync(string topic, string message)
    {
        try
        {
            switch (topic)
            {
                case "user-events":
                    var userEvent = JsonSerializer.Deserialize<UserEvent>(message);
                    _logger.LogInformation("UserEvent processed: UserId={Id}, Action={Action}, Email={Email}",
                        userEvent.Id, userEvent.Action, userEvent.Email);
                    break;

                case "payment-events":
                    var paymentEvent = JsonSerializer.Deserialize<PaymentEvent>(message);
                    _logger.LogInformation("PaymentEvent processed: PaymentId={Id}, Amount={Amount}, Status={Status}",
                        paymentEvent.Id, paymentEvent.Amount, paymentEvent.Status);
                    break;

                case "movie-events":
                    var movieEvent = JsonSerializer.Deserialize<MovieEvent>(message);
                    _logger.LogInformation("MovieEvent processed: MovieId={Id}, Title={Title}, Action={Action}",
                        movieEvent.Id, movieEvent.Title, movieEvent.Action);
                    break;
            }
            _logger.LogInformation("Process");
            await Task.Delay(100);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to deserialize message {1}: {2}", topic, message);
        }
    }

    public override void Dispose()
    {
        _consumer?.Close();
        _consumer?.Dispose();
        base.Dispose();
    }
}