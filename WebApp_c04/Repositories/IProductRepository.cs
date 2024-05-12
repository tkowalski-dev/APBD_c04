using WebApp_c04.Entities;

namespace WebApp_c04.Repositories;

public interface IProductRepository
{
    public Product? GetById(int id);
}