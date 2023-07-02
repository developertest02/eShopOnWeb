using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.BuyerAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using static Dapper.SqlMapper;
using CatalogItem = Microsoft.eShopWeb.ApplicationCore.Entities.CatalogItem;

namespace Microsoft.eShopWeb.ApplicationCore;

public class DataMaster : IDataMaster
{
    private SqlConnection GetConnection()
    {
        var CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.CatalogDb;";
        var connection = new SqlConnection(CONNECTION_STRING);
        return connection;
    }

    
    public async void SeedDatabase()
    {
        //var connection = GetConnection();
        //connection.Execute(
        //"ResetDatabase",   
        //   commandType: CommandType.StoredProcedure);
        //var dm = new DataMaster();
        //var catalogItems = GetPreconfiguredItems().ToList();
        //catalogItems.ForEach(c => dm.AddNewCatalogItem(c));
    }

    #region catalog

    public Entities.CatalogItem AddNewCatalogItem(Entities.CatalogItem source)
    {
        var connection = GetConnection();
        // Define a parameter for the inserted Id value
        var insertedId = new SqlParameter("@InsertedId", SqlDbType.Int)
        {
            Direction = ParameterDirection.ReturnValue
        };

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var parameters = new
        {
            source.Name,
            source.Description,
            source.Price,
            source.PictureUri,
            source.CatalogTypeId,
            source.CatalogBrandId
        };
        dynamicParameters.AddDynamicParams(parameters);

        // Execute the stored procedure using Dapper
        var result = connection.Execute("AddCatalogItem",
            dynamicParameters,
            null,
            null,
            CommandType.StoredProcedure);
        var id = dynamicParameters.Get<int>("@InsertedId");

        source.SetId(id);
        return source;


    }

    public async Task AddNewBasket(Basket source)
    {
        var connection = GetConnection();
        var insertedId = new SqlParameter("@InsertedId", SqlDbType.Int)
        {
            Direction = ParameterDirection.ReturnValue
        };
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);
        var parameters = new
        {
            source.BuyerId
        };
        dynamicParameters.AddDynamicParams(parameters);

        var result = connection.Execute("AddBasket",
          dynamicParameters,
          null,
          null,
          CommandType.StoredProcedure);
        var id = dynamicParameters.Get<int>("@InsertedId");
        
