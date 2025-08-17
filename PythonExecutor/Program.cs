var builder = WebApplication.CreateBuilder(args);

// Essential services
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

// Service Discovery (if needed)
// builder.Services.AddServiceDiscovery();

var app = builder.Build();
app.MapGet("/", () => "Hello from Aspire API!");
// Minimal endpoints
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();