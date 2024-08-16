namespace DataTransferObjects;
public class OrderItem
{
	public long ID { get; set; }
	public long OrderID { get; set; }
	public long BookID { get; set; }
	public int Quantity { get; set; }
}
