using System.Text;
using P1.Service;

namespace P1.Structure;

public class MinHeap<T> where T: Service.IComparable<T>, IPrintable
{
    private T[] Heap { get; }
    private int Size {get;  set; }
    private readonly int _maxSize;
    public MinHeap(int maxSize)
    {
        _maxSize = maxSize;
        Heap = new T[maxSize];
        Size = 0;
    }
    public void Insert(T toInsert)
    {
        if (Size == _maxSize)
            throw new InvalidOperationException("Maximum size reached.");
        Heap[Size++] = toInsert;
        for (var i = Size / 2 - 1; i >= 0; i--)
            Heapify(i);
    }
    public void Delete(T toDelete)
    {
        if (Size == 0)
            throw new InvalidOperationException("Heap is empty.");
        var deletedIdx = Search(toDelete);
        Heap[deletedIdx] = Heap[--Size];
        Heapify(deletedIdx);
    }
    public T Peek()
    {
        if (Size == 0)
            throw new InvalidOperationException("Heap is empty.");
        return Heap[0];
    }
    public void MakeHeapFrom(T[] arr)
    {
        if(arr.Length> _maxSize)
            throw new InvalidOperationException("Maximum size reached.");
        Array.Copy(arr, Heap, arr.Length);
        Size = arr.Length;
        for (var i = Size / 2 - 1; i >= 0; i--)
            Heapify(i);
    }
    private void Swap(int firstIdx, int secondIdx)
    {
        (Heap[firstIdx], Heap[secondIdx]) = (Heap[secondIdx], Heap[firstIdx]);
    }
    private void Heapify(int i)
    {
        var smallest = i;
        var left = 2 * i + 1;
        var right = 2 * i + 2;
        if(left < Size && Heap[left].SmallerThan(Heap[smallest]))
            smallest = left;
        if(right < Size && Heap[right].SmallerThan(Heap[smallest]))
            smallest = right;
        if(smallest != i)
        {
            Swap(i, smallest);
            Heapify(smallest);
        }
    }
    private int Search(T toSearch)
    {
        for (var i = 0; i < Size; i++)
        {
            if (Heap[i].Equals(toSearch))
                return i;
        }
        throw new InvalidOperationException("Element not founded.");
    }
    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0 ; i < Size ; i++)
        {
            sb.Append(Heap[i].Print() + " ");
        }
        return sb.ToString();
    }
}