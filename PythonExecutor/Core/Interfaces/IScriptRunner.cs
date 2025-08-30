public interface IScriptRunner
{
    Task<ScriptResponse> ExecuteAsync(ScriptRequest request);
}