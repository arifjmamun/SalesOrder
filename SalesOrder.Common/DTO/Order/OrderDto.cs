namespace SalesOrder.Common.DTO.Order;

public class OrderDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}