using WebApp_c04.Entities;

namespace WebApp_c04.Repositories;

public interface IProduct_WarehouseRepository
{
    public List<Product_Warehouse> FindOrders(Order order);
    public int AddRecord(Product product, Warehouse warehouse, Order order, decimal price);
}