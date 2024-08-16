using System.Linq.Expressions;

namespace Part2.Operator;

public class Project<T> : DynamicOperator<T>
{
    public Project(IQueryable<T> table) : base(table)
    {
    }

    protected override void ShowColumns()
    {
        Console.WriteLine("Select columns you want to project: (e.g. 1,2,3)");
        base.ShowColumns();
    }

    protected override List<string> SelectColumns()
    {
        ShowColumns();
        var selectedColumnNumber = Console.ReadLine()!.Split(',').Select(int.Parse).ToList();
        var selectedColumns = selectedColumnNumber.Select(i => Properties[i - 1]).ToList();
        Console.WriteLine($"You selected: {string.Join(", ", selectedColumns)}");
        return selectedColumns;
    }

    private LambdaExpression MakeExpressionTree(List<string> selectedColumn)
    {
        var parameter = Expression.Parameter(typeof(T), "source");
        var propertyList = selectedColumn.Select(column => Expression.Property(parameter, column));
        var memberInit = Expression.MemberInit(
            Expression.New(typeof(T)),
            propertyList.Select(p => Expression.Bind(p.Member, p))
        );
        var lambda = Expression.Lambda(memberInit, parameter);
        return lambda;
    }

    public override IQueryable<T>? Apply(List<string>? columns = null, string? column = null, string? choice = null,
        string? constantValue = null)
    {
        columns ??= SelectColumns();
        var lambdaExpr = MakeExpressionTree(columns);
        var method = GetMethod("Select", lambdaExpr);
        return InvokeMethod(method, lambdaExpr);
    }
}