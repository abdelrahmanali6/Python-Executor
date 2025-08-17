
public class ScriptResponse
{
    public required string ExecutionId { get; init; }
    public string Output { get; init; }
    public bool IsSuccess { get; init; }
    public TimeSpan Duration { get; init; }
    public List<ScriptVariable> OutputVariables { get; init; } = new List<ScriptVariable>();
}