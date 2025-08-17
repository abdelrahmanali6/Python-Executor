using System.ComponentModel.DataAnnotations;

public class ScriptRequest
{
    public required string Code { get; init; }
    public int? TimeoutSeconds { get; init; } = 5;
    public List<ScriptVariable>? Variables { get; init; } = new List<ScriptVariable>();
}