using System.Collections;

namespace Bonus;

public class Table<T> : IEnumerable<T>
{
    private List<T> _records { get; init; } = new List<T>();

    public Table()
    {
    }
    public Table(List<T> records)
    {
        _records = records;
    }
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var record in _records)
        {
            yield return record;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

