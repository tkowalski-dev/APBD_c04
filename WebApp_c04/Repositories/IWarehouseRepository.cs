using WebApp_c04.Entities;

namespace WebApp_c04.Repositories;

public interface IWarehouseRepository
{
    public Warehouse? GetById(int id);
}