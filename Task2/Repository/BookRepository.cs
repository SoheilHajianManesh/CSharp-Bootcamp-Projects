using Model.Catalog;
namespace Task2.Repository;

public class BookRepository: IRepository<Book,long>
{
    private static BookRepository _instance;
    private List<Book> _books { get; set; }
    private BookRepository()
    {
        _books = new List<Book>();
    }
    public static BookRepository getInstance()
    {
        if (_instance == null)
            _instance = new BookRepository();
        return _instance;
    }
    public Book GetById(long id)
    {
        return _books.FirstOrDefault(x => x.ID == id);
    }
    public List<Book> GetAll()
    {
        return _books;
    }
    public void Add(Book book)
    {
        _books.Add(book);
    }
    public void AddRange(IEnumerable<Book> books)
    {
        _books.AddRange(books);
    }
    public void Update(Book book)
    {
        var existingBook = _books.FirstOrDefault(x => x.ID == book.ID);
        if (existingBook == null)
        {
            throw new InvalidOperationException("Book not found");
        }
        existingBook = book;
    }
    public void Remove(long id)
    {
        var book = _books.FirstOrDefault(x => x.ID == id);
        if (book == null)
        {
            throw new InvalidOperationException("Book not found");
        }
        _books.Remove(book);
    }
}