namespace P1.Service;

public interface IComparable<T>
{
    public bool SmallerThan(T toCompare);
}