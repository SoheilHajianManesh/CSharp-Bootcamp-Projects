using Task2.Repository;
namespace Task2.Service;

public class OrderItemMapper : IMapper<DataTransferObjects.OrderItem,Model.Ordering.OrderItem>
{
    private BookRepository _bookRepository { get; init; } = BookRepository.getInstance();
    private OrderRepository _orderRepository { get; init; } = OrderRepository.getInstance();
    
    public List<Model.Ordering.OrderItem> Map(List<DataTransferObjects.OrderItem> sourceObjs)
    {
        var orderItems = new List<Model.Ordering.OrderItem>();
        foreach (var obj in sourceObjs)
        {
            var book= _bookRepository.GetById(obj.BookID);
            var orderItem = new Model.Ordering.OrderItem(obj.ID, obj.OrderID, book, obj.Quantity);
            _orderRepository.GetById(orderItem.OrderID).AddItem(orderItem);
            orderItems.Add(orderItem);
        }
        return orderItems;
    }
}