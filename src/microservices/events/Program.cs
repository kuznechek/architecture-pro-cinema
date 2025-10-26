using EventsService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<EventsProducerService>();
builder.Services.AddHostedService<EventConsumerService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Urls.Add("http://*:8082");

app.Run();