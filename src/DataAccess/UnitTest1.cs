using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccess.Dto;
using FluentAssertions;

namespace DataAccess;

public class UnitTest1
{
    
    
    [Fact]
    public void Test1()
    {

        var repo = new BasketItemRepository(Constants.CONNECTION_STRING);
        var items = repo.GetBasketItemsByBuyerId("demouser@microsoft.com");
        items.Count.Should().BeGreaterOrEqualTo(1);

    }

    public class BasketItemRepository
    {
        private readonly string connectionString;

        public BasketItemRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<BasketItem> GetBasketItemsByBuyerId(string buyerId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new { BuyerId = buyerId };
                return connection.Query<BasketItem>("GetBasketItemsByBuyerId", parameters, commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
