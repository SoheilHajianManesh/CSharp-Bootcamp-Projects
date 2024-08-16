using System.Linq.Expressions;
using System.Reflection;

namespace Part2.Operator;

public class Filter<T> : DynamicOperator<T>
{
    private readonly List<string> _operators = new List<string>()
    {
        "Equal", "NotEqual", "GreaterThan", "LessThan", "GreaterThanOrEqual", "LessThanOrEqual", "Contains",
        "StartsWith", "EndsWith"
    };

    public Filter(IQueryable<T> table) : base(table)
    {
    }

    protected override void ShowColumns()
    {
        Console.WriteLine("Select a column number to filter based on that: (e.g. 1)");
        base.ShowColumns();
    }

    protected override void ShowChoices()
    {
        Console.WriteLine("Select a logical operator number:(e.g. 3)");
        for (int i = 0; i < _operators.Count; i++)
        {
            Console.WriteLine($"    {i + 1}. {_operators[i]}");
        }
    }

    private string SelectOperator()
    {
        ShowChoices();
        int operatorSelection = int.Parse(Console.ReadLine()!) - 1;
        if (operatorSelection < 0 || operatorSelection >= _operators.Count)
        {
            throw new Exception("Invalid operator select");
        }

        var selectedOperator = _operators[operatorSelection];
        Console.WriteLine($"You selected: {selectedOperator}");
        return selectedOperator;
    }

    private string GetConstantValue()
    {
        Console.WriteLine("Enter your constant value:");
        string? value = Console.ReadLine();
        if (value == null)
            throw new Exception("Invalid value");
        return value;
    }

    private Expression CreateConstantExpression(Type constantType, string constantValue)
    {
        var underlyingType = Nullable.GetUnderlyingType(constantType) ?? constantType;
        var convertedValue = constantType == typeof(DateTime?)
            ? DateTime.Parse(constantValue)
            : Convert.ChangeType(constantValue, underlyingType);
        var valueExpression = Expression.Constant(convertedValue, underlyingType);
        Expression convertedValueExpression = constantType != underlyingType
            ? Expression.Convert(valueExpression, constantType)
            : valueExpression;
        return convertedValueExpression;
    }

    private Expression CreateOprationExpresion(string selectedOperator, MemberExpression property,
        Expression constantExpression)
    {
        if ((selectedOperator.Equals("Contains") || selectedOperator.Equals("StartsWith") ||
             selectedOperator.Equals("EndsWith")) && property.Type != typeof(string))
        {
            throw new Exception("Cannot apply this operator on non-string types");
        }

        return selectedOperator switch
        {
            "Equal" => Expression.Equal(property, constantExpression),
            "NotEqual" => Expression.NotEqual(property, constantExpression),
            "GreaterThan" => Expression.GreaterThan(property, constantExpression),
            "LessThan" => Expression.LessThan(property, constantExpression),
            "GreaterThanOrEqual" => Expression.GreaterThanOrEqual(property, constantExpression),
            "LessThanOrEqual" => Expression.LessThanOrEqual(property, constantExpression),
            "Contains" => Expression.Call(property, typeof(string).GetMethod("Contains",new[] {typeof(string)})!,
                constantExpression),
            "StartsWith" => Expression.Call(property, typeof(string).GetMethod(selectedOperator,new[] {typeof(string)})!,
                constantExpression),
            "EndsWith" => Expression.Call(property, typeof(string).GetMethod(selectedOperator,new []{typeof(string)})!,
                constantExpression),
            _ => throw new NotImplementedException()
        };
    }

    private LambdaExpression MakeExpressionTree(string selectedColumn, string selectedOperator, string constantValue)
    {
        var parameter = Expression.Parameter(typeof(T), "source");
        var property = Expression.Property(parameter, selectedColumn);
        var valueExpression = CreateConstantExpression(property.Type, constantValue);
        var operation = CreateOprationExpresion(selectedOperator, property, valueExpression);
        var lambda = Expression.Lambda(operation, parameter);
        return lambda;
    }


    protected override MethodInfo GetMethod(string methodName, LambdaExpression lambda)
    {
        return typeof(Queryable).GetMethods().First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T));
    }

    public override IQueryable<T>? Apply(List<string>? columns = null, string? column = null, string? choice = null,
        string? constantValue = null)
    {
        column ??= SelectColumns()[0];
        choice ??= SelectOperator();
        constantValue ??= GetConstantValue();
        var lambdaExpr = MakeExpressionTree(column, choice, constantValue);
        var method = GetMethod("Where", lambdaExpr);
        return InvokeMethod(method, lambdaExpr);
    }
}