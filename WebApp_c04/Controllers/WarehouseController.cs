using Microsoft.AspNetCore.Mvc;
using WebApp_c04.Entities;
using WebApp_c04.Services;

namespace WebApp_c04.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    
    [HttpPost]
    public IActionResult AddProductToWarehouse(Warehouse warehouse)
    {
        try
        {
            var affectedCount = _warehouseService.AddProductToWarehouse(warehouse);
            return Created();
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }
}