namespace Task2.Repository;

public interface IRepository<TItem,TKey>
{ 
    public List<TItem> GetAll();
    public TItem GetById(TKey id);
    public void Add(TItem item);
    public void AddRange(IEnumerable<TItem> items);
    public void Update(TItem item);
    public void Remove(TKey id);
}