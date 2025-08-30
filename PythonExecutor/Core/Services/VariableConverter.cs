public sealed class VariableConverter
{
    public string ToPython(ScriptVariable variable)
    {
        if (variable.Type == "string")
        {
            return $"\"{EscapeQuotes(variable.Value)}\"";
        }

        if (variable.Type == "bool")
        {
            return variable.Value.ToLowerInvariant();
        }

        return variable.Value;
    }
       public string GetTypeConversionLine(ScriptVariable variable, string pythonVarName)
    {
        var type = variable.Type.ToLower();
        if (type == "int" || type == "float" || type == "bool")
            return $"{variable.Variable} = {variable.Value}";  // no quotes
        else
            return $"{variable.Variable} = \"{variable.Value}\"";
    }
    public string EscapeQuotes(string input)
    {
        return input.Replace("\"", "\\\"");
    }
}