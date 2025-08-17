var builder = DistributedApplication.CreateBuilder(args);
var apiService = builder.AddProject<Projects.PythonExecutor>("PythonExecutor");
builder.Build().Run();
