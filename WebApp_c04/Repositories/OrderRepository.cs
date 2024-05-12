using System.Data.SqlClient;
using WebApp_c04.Entities;

namespace WebApp_c04.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Order? FindMatchingTo(int idProduct, int amount, DateTime createdAt)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"SELECT * FROM \"Order\" WHERE IdProduct = @IdProduct and Amount = @Amount and CreatedAt < @CreatedAt";
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

        var dr = cmd.ExecuteReader();
        var orders = new List<Order>();
        while (dr.Read())
        {
            var eOrder = new Order
            {
                IdOrder = (int)dr["IdOrder"],
                IdProduct = (int)dr["IdProduct"],
                Amount = (int)dr["Amount"],
                CreatedAt = (DateTime)dr["CreatedAt"],
                FulfilledAt = (DateTime)dr["FullfilledAt"]
            };
            orders.Add(eOrder);
        }
        
        dr.Close();
        con.Close();

        return orders.FirstOrDefault();
    }

    public int FullfillNow(Order order)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO \"Order\"(IdOrder, IdProduct, Amount, CreatedAt, FulfilledAt) VALUES (@IdOrder, @IdProduct, @Amount, GETDATE(), @FulfilledAt)";
        cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder.ToString());
        cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct.ToString());
        cmd.Parameters.AddWithValue("@Amount", order.Amount.ToString());
        cmd.Parameters.AddWithValue("@FulfilledAt", order.FulfilledAt.ToString());

        var affRows = cmd.ExecuteNonQuery();
        
        con.Close();

        return affRows;
    }
}