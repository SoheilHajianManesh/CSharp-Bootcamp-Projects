namespace Model.Catalog;

public class AudioBook : Book
{
    public int TotalMinutes { get; private init; }
    public AudioBook(int totalMinutes, long id, string title, decimal price, string isbn) : base(id, title, price, isbn)
    {
        TotalMinutes = totalMinutes;
    }
}