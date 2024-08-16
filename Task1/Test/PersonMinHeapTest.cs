using P1.Entity;
using P1.Structure;

namespace P1.Test;
public static class PersonMinHeapTest
{
    private static readonly List<Person> Persons = new List<Person>
    {
        new Person("ali", 20,1),
        new Person("reza", 22,2),
        new Person("javad", 18,3),
        new Person("hossein", 25,4),
        new Person("soheil", 10,5),
        new Person("bahar", 15,6),
        new Person("mohammad", 30,7),
        new Person("mohsen", 35,8)
    };
    private static MinHeap<Person> _heapPersons;
    private static void initialize_heap_from_persons_array(int count,int maxSize=8)
    {
        _heapPersons = new MinHeap<Person>(maxSize);
        _heapPersons.MakeHeapFrom(Persons.GetRange(0, count).ToArray());
    }
    private static void assert_that(string expected, string actual)
    {
        if (expected.Equals(actual))
            Console.WriteLine("Test passed - " + expected);
        else
            Console.WriteLine("Test failed");
    }
    public static void delete_from_empty_heap()
    {
        try
        {
            initialize_heap_from_persons_array(0);
            _heapPersons.Delete(Persons[0]);
            Console.WriteLine("Test failed");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Test passed - " + e.Message);
        }
    }
    public static void peek_from_empty_heap()
    {
        try
        {
            initialize_heap_from_persons_array(0);
            _heapPersons.Peek();
            Console.WriteLine("Test failed");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Test passed - " + e.Message);
        }
    }
    public static void insert_to_full_heap()
    {
        try
        {
            initialize_heap_from_persons_array(8);
            _heapPersons.Insert(new Person("sara", 8, 9));
            Console.WriteLine("Test failed");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Test passed - " + e.Message);
        }
    }
    public static void make_heap_from_array()
    {
        initialize_heap_from_persons_array(8); 
        assert_that("[soheil-10] [ali-20] [bahar-15] [hossein-25] [reza-22] [javad-18] [mohammad-30] [mohsen-35] ",
            _heapPersons.ToString());
    }
    public static void delete_root_from_heap()
    {
        initialize_heap_from_persons_array(8);
        _heapPersons.Delete(Persons[4]);
        assert_that("[bahar-15] [ali-20] [javad-18] [hossein-25] [reza-22] [mohsen-35] [mohammad-30] ", 
            _heapPersons.ToString());
    }
    public static void insert_person_that_smaller_than_root()
    {
        initialize_heap_from_persons_array(7);
        _heapPersons.Insert(new Person("sara", 8, 9));
        assert_that("[sara-8] [soheil-10] [bahar-15] [ali-20] [reza-22] [javad-18] [mohammad-30] [hossein-25] ", 
            _heapPersons.ToString());
    }
    public static void delete_two_persons_from_heap()
    {
        initialize_heap_from_persons_array(8);
        _heapPersons.Delete(Persons[0]);
        _heapPersons.Delete(Persons[5]);
        assert_that("[soheil-10] [reza-22] [javad-18] [hossein-25] [mohsen-35] [mohammad-30] ", 
            _heapPersons.ToString());
    }
    public static void insert_two_persons_to_heap()
    {
        initialize_heap_from_persons_array(6);
        _heapPersons.Insert(new Person("zahra", 11, 9));
        _heapPersons.Insert(new Person("sara", 18, 10));
        assert_that("[soheil-10] [sara-18] [zahra-11] [ali-20] [reza-22] [javad-18] [bahar-15] [hossein-25] ",
            _heapPersons.ToString());
    }
}

