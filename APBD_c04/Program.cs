// See https://aka.ms/new-console-template for more information

// Console.WriteLine("Hello, World!");

using System.Data.SqlClient;

SqlTransaction tran = null;

try
{
    SqlConnection con = new SqlConnection("");
    tran = con.BeginTransaction();

    SqlCommand comm = new SqlCommand("");
    comm.Connection = con;
    comm.Transaction = tran;
    
    tran.Commit();
    // tran.CommitAsync();
}
catch (Exception ex)
{
    // ignored
    tran?.Rollback();
}
