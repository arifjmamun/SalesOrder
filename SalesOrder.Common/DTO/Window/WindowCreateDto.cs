using SalesOrder.Common.DTO.Element;

namespace SalesOrder.Common.DTO.Window;

public class WindowCreateDto
{
    public string Name { get; set; }

    public int QuantityOfWindows { get; set; }

    public List<SubElementCreateDto> SubElements { get; set; } = new List<SubElementCreateDto>();
}
