using Model.Catalog;

namespace Model.Ordering;

public class Order
{
    public long ID { get; private init; }
    public DateTime DateTime { get; private init; }
    public decimal TotalPrice { get; private set; }
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public Order(long id,DateTime dateTime)
    {
        ID = id;
        DateTime = dateTime;
        TotalPrice = 0;
    }
    
    public void AddItem(OrderItem orderItem)
    {
        if (_items.Count > 20)
            throw new InvalidOperationException("Can't add more than 5 items to an order.");
        _items.Add(orderItem);
        TotalPrice += orderItem.Price;
    }
}

// public enum PaymentMethod
// {
//     Online,
//     Receipt,
// }