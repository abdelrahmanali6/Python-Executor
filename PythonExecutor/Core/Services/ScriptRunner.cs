using System.Diagnostics;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
namespace PythonScriptService.Services; 
public sealed class ScriptRunner : IScriptRunner, IDisposable
{
    private readonly ScriptGenerator? _generator;
    private readonly ScriptEngine _engine;
    private readonly ILogger<ScriptRunner> _logger;

    public ScriptRunner(ScriptGenerator generator,ILogger<ScriptRunner> logger)
    {
        _generator = generator;
        _logger = logger;
        _engine = Python.CreateEngine();
    }

    public async Task<ScriptResponse> ExecuteAsync(ScriptRequest request)
    {

        var stopwatch = Stopwatch.StartNew();
        var execution = new ExecutionScript
        {
            script = request.Code,
            Variables = request.Variables
        };

        try
        {
            var script = _generator.Generate(execution);
             var result = await Task.Run(() =>
        {
            var scope = _engine.CreateScope();

            if (execution.Variables != null)
            {
                foreach (var kvp in execution.Variables)
                {
                    scope.SetVariable(kvp.Variable, kvp.Value);
                }
            }

            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms) { AutoFlush = true };
            _engine.Runtime.IO.SetOutput(ms, writer);

            var execResult = _engine.Execute(script, scope);

            ms.Position = 0;
            var output = new StreamReader(ms).ReadToEnd();
            
            if (execResult != null)
                output += execResult.ToString();

            return output;
        });
            return new ScriptResponse
            {
                ExecutionId = execution.Id.ToString(),
                Output = result?.ToString() ?? string.Empty,
                IsSuccess = true,
                Duration = stopwatch.Elapsed
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Execution failed");
            return new ScriptResponse
            {
                ExecutionId = execution.Id.ToString(),
                Output = ex.Message,
                IsSuccess = false,
                Duration = stopwatch.Elapsed
            };
        }
    }

    public void Dispose() => _engine.Runtime.Shutdown();
}
