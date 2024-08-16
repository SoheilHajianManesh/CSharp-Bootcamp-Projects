using System.Linq.Expressions;
using System.Reflection;

namespace Part2.Operator;

public abstract class DynamicOperator<T>
{
    private IQueryable<T> _table { get ; init; }
    protected List<string> Properties { get; init; }
    public DynamicOperator(IQueryable<T> table)
    {
        _table = table;
        Properties = typeof(T).GetProperties().Select(p => p.Name).ToList();
    }
    protected virtual void ShowColumns()
    {
        for (var i = 0; i < Properties.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Properties[i]}");
        }
    }
    protected virtual List<string> SelectColumns()
    {
        ShowColumns();
        var columnSelected = int.Parse(Console.ReadLine()!) - 1;
        if (columnSelected < 0 || columnSelected >= Properties.Count)
        {
            throw new Exception("Invalid column select");
        }
        var selectedColumn = Properties[columnSelected];
        Console.WriteLine($"You selected: {selectedColumn}");
        return new List<string>(){selectedColumn};
    }
    protected virtual void ShowChoices() { }
    protected virtual MethodInfo GetMethod(string methodName,LambdaExpression lambda)
    {
        return typeof(Queryable).
            GetMethods().
            First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T),lambda.Body.Type);
    }
    protected IQueryable<T>? InvokeMethod(MethodInfo methodInfo,LambdaExpression lambda)
    {
        var query = methodInfo.Invoke(null, new object[] { _table, lambda });
        return query as IQueryable<T>;
    }

    public abstract IQueryable<T>? Apply(List<string>? columns = null, string? column = null, string? choice = null,
        string? constantValue = null);
}