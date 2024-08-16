namespace Model.Catalog;

public class EBook : Book
{
    public decimal SizeInBytes { get; private init; }

    public EBook(decimal sizeInBytes ,long id ,string title ,decimal price,string isbn) 
        : base(id ,title ,price,isbn)
    {
        SizeInBytes = sizeInBytes;
    }
}