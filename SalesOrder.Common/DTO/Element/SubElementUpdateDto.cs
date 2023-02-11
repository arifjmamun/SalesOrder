namespace SalesOrder.Common.DTO.Element;

public class SubElementUpdateDto
{
    public int Id { get; set; }
    
    public int Element { get; set; }

    public string Type { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }
}
