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


    public async Task<int> CreateOrder(string buyerId, DateTimeOffset orderDate, string shipToAddressStreet,
        string shipToAddressCity, string shipToAddressState, string shipToAddressCountry, string shipToAddressZipCode)
    {
        using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();

            var parameters = new
            {
                BuyerId = buyerId,
                OrderDate = orderDate,
                ShipToAddress_Street = shipToAddressStreet,
                ShipToAddress_City = shipToAddressCity,
                ShipToAddress_State = shipToAddressState,
                ShipToAddress_Country = shipToAddressCountry,
                ShipToAddress_ZipCode = shipToAddressZipCode
            };

            var dynamicParameters = new DynamicParameters(parameters);
            dynamicParameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);


            await connection.ExecuteAsync(
                  "AddNewOrder",
                  dynamicParameters,
                  commandType: CommandType.StoredProcedure
              );

            var result = dynamicParameters.Get<int>("@InsertedId");
            return result;

        }
    }

    public async Task<int> AddNewOrderItem(
        int itemOrderedCatalogItemId,
        string itemOrderedProductName,
        string itemOrderedPictureUri,
        decimal unitPrice,
        int units,
        int orderId)
    {
        using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            var parameters = new
            {
                ItemOrdered_CatalogItemId = itemOrderedCatalogItemId,
                ItemOrdered_ProductName = itemOrderedProductName,
                ItemOrdered_PictureUri = itemOrderedPictureUri,
                UnitPrice = unitPrice,
                Units = units,
                OrderId = orderId
            };
            var dynamicParameters = new DynamicParameters(parameters);
            dynamicParameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            connection.Open();

            // Execute the stored procedure and retrieve the OUTPUT parameter value
            connection.Execute(
                "AddNewOrderItem",
                dynamicParameters,
                commandType: CommandType.StoredProcedure);

            var result = dynamicParameters.Get<int>("@InsertedId");
            return result;
        }
    }

    public Order CreateTestOrder()
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
                #endregion
            }catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }
    }
    public static List<BasketItem> GenerateTestBasketItems()
    {
        var testBasketItems = new List<BasketItem>();

        // Generate five test BasketItems
        for (int i = 1; i <= 5; i++)
        {
            var item = new BasketItem(i, 10.99m * i, i, i, 1);
            testBasketItems.Add(item);
        }

        return testBasketItems;
    }

    public static Basket CreateTestBasket()
    {
        // Create a new basket with a buyer ID
        var basket = new Basket("buyer123");

        // Generate test BasketItems
        var testBasketItems = GenerateTestBasketItems();

        // Add the test BasketItems to the basket
        foreach (var item in testBasketItems)
        {
            basket.AddItem(item.CatalogItemId, item.UnitPrice);
        }

        return basket;
    }
    
    [Fact]
    public async Task CreateNewOrderTest()
    {
        var order = CreateTestOrder();
        await SaveNewOrderAsync(order);
        order.Id.Should().BeGreaterThan(0);
    }



    
}

