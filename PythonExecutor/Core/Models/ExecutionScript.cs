public class ExecutionScript
{
    public Guid Id { get; } = Guid.NewGuid();
    public string script { get; set; }
    public DateTimeOffset StartTime { get; } = DateTimeOffset.UtcNow;
    public List<ScriptVariable> Variables { get; init; } = new List<ScriptVariable>();
    
}