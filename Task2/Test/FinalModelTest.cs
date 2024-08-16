using Task2.Repository;
using Task2.Service;
using OrderDTO = DataTransferObjects.Order;

namespace Task2.Test;

public static class FinalModelTest
{
    private static OrderRepository _orderRepository = OrderRepository.getInstance();
    public static void check_total_results_of_orders_equal_with_dataset()
    { 
        var orders = _orderRepository.GetAll();
        var csvReader = new CsvReaderService();
        var orderDtos = csvReader.ReadFromCsv<OrderDTO>("C:/Users/Lenovo/Desktop/C#-codes/bootCamp/Task2/Datasets/Orders.csv");
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].ID == orderDtos[i].ID)
            {
                if (orders[i].TotalPrice != orderDtos[i].TotalPrice)
                {
                    Console.WriteLine("Test failed");
                    return;
                }
            }
        }
        Console.WriteLine("Test passed");
    }
}