using System.Data.SqlClient;
using WebApp_c04.Entities;
using WebApp_c04.Services;

namespace WebApp_c04.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Warehouse? GetById(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand("");
        cmd.Connection = con;
        cmd.CommandText = $"";

        var dr = cmd.ExecuteReader();
        var warehouses = new List<Warehouse>();
        while (dr.Read())
        {
            var eWarehouse = new Warehouse
            {
                IdProduct = (int)dr["IdProduct"],
                IdWarehouse = (int)dr[""],
                Amount = (int)dr[""],
                CreatedAt = (DateTime)dr[""]
            };
            warehouses.Add(eWarehouse);
        }
        
        dr.Close();
        con.Close();

        return warehouses.FirstOrDefault();
    }
}