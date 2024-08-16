using FinalProject.Common;
using FinalProject.Common.Entities;
using FinalProject.Data;
using Part2.Operator;

namespace Part2.Test;

public class DynamicSortTest
{
    private static IDbContext _dbContext = new AppDbContext();
    private static void assert_that(IEnumerable<object>? firstSequence, IEnumerable<object>? secondSequence,
        string testTitle)
    {
        if(firstSequence == null && secondSequence == null)
            Console.WriteLine("Test passed - " + testTitle);
        else if(firstSequence == null || secondSequence == null)
            Console.WriteLine("Test failed - " + testTitle);
        else if (firstSequence.SequenceEqual(secondSequence))
            Console.WriteLine("Test passed - " + testTitle);
        else
            Console.WriteLine("Test failed - " + testTitle);
    }
    public static void run_all_tests()
    {
        sort_customers_by_first_name_in_ascending_order();
        sort_invoices_by_total_in_descending_order();
        sort_employee_by_birthdate_in_ascending_order();
        sort_tracks_by_name_in_descending_order();
    }

    private static void sort_customers_by_first_name_in_ascending_order()
    {
        var resultTableUsingDynamicSort = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Customer))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Sort", _dbContext.Customers, null, "FirstName", "Ascending", null})) as
            IEnumerable<Customer>;
        var resultTableUsingLinq = _dbContext.Customers.OrderBy(c => c.FirstName).AsEnumerable();
        assert_that(resultTableUsingDynamicSort, resultTableUsingLinq,
            "sort_customers_by_first_name_in_ascending_order");
    }
    private static void sort_invoices_by_total_in_descending_order()
    {
        var resultTableUsingDynamicSort = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Invoice))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Sort", _dbContext.Invoices, null, "Total", "Descending", null})) as
            IEnumerable<Invoice>;
        var resultTableUsingLinq = _dbContext.Invoices.OrderByDescending(i => i.Total).AsEnumerable();
        assert_that(resultTableUsingDynamicSort, resultTableUsingLinq, "sort_invoices_by_total_in_descending_order");
    }
    private static void sort_employee_by_birthdate_in_ascending_order()
    {
        var resultTableUsingDynamicSort = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Employee))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Sort", _dbContext.Employees, null, "BirthDate", "Ascending",null})) as
            IEnumerable<Employee>;
        var resultTableUsingLinq = _dbContext.Employees.OrderBy(e => e.BirthDate).AsEnumerable();
        assert_that(resultTableUsingDynamicSort, resultTableUsingLinq, "sort_employee_by_birthdate_in_ascending_order");
    }
    private static void sort_tracks_by_name_in_descending_order()
    {
        var resultTableUsingDynamicSort = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Track))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Sort", _dbContext.Tracks, null, "Name", "Descending", null})) as
            IEnumerable<Track>;
        var resultTableUsingLinq = _dbContext.Tracks.OrderByDescending(t => t.Name).AsEnumerable();
        assert_that(resultTableUsingDynamicSort, resultTableUsingLinq, "sort_tracks_by_name_in_descending_order");
    }
}