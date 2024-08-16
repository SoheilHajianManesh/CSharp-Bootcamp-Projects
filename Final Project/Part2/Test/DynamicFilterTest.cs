using FinalProject.Common;
using FinalProject.Common.Entities;
using FinalProject.Data;
using Part2.Operator;

namespace Part2.Test;

public static class DynamicFilterTest
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
        customers_with_german_nationality();
        employee_with_birthdate_after_1995();
        employees_that_do_not_have_manager_with_id_1();
        invoice_lines_with_unit_price_less_than_2_dollars();
        customers_with_first_name_contains_o();
        genres_with_name_starts_with_Po();
        tracks_ends_with_s();
    }
    private static void customers_with_german_nationality()
    {
        var resultTableUsingDynamicFilter = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Customer))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Filter", _dbContext.Customers, null, "Country", "Equal", "Germany"})) as
            IEnumerable<Customer>;
        var resultTableUsingLinq = _dbContext.Customers.Where(c => c.Country == "Germany").AsEnumerable();
        assert_that(resultTableUsingDynamicFilter, resultTableUsingLinq, "customers_with_german_national");
    }
    private static void employee_with_birthdate_after_1995()
    {
        var resultTableUsingDynamicFilter = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Employee))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null,
                    new object[]
                        {"Filter", _dbContext.Employees, null, "BirthDate", "GreaterThan", "1995-01-01"})) as
            IEnumerable<Employee>;
        var resultTableUsingLinq =
            _dbContext.Employees.Where(e => e.BirthDate > new DateTime(1980, 1, 1)).AsEnumerable();
        assert_that(resultTableUsingDynamicFilter, resultTableUsingLinq, "employee_with_birthdate_after_1995");
    }
    private static void employees_that_do_not_have_manager_with_id_1()
    {
        var resultTableUsingDynamicFilter = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Employee))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Filter", _dbContext.Employees, null, "ReportsTo", "NotEqual", "1"})) as
            IEnumerable<Employee>;
        var resultTableUsingLinq = _dbContext.Employees.Where(e => e.ReportsTo != 1).AsEnumerable();
        assert_that(resultTableUsingDynamicFilter, resultTableUsingLinq,
            "employees_that_do_not_have_manager_with_id_1");
    }
    private static void invoice_lines_with_unit_price_less_than_2_dollars()
    {
        var resultTableUsingDynamicFilter = (typeof(OperatorsFactory<>).MakeGenericType(typeof(InvoiceLine))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Filter", _dbContext.InvoiceLines, null, "UnitPrice", "LessThan", "2"})) as
            IEnumerable<InvoiceLine>;
        var resultTableUsingLinq = _dbContext.InvoiceLines.Where(il => il.UnitPrice < 2).AsEnumerable();
        assert_that(resultTableUsingDynamicFilter, resultTableUsingLinq, "invoice_lines_with_unit_price_less_than_2_dollars");
    }
    private static void customers_with_first_name_contains_o()
    {
        var resultTableUsingDynamicFilter = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Customer))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Filter", _dbContext.Customers, null, "FirstName", "Contains", "o"})) as
            IEnumerable<Customer>;
        var resultTableUsingLinq = _dbContext.Customers.Where(c => c.FirstName.Contains("o")).AsEnumerable();
        assert_that(resultTableUsingDynamicFilter, resultTableUsingLinq, "customers_with_first_name_contains_o");
    }
    private static void genres_with_name_starts_with_Po()
    {
        var resultTableUsingDynamicFilter = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Genre))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Filter", _dbContext.Genres, null, "Name", "StartsWith", "Po"})) as
            IEnumerable<Genre>;
        var resultTableUsingLinq = _dbContext.Genres.Where(g => g.Name.StartsWith("Po")).AsEnumerable();
        assert_that(resultTableUsingDynamicFilter, resultTableUsingLinq, "genres_with_name_start_with_Po");
    }
    private static void tracks_ends_with_s()
    {
        var resultTableUsingDynamicFilter = (typeof(OperatorsFactory<>).MakeGenericType(typeof(Track))
                .GetMethod("CreateAndApplyOperation")!
                .Invoke(null, new object[] {"Filter", _dbContext.Tracks, null, "Name", "EndsWith", "s"})) as
            IEnumerable<Track>;
        var resultTableUsingLinq = _dbContext.Tracks.Where(t => t.Name.EndsWith("s")).AsEnumerable();
        assert_that(resultTableUsingDynamicFilter, resultTableUsingLinq, "tracks_ends_with_s");
    }
}