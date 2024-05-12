using System.ComponentModel.DataAnnotations;

namespace WebApp_c04.Entities;

public class Product
{
    [Required]
    public int IdProduct { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}