using Model.Ordering;

namespace Task2.Repository;

public class OrderRepository : IRepository<Order,long>
{
    private static OrderRepository _instance;
    private List<Order> _orders;
    private OrderRepository()
    {
        _orders = new List<Order>();
    }
    public static OrderRepository getInstance()
    {
        if (_instance == null)
            _instance = new OrderRepository();
        return _instance;
    }
    public Order GetById(long id)
    {
        return _orders.FirstOrDefault(x => x.ID == id);
    }
    public List<Order> GetAll()
    {
        return _orders;
    }
    public void Add(Order order)
    {
        _orders.Add(order);
    }
    public void AddRange(IEnumerable<Order> orders)
    {
        _orders.AddRange(orders);
    }
    public void Update(Order order)
    {
        var existingOrder = _orders.FirstOrDefault(x => x.ID == order.ID);
        if (existingOrder == null)
        {
            throw new InvalidOperationException("Order not found");
        }
        existingOrder = order;
    }
    public void Remove(long id)
    {
        var order = _orders.FirstOrDefault(x => x.ID == id);
        if (order == null)
        {
            throw new InvalidOperationException("Order not found");
        }
        _orders.Remove(order);
    }
}