        source.SetId(id);

    }
    public async Task DeleteBasket(Basket basket)
    {
        var connection = GetConnection();
        _ = await connection.ExecuteAsync("DeleteBasket", new { Id = basket.Id }, commandType: CommandType.StoredProcedure);
    }
    public async Task DeleteCatalogItemByIdAsync(int catalogItemId)
    {
        var connection = GetConnection();
        _ = await connection.ExecuteAsync("DeleteCatalogItem", new { Id = catalogItemId }, commandType: CommandType.StoredProcedure);

    }
    public async Task<List<CatalogItemDto>> GetCatalogItems(int? pageNumber, int? pageSize, int? catalogTypeId, int? catalogBrandId)
    {

        var connection = GetConnection();
        var queryValues = new { PageNumber = pageNumber, PageSize = pageSize, CatalogTypeId = catalogTypeId, CatalogBrandId = catalogBrandId };
        var procedure = "FetchCatalogItems";
        var result = await connection.QueryAsync<CatalogItemDto>(procedure, queryValues, commandType: CommandType.StoredProcedure);
        return result.ToList();

    }

    public async Task<List<CatalogItemDto>> FetchCatalogItemsByIds(List<int> ids)
    {
        var connection = GetConnection();
        var result = await connection.QueryAsync<CatalogItemDto>("SELECT * FROM Catalog WHERE id in @ids", new { ids });
        return result.ToList();
    }
    public async Task<List<BasketItem>> FetchBasketItems(int id)
    {
        var connection = GetConnection();
        var dynamicParameters = new DynamicParameters();
        var parameters = new
        {
            BasketId = id
        };
        dynamicParameters.AddDynamicParams(parameters);
        var procedure = "FetchBasketItems";
        var result = await connection.QueryAsync<BasketItem>(procedure, dynamicParameters, commandType: CommandType.StoredProcedure);
        return result.ToList();

    }

    //public async Task<Basket> FetchBasketWithItemsForBuyer(string buyerId)
    //{
    //    var basket = await FetchBasketForBuyer(buyerId);
    //    if(basket != null)
    //    {
    //        var items = await FetchBasketItems(basket.Id);
    //        items.ForEach(item => basket.AddItem(item.CatalogItemId, item.UnitPrice, item.Quantity));
    //    }
    //    return basket;
    //}
    public async Task<Basket> FetchBasketById(int id)
    {
        var connection = GetConnection();
        var dynamicParameters = new DynamicParameters();
        var parameters = new
        {
            id
        };
        dynamicParameters.AddDynamicParams(parameters);
        var procedure = "FetchBasketById";
        var result = await connection.QueryFirstOrDefaultAsync<Basket>(procedure, dynamicParameters, commandType: CommandType.StoredProcedure);
        if (result != null)
        {
            result = await FetchBasketForBuyer(result.BuyerId);
        }
        return result;
    }
    public async Task<Basket> FetchBasketForBuyer(string buyerId)
    {

        var connection = GetConnection();
        var dynamicParameters = new DynamicParameters();
        var parameters = new
        {
            buyerId
        };
        dynamicParameters.AddDynamicParams(parameters);
        var procedure = "FetchBuyerBasket";
        var result = await connection.QueryFirstOrDefaultAsync<Basket>(procedure, dynamicParameters, commandType: CommandType.StoredProcedure);
        if (result != null)
        {
            var items = await FetchBasketItems(result.Id);
            items.ForEach(item => result.AddItem(item));
        }
        return result;

    }
    public async Task<CatalogItemDto> GetCatalogItemById(int id)
    {
        var connection = GetConnection();
        var result = await connection.QuerySingleOrDefaultAsync<CatalogItemDto>("SELECT * FROM Catalog WHERE Id = @Id", new { Id = id });
        return result;
    }
    public List<CatalogBrandDto> GetCatalogBrands()
    {
        var connection = GetConnection();
        var result = connection.Query<CatalogBrandDto>("SELECT Id, Brand as Name FROM CatalogBrands");
        return result.ToList();

    }

    public List<CatalogTypeDto> GetCatalogTypes()
    {
        var connection = GetConnection();
        var result = connection.Query<CatalogTypeDto>("SELECT Id, Type as Name FROM CatalogTypes").ToList();
        return result;
    }

    public async Task UpdateBasket(Basket basket)
    {
        var basketWithItems = await FetchBasketForBuyer(basket.BuyerId);
        if (basketWithItems == null)
            return;
        var connection = GetConnection();
        var dynamicParameters = new DynamicParameters();
        var parameters = new
        {
            BasketId = basket.Id
        };
        dynamicParameters.AddDynamicParams(parameters);

        var procedure = "DeleteAllItemsFromBasket";
        await connection.ExecuteAsync(procedure, dynamicParameters, commandType: CommandType.StoredProcedure);

        dynamicParameters = new DynamicParameters();
        foreach (var item in basket.Items)
        {
            var parameters2 = new
            {
                BasketId = basket.Id,
                item.UnitPrice,
                item.Quantity,
                item.CatalogItemId

            };
            dynamicParameters.AddDynamicParams(parameters2);
            var procedure2 = "AddItemToBasket";
            await connection.ExecuteAsync(procedure2, dynamicParameters, commandType: CommandType.StoredProcedure);
        }

    }

    public void UpdateCatalogItem(Entities.CatalogItem source)
    {
        var connection = GetConnection();
        var dynamicParameters = new DynamicParameters();
        var parameters = new
        {
            source.Id,
            source.Name,
            source.Description,
            source.Price,
            source.PictureUri,
            source.CatalogTypeId,
            source.CatalogBrandId
        };
        dynamicParameters.AddDynamicParams(parameters);

        // Execute the stored procedure using Dapper
        var result = connection.Execute("UpdateCatalogItem",
            dynamicParameters,
            null,
            null,
            CommandType.StoredProcedure);


    }
    public static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
            {
                new(2,2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new(1,2, ".NET Black & White Mug", ".NET Black & White Mug", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.png"),
                new(2,5, "Prism White T-Shirt", "Prism White T-Shirt", 12,  "http://catalogbaseurltobereplaced/images/products/3.png"),
                new(2,2, ".NET Foundation Sweatshirt", ".NET Foundation Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new(3,5, "Roslyn Red Sheet", "Roslyn Red Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.png"),
                new(2,2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/6.png"),
                new(2,5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt",  12, "http://catalogbaseurltobereplaced/images/products/7.png"),
                new(2,5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5M, "http://catalogbaseurltobereplaced/images/products/8.png"),
                new(1,5, "Cup<T> White Mug", "Cup<T> White Mug", 12, "http://catalogbaseurltobereplaced/images/products/9.png"),
                new(3,2, ".NET Foundation Sheet", ".NET Foundation Sheet", 12, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,2, "Cup<T> Sheet", "Cup<T> Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/11.png"),
                new(2,5, "Prism White TShirt", "Prism White TShirt", 12, "http://catalogbaseurltobereplaced/images/products/12.png")
            };
    }

    #endregion

    #region orders
    public async Task SaveNewOrderAsync(Order order)
    {
        using (IDbConnection connection = GetConnection())
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
                      transaction: transaction
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

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }
    }

    public async Task<List<Order>> FetchOrdersAsync(int? orderId = null, string? buyerId = null)
    {
        List<Order> result = new List<Order>();
        List<OrderWithItemsDto> queryItems;
        var sql = "SELECT * FROM OrdersWithItemsView";
        if (orderId.HasValue)
            sql = $"{sql} WHERE OrderId = {orderId.GetValueOrDefault()}";
        else if(!string.IsNullOrEmpty(buyerId))
            sql = $"{sql} WHERE BuyerId = '{buyerId}'";
        using (var connection = GetConnection())
        {
            var queryResult = await connection.QueryAsync<OrderWithItemsDto>(sql);
            queryItems = queryResult.ToList();
        }

        
        var itemsGroupedByOrders = queryItems.GroupBy(n => n.OrderId);
        foreach(var orderWithItems in itemsGroupedByOrders)
        {
            var order = orderWithItems.First();
            
            var address = new Address(order.ShipToAddress_Street, order.ShipToAddress_City, order.ShipToAddress_State, order.ShipToAddress_Country, order.ShipToAddress_ZipCode);
            var orderItems = new List<OrderItem>();
            foreach(var item in orderItems)
            {
                var catalogItemOrdered = new CatalogItemOrdered(item.ItemOrdered.CatalogItemId, item.ItemOrdered.ProductName, item.ItemOrdered.PictureUri);
                var orderItemModel = new OrderItem(catalogItemOrdered, item.UnitPrice, item.Units);
                orderItems.Add(orderItemModel);
            }

            var orderModel = new Order(order.BuyerId, address, orderItems);
            orderModel.SetId(order.OrderId);
            result.Add(orderModel);

        }

        return result;
            
    }

    #region dtos
    private class OrderWithItemsDto
    {
        public int OrderId { get; set; }
        public string BuyerId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string ShipToAddress_Street { get; set; }
        public string ShipToAddress_City { get; set; }
        public string ShipToAddress_State { get; set; }
        public string ShipToAddress_Country { get; set; }
        public string ShipToAddress_ZipCode { get; set; }
        public int OrderItemId { get; set; }
        public int? ItemOrdered_CatalogItemId { get; set; }
        public string ItemOrdered_ProductName { get; set; }
        public string ItemOrdered_PictureUri { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
    }
    #endregion
    #endregion
}
