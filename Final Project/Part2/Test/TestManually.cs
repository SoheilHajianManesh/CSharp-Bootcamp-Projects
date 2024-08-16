using FinalProject.Common;
using FinalProject.Data;
using Part2.Operator;

namespace Part2.Test;

public static class TestManually
{
    private static IDbContext dbContext = new AppDbContext();

    private static Dictionary<string, IQueryable<object>> _tables = new Dictionary<string, IQueryable<object>>()
    {
        {"Album", dbContext.Albums},
        {"Artist", dbContext.Artists},
        {"Customer", dbContext.Customers},
        {"Employee", dbContext.Employees},
        {"Genre", dbContext.Genres},
        {"Invoice", dbContext.Invoices},
        {"InvoiceLine", dbContext.InvoiceLines},
        {"MediaType", dbContext.MediaTypes},
        {"Playlist", dbContext.Playlists},
        {"Track", dbContext.Tracks}
    };

    public static void TestUsingTerminal()
    {
        Console.WriteLine("What operation do you want to perform? (Filter, Project, Sort)");
        var operation = Console.ReadLine();
        if (operation == null)
        {
            Console.WriteLine("Invalid operation select");
            return;
        }

        var selectTable = SelectTable();
        var operatorFactoryType = typeof(OperatorsFactory<>).MakeGenericType(_tables[selectTable].ElementType);
        var operatorMethod = operatorFactoryType.GetMethod("CreateAndApplyOperation");
        var result =
            operatorMethod!.Invoke(null, new object[] {operation, _tables[selectTable], null, null, null, null});
        ShowResultTable(result as IEnumerable<object>);
    }


    private static void ShowTables()
    {
        Console.WriteLine("Select a table number: (e.g. 3)");
        for (int i = 0; i < _tables.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_tables.Keys.ElementAt(i)}");
        }
    }

    private static string SelectTable()
    {
        ShowTables();
        var selectedTableNumber = int.Parse(Console.ReadLine()!) - 1;
        if (selectedTableNumber < 0 || selectedTableNumber >= _tables.Count)
        {
            Console.WriteLine("Invalid selection.");
            return null;
        }

        var selectedTable = _tables.Keys.ElementAt(selectedTableNumber);
        Console.WriteLine($"You selected: {selectedTable}");
        return selectedTable;
    }

    private static void ShowResultTable(IEnumerable<object> filteredTable)
    {
        foreach (var tuple in filteredTable)
        {
            var properties = tuple.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(tuple);
                Console.WriteLine($"{property.Name}: {value}");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}