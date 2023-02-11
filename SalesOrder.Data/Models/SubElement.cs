using System.ComponentModel.DataAnnotations;

namespace SalesOrder.Data.Models;

public class SubElement : BaseModel<int>
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Element { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Type { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int Width { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int Height { get; set; }
}
