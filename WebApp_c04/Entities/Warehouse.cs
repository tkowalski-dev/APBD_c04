using System.ComponentModel.DataAnnotations;

namespace WebApp_c04.Entities;

public class Warehouse
{
    [Required]
    public int IdProduct { get; set; }
    
    [Required]
    public int IdWarehouse { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
}