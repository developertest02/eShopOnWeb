using System.Data;
using System.Data.Common;

using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.BuyerAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using NuGet.Protocol.Plugins;
using static Dapper.SqlMapper;
using static Microsoft.eShopWeb.ApplicationCore.DataMaster;

namespace ApplicationTests;

public class DapperTests
{
    private const string CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.CatalogDb;";


    Order CreateTestOrder()
    {
        var buyerId = "demouser@microsoft.com";
        var orderDate = DateTime.UtcNow;
        var shipToAddressStreet = "123 Main St";
        var shipToAddressCity = "Redmond";
        var shipToAddressState = "WA";
        var shipToAddressCountry = "USA";
        var shipToAddressZipCode = "98052";

        var address = new Address(shipToAddressStreet, shipToAddressCity,shipToAddressState, shipToAddressCountry, shipToAddressZipCode);

        var orderItems = new List<OrderItem>();
        for (int i = 1; i <= 5; i++)
        {
            var orderedItem = new CatalogItemOrdered(i, Guid.NewGuid().ToString(), "http://catalogbaseurltobereplaced/images/products/1.png");
            var item = new OrderItem(orderedItem, 1.00m * i, i);
            orderItems.Add(item);
        }

        var result = new Order(buyerId, address, orderItems);

        return result;
    }

    public async Task SaveNewOrderAsync(Order order)
    {
        using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                #region order
                var orderParameters = new
                {
                    BuyerId = order.BuyerId,
                    OrderDate = order.OrderDate,
                    ShipToAddress_Street = order.ShipToAddress.Street,
                    ShipToAddress_City = order.ShipToAddress.City,
                    ShipToAddress_State = order.ShipToAddress.State,
                    ShipToAddress_Country = order.ShipToAddress.Country,
                    ShipToAddress_ZipCode = order.ShipToAddress.ZipCode
                };

                var dyanmicOrderParameters = new DynamicParameters(orderParameters);
                dyanmicOrderParameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);


                await connection.ExecuteAsync(
                      "AddNewOrder",
                      dyanmicOrderParameters,
                      commandType: CommandType.StoredProcedure,
                      transaction:transaction
                  );


                var orderId = dyanmicOrderParameters.Get<int>("@InsertedId");
                order.SetId(orderId);
                #endregion

                #region order items
                foreach (var item in order.OrderItems)
                {


                    var orderItemParameters = new
                    {
                        ItemOrdered_CatalogItemId = item.ItemOrdered.CatalogItemId,
                        ItemOrdered_ProductName = item.ItemOrdered.ProductName,
                        ItemOrdered_PictureUri = item.ItemOrdered.PictureUri,
                        UnitPrice = item.UnitPrice,
                        Units = item.Units,
                        OrderId = order.Id
                    };
                    var orderItemDynamicParameters = new DynamicParameters(orderItemParameters);
                    orderItemDynamicParameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                      "AddNewOrderItem",
                      orderItemDynamicParameters,
                      commandType: CommandType.StoredProcedure,
                      transaction: transaction
                  );

                    var orderItemId = orderItemDynamicParameters.Get<int>("@InsertedId");
                    item.SetId(orderItemId);

                }

                transaction.Commit();
                #endregion
            }catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }
    }
    
    [Fact]
    public async Task CreateNewOrderTest()
    {
        var order = CreateTestOrder();
        var sut = new DataMaster();
        await sut.SaveNewOrderAsync(order);
        order.Id.Should().BeGreaterThan(0);
    }


    [Fact]
    public void SeedIt()
    {
        var dm = new DataMaster();
        dm.SeedDatabase();
    }

    
}

