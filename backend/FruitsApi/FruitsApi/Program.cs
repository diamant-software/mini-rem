using FruitsApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISendToKafkaService, SendToKafkaService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.UseRouting();
app.Run();