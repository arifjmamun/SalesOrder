using System.ComponentModel.DataAnnotations;

namespace SalesOrder.Data.Models;

public sealed class Order : BaseModel<int>
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(50)]
    public string State { get; set; }
    
    public ICollection<Window> Windows { get; set; }
}
