using Azure.Monitor.OpenTelemetry.Exporter;
using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Managers;
using MemesFinderGreeter.Models.Options;
using MemesFinderGreeter.Options;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenTelemetry.Trace;
using Azure.Core.Serialization;
using Telegram.Bot;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("Azure.Messaging.ServiceBus.*"))
    .UseFunctionsWorkerDefaults()
    .UseAzureMonitorExporter();

builder.Services.Configure<WorkerOptions>(options =>
    options.Serializer = new NewtonsoftJsonObjectSerializer());

builder.Services.Configure<TelegramBotOptions>(builder.Configuration.GetSection("TelegramBotOptions"));
builder.Services.Configure<GreeterOptions>(builder.Configuration.GetSection("GreeterOptions"));
builder.Services.AddSingleton<ITelegramBotClient>(provider =>
    new TelegramBotClient(provider.GetRequiredService<IOptions<TelegramBotOptions>>().Value.Token));
builder.Services.AddTransient<IChatMemberManager, ChatMemberManager>();
builder.Services.AddTransient<IGreetingsFormatter, GreetingsFormatter>();

builder.Build().Run();
