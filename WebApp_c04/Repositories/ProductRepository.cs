using System.Data.SqlClient;
using WebApp_c04.Entities;

namespace WebApp_c04.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Product? GetById(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"SELECT * FROM \"Product\" WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", id);

        var dr = cmd.ExecuteReader();
        var products = new List<Product>();
        while (dr.Read())
        {
            var eProduct = new Product
            {
                IdProduct = (int)dr["IdProduct"],
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Price = (decimal)dr["Price"]
            };
            products.Add(eProduct);
        }
        
        dr.Close();
        con.Close();

        return products.FirstOrDefault();
    }
}