namespace Bonus.Test;

public static class LinqOperationsTest
{
    private static Table<int> _collection = new Table<int>(new List<int>(){4,5,2,1,10,9,7,3,6,8});
    private static void assert_that(string expected, string actual)
    {
        if (expected.Equals(actual))
            Console.WriteLine("Test passed - " + expected);
        else
            Console.WriteLine("Test failed");
    }
    public static void print_entire_collection()
    {
        string result = "";
        foreach (var item in _collection)
        {
            result += item + "  ";
        }
        assert_that("4  5  2  1  10  9  7  3  6  8  ", result);
    }
    public static void print_odd_number_less_that_5()
    {
        string result = "";
        var oddNumbersLessThanFive = _collection.Where(a => a < 5 && a % 2 == 1);
        foreach(var number in oddNumbersLessThanFive)
        {
            result += number + "  ";
        }
        assert_that("1  3  ", result);
    }
    public static void take_first_three_number_after_order_ascending()
    {
        string result = "";
        var takeThreeNumberAfterOrderingCollection = _collection.OrderBy(a => a).Take(3);
        foreach(var number in takeThreeNumberAfterOrderingCollection)
        {
            result += number + "  ";
        }
        assert_that("1  2  3  ", result);
    }
    public static void print_power_of_two_of_odd_numbers_in_descending_order()
    {
        string result = "";
        var powerOfTwoOfOddNumbers = _collection.
            Where(a => a % 2 == 1).
            Select(a => a * a).
            OrderDescending();
        foreach(var number in powerOfTwoOfOddNumbers)
        {
            result += number + "  ";
        }
        assert_that("81  49  25  9  1  ", result);
    }
}