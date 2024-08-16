using System.Text;
using P1.Service;

namespace P1.Entity;

public class Person : Service.IComparable<Person>,IPrintable
{
    private string Name { get; init; } 
    private int Age { get; set; }
    public Person(string name, int age, int id)
    {
        Name = name;
        Age = age;
    }
    public string Print()
    {
        var sb = new StringBuilder();
        sb.Append("[" + Name + "-" + Age + "]");
        return sb.ToString();
    }
    public bool SmallerThan(Person toCompare)
    {
        return this.Age < toCompare.Age;
    }
}