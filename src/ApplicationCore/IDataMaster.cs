using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore;
public interface IDataMaster
{
    Task AddNewBasket(Basket source);
    CatalogItem AddNewCatalogItem(CatalogItem source);
    Task DeleteBasket(Basket basket);
    Task DeleteCatalogItemByIdAsync(int catalogItemId);
    Task<Basket> FetchBasketById(int id);
    Task<Basket> FetchBasketForBuyer(string buyerId);
    Task<List<BasketItem>> FetchBasketItems(int id);
    Task<List<CatalogItemDto>> FetchCatalogItemsByIds(List<int> ids);
    List<CatalogBrandDto> GetCatalogBrands();
    Task<CatalogItemDto> GetCatalogItemById(int id);
    Task<List<CatalogItemDto>> GetCatalogItems(int? pageNumber, int? pageSize, int? catalogTypeId, int? catalogBrandId);
    List<CatalogTypeDto> GetCatalogTypes();
    void SeedDatabase();
    Task UpdateBasket(Basket basket);
    void UpdateCatalogItem(CatalogItem source);
    Task<List<Order>> FetchOrdersAsync(int? orderId = null, string? buyerId = null);
}
