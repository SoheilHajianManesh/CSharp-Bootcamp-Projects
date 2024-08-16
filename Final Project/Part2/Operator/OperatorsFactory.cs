namespace Part2.Operator;

public static class OperatorsFactory<T>
{
    public static IQueryable<T>? CreateAndApplyOperation(string operationType, IQueryable<T> table,
        List<string>? columns, string? column, string? choice, string? value)
    {
        try
        {
            DynamicOperator<T> operation = operationType switch
            {
                "Filter" => new Filter<T>(table),
                "Project" => new Project<T>(table),
                "Sort" => new Sort<T>(table),
                _ => throw new Exception("Invalid Operation select")
            };
            return operation.Apply(columns, column, choice, value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}