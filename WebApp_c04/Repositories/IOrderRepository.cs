using WebApp_c04.Entities;

namespace WebApp_c04.Repositories;

public interface IOrderRepository
{
    public Order? FindMatchingTo(int idProduct, int amount, DateTime createdAt);
    public int FullfillNow(Order order);
}