using System.Data;
using System.Data.SqlClient;
using Dapper;

public class BasketRepository
{
    private readonly string connectionString;

    public BasketRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public int CreateBasket(string buyerId)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BuyerId", buyerId);

            var result = connection.ExecuteScalar<int>("CreateBasket", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
