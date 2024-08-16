namespace DataTransferObjects;
public class AudioBook
{
	public long ID { get; set; }
	public string Title { get; set; }
	public string ISBN { get; set; }
	public decimal Price { get; set; }
	public int TotalMinutes { get; set; }
}

