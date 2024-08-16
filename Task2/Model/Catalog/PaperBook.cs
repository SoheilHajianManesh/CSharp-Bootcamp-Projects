namespace Model.Catalog;

public class PaperBook : Book
{
    public decimal TotalPages { get; private init; }
    public PaperBook(decimal totalPages,long id,string title,decimal price,string isbn) 
        : base(id,title,price,isbn)
    {
        TotalPages = totalPages;
    }
}