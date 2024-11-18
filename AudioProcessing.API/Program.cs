using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using AudioProcessing.Aplication;
using AudioProcessing.Infrastructure.Persistence;
using AudioProcessing.Infrastructure.Configurations.Persistence;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:4200") 
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddAudioProcessingServices();
builder.Services.AddPersistenceServices(builder.Configuration);


builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(builder => builder.AddService(serviceName: "audioprocessing-api"))
    .WithMetrics(metrics =>
    {
        metrics
            .AddMeter("audioprocessingMeter")
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddProcessInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter();

        metrics.AddOtlpExporter(options =>
            options.Endpoint = new Uri("http://i3lab-dashboard:18889")); 
    })
    .WithTracing(tracing =>
    {
        tracing.AddHttpClientInstrumentation()
               .AddAspNetCoreInstrumentation()
               .AddNpgsql()
               .AddEntityFrameworkCoreInstrumentation()
               .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

        tracing.AddOtlpExporter(options =>
            options.Endpoint = new Uri("http://i3lab-dashboard:18889"));
    }); 

builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter(options 
    => options.Endpoint = new Uri("http://i3lab-dashboard:18889")));


var app = builder.Build();

 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ClearDbContextMigrations();
    app.ApplyWorkContextMigrations();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
