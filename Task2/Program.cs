using Task2.Service;
using Task2.Repository;
using Task2.Test;
using AudioBookDTO = DataTransferObjects.AudioBook;
using EBookDTO = DataTransferObjects.EBook;
using PaperBookDTO = DataTransferObjects.PaperBook;
using OrderDTO = DataTransferObjects.Order;
using OrderItemDTO = DataTransferObjects.OrderItem;

class Program
{
    static void Main(string[] args)
    {
        ReadCsvs();
        MapDtoToModel();
        FinalModelTest.check_total_results_of_orders_equal_with_dataset();
    }
    
    private static readonly CsvReaderService _csvReader = new CsvReaderService();
    private static readonly MapperFactory _mapperFactory = new MapperFactory();
    
    private static readonly BookRepository _bookRepository = BookRepository.getInstance();
    private static readonly OrderRepository _orderRepository = OrderRepository.getInstance();
    private static readonly OrderItemRepository _orderItemRepository = OrderItemRepository.getInstance();
    
    private static List<EBookDTO>? _eBooksDtos;
    private static List<AudioBookDTO>? _audioBooksDtos;
    private static List<PaperBookDTO>? _paperBooksDtos;
    private static List<OrderDTO>? _orderDtos;
    private static List<OrderItemDTO>? _orderItemDtos;
    
    static void ReadCsvs()
    {
        _eBooksDtos = _csvReader.ReadFromCsv<EBookDTO>("C:/Users/Lenovo/Desktop/C#-codes/bootCamp/Task2/Datasets/EBooks.csv");
        _audioBooksDtos = _csvReader.ReadFromCsv<AudioBookDTO>("C:/Users/Lenovo/Desktop/C#-codes/bootCamp/Task2/Datasets/AudioBooks.csv");
        _paperBooksDtos = _csvReader.ReadFromCsv<PaperBookDTO>("C:/Users/Lenovo/Desktop/C#-codes/bootCamp/Task2/Datasets/PaperBooks.csv");
        _orderDtos = _csvReader.ReadFromCsv<OrderDTO>("C:/Users/Lenovo/Desktop/C#-codes/bootCamp/Task2/Datasets/Orders.csv");
        _orderItemDtos = _csvReader.ReadFromCsv<OrderItemDTO>("C:/Users/Lenovo/Desktop/C#-codes/bootCamp/Task2/Datasets/OrderItems.csv");
    }
    static void MapDtoToModel()
    {
        _bookRepository.AddRange(_mapperFactory.GetMapper<EBookDTO,Model.Catalog.EBook>().Map(_eBooksDtos));
        _bookRepository.AddRange(_mapperFactory.GetMapper<AudioBookDTO,Model.Catalog.AudioBook>().Map(_audioBooksDtos));
        _bookRepository.AddRange(_mapperFactory.GetMapper<PaperBookDTO,Model.Catalog.PaperBook>().Map(_paperBooksDtos));
        _orderRepository.AddRange(_mapperFactory.GetMapper<OrderDTO,Model.Ordering.Order>().Map(_orderDtos));
        _orderItemRepository.AddRange(_mapperFactory.GetMapper<OrderItemDTO,Model.Ordering.OrderItem>().Map(_orderItemDtos));
    }
}









