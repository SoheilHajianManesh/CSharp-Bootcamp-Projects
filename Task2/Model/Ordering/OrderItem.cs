using Model.Catalog;

namespace Model.Ordering;

public class OrderItem
{
    public long ID { get; private init; }
    public long OrderID { get; private init; }
    public Book Book { get; private init; }
    public int Quantity { get; set; }
    public decimal Price { get; private init; }
    public OrderItem(long id,long orderId, Book book, int quantity)
    {
        ID = id;
        OrderID = orderId;
        Book = book;
        Quantity = quantity;
        Price = book.Price * Quantity;
    }
}