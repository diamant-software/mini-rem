using FruitRecognitionService.Persistence;
using FruitRecognitionService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IFruitRecognitionPipeline, FruitRecognitionPipeline>();
builder.Services.AddSingleton<IFruitClassificationService, FruitClassificationService>();
builder.Services.AddSingleton<IFruitRepository, FruitRepository>();
builder.Services.AddSingleton<IFruitClassifier, FruitClassifier>();
builder.Services.AddHostedService<ListenerService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseRouting();
app.Run();