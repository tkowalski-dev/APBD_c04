using System.Data.SqlClient;
using WebApp_c04.Entities;

namespace WebApp_c04.Repositories;

public class Product_WarehouseRepository : IProduct_WarehouseRepository
{
    private readonly IConfiguration _configuration;

    public Product_WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Product_Warehouse> FindOrders(Order order)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"SELECT * FROM \"Order\" WHERE IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);

        var dr = cmd.ExecuteReader();
        var product_Warehouses = new List<Product_Warehouse>();
        while (dr.Read())
        {
            var eProduct_Warehouse = new Product_Warehouse
            {
                IdProductWarehouse = (int)dr["IdProductWarehouse"],
                IdWarehouse = (int)dr["IdWarehouse"],
                IdProduct = (int)dr["IdProduct"],
                IdOrder = (int)dr["IdOrder"],
                Amount = (int)dr["Amount"],
                Price = (decimal)dr["Price"],
                CreatedAt = (DateTime)dr["CreatedAt"]
            };
            product_Warehouses.Add(eProduct_Warehouse);
        }
        
        dr.Close();
        con.Close();

        return product_Warehouses;
    }

    public int AddRecord(Product product, Warehouse warehouse, Order order, decimal price)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO \"Product_Warehouse\"(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES " +
                          $"(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, GETDATE())";
        cmd.Parameters.AddWithValue("@IdWarehouse", warehouse.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdProduct", product.IdProduct);
        cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
        cmd.Parameters.AddWithValue("@Amount", order.Amount);
        cmd.Parameters.AddWithValue("@Price", price);

        var affRows = cmd.ExecuteNonQuery();
        
        con.Close();

        return affRows;
    }
}