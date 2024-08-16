namespace Model.Catalog;

public abstract class Book
{
    public long ID { get; private init; }
    public string Title { get; init; }
    public decimal Price { get; private set; }
    public string ISBN { get; private set; }
    protected Book(long id,string title,decimal price,string isbn)
    {
        ID = id;
        Title = title;
        Price = price;
        ISBN = isbn;
    }
    public void ChangePrice(decimal price)
    {
        Price = price;
    }
}