public sealed class ScriptGenerator
{
    private readonly VariableConverter _converter;
    public ScriptGenerator(VariableConverter converter)
    {
        _converter = converter;
    }
    public string Generate(ExecutionScript execution)
    {
        if (execution.Variables.Any())
        {
            return execution.script;
        }

        var declarations = execution.Variables.Select(v => $"{v.Variable} = {_converter.ToPython(v)}");
        var conversions = execution.Variables.Select(v => _converter.GetTypeConversionLine(v, v.Variable)).Where(c => !string.IsNullOrEmpty(c));
        return $"{string.Join("\n", declarations)}\n{execution.script}";
    }
}