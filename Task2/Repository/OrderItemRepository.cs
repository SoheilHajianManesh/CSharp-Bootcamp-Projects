using Model.Ordering;

namespace Task2.Repository;

public class OrderItemRepository : IRepository<OrderItem,long>
{
    private static OrderItemRepository _instance;
    private List<OrderItem> _orderItems;
    private OrderItemRepository()
    {
        _orderItems = new List<OrderItem>();
    }
    public static OrderItemRepository getInstance()
    {
        if (_instance == null)
            _instance = new OrderItemRepository();
        return _instance;
    }
    public OrderItem GetById(long id)
    {
        return _orderItems.FirstOrDefault(x => x.ID == id);
    }
    public List<OrderItem> GetAll()
    {
        return _orderItems;
    }
    public void Add(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }
    public void AddRange(IEnumerable<OrderItem> orderItems)
    {
        _orderItems.AddRange(orderItems);
    }
    public void Update(OrderItem orderItem)
    {
        var existingOrderItem = _orderItems.FirstOrDefault(x => x.ID == orderItem.ID);
        if (existingOrderItem == null)
        {
            throw new InvalidOperationException("OrderItem not found");
        }
        existingOrderItem = orderItem;
    }
    public void Remove(long id)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.ID == id);
        if (orderItem == null)
        {
            throw new InvalidOperationException("OrderItem not found");
        }
        _orderItems.Remove(orderItem);
    }
}