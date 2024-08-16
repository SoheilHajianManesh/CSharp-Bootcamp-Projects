using System.Linq.Expressions;

namespace Part2.Operator;

public class Sort<T> : DynamicOperator<T>
{
    public Sort(IQueryable<T> table) : base(table) { }
    protected override void ShowColumns()
    {
        Console.WriteLine("Select a column number to sort based on that: (e.g. 1)");
        base.ShowColumns();
    }
    protected override void ShowChoices()
    {
        Console.WriteLine("Select the sort order number: (e.g. 1)");
        Console.WriteLine("     1. Ascending");
        Console.WriteLine("     2. Descending");
    }
    private string SelectOrder(string? choice)
    {
        string selectedOrder;
        if (choice==null)
        {
            ShowChoices();
            var orderSelection = int.Parse(Console.ReadLine()!);
            if (orderSelection != 1 && orderSelection != 2)
            {
                throw new Exception("Invalid sort operation");
            }
            selectedOrder = orderSelection == 1 ? "OrderBy" : "OrderByDescending";
            Console.WriteLine($"You selected: {selectedOrder}");
        }
        else selectedOrder = choice == "Ascending" ? "OrderBy" : "OrderByDescending";
        return selectedOrder;
    }
    private LambdaExpression MakeExpressionTree(string selectedColumn)
    {
        var parameter = Expression.Parameter(typeof(T), "source");
        var property = Expression.Property(parameter, selectedColumn);
        var lambda = Expression.Lambda(property, parameter);
        return lambda;
    }

    public override IQueryable<T>? Apply(List<string>? columns = null, string? column = null, string? choice = null,
        string? constantValue = null)
    {
        column ??= SelectColumns()[0];
        var order = SelectOrder(choice);
        var lambdaExpr = MakeExpressionTree(column);
        var method = GetMethod(order, lambdaExpr);
        return InvokeMethod(method, lambdaExpr);
    }
}