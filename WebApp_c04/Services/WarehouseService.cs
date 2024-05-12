using WebApp_c04.Entities;
using WebApp_c04.Repositories;

namespace WebApp_c04.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IConfiguration _configuration;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProduct_WarehouseRepository _productWarehouseRepository;

    public WarehouseService(IConfiguration configuration, IOrderRepository orderRepository, IProductRepository productRepository, IWarehouseRepository warehouseRepository, IProduct_WarehouseRepository product_WarehouseRepository)
    {
        _configuration = configuration;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _productWarehouseRepository = product_WarehouseRepository;
    }

    public int AddProductToWarehouse(Warehouse w)
    {
        var product = _productRepository.GetById(w.IdProduct);
        if (product == null)
        {
            throw new DomainException("Produkt o podanym id nie istnieje!");
        }
        
        var warehouse = _warehouseRepository.GetById(w.IdWarehouse);
        if (warehouse == null)
        {
            throw new DomainException("Magazyn o podanym id nie istnieje!");
        }

        if (w.Amount <= 0)
        {
            throw new DomainException("Ilość nie może być niedodatnia!");
        }

        var order = _orderRepository.FindMatchingTo(w.IdProduct, w.Amount, w.CreatedAt);
        if (order == null)
        {
            throw new DomainException("Nie znaleziono pasującego zamówienia!");
        }

        var pastOrders = _productWarehouseRepository.FindOrders(order);
        if (pastOrders.Count > 0)
        {
            throw new DomainException("Zamówienie zostało już wcześniej zrealizowane!");
        }

        _orderRepository.FullfillNow(order);
        var id_productWarehouse = _productWarehouseRepository.AddRecord(product, warehouse, order, product.Price * order.Amount);
        
        return id_productWarehouse;
    }
}