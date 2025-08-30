using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/scripts")]
public sealed class ScriptController : ControllerBase
{
    private readonly IScriptRunner _runner;
    private readonly ILogger<ScriptController> _logger;

    public ScriptController(IScriptRunner runner, ILogger<ScriptController> logger)
    {
        _runner = runner;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ScriptResponse>> Execute([FromBody] ScriptRequest request)
    {
        var response = await _runner.ExecuteAsync(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
        [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "API is working!" });
    }
}