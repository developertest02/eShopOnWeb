using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class OrderService : IOrderService
{
   // private readonly IRepository<Order> _orderRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IDataMaster _dataMaster;
    //private readonly IRepository<Basket> _basketRepository;
    //private readonly IRepository<CatalogItem> _itemRepository;

    public OrderService(
        IUriComposer uriComposer
        ,IDataMaster dataMaster)
    {
        _dataMaster = dataMaster;
        _uriComposer = uriComposer;

    }

    public async Task CreateOrderAsync(int basketId, Address shippingAddress)
    {

        var basket = await _dataMaster.FetchBasketById(basketId);

        if (basket is null)
            throw new ArgumentNullException("basket");

        if (!basket.Items.Any())
            throw new EmptyBasketOnCheckoutException();

        var allCatalogItems = await _dataMaster.GetCatalogItems(null,null,null,null);
        var catalogItems = allCatalogItems.Where(c => basket.Items.Any(b => b.CatalogItemId == c.Id)).ToList();

        var items = basket.Items.Select(basketItem =>
        {
            var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);
            var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, _uriComposer.ComposePicUri(catalogItem.PictureUri));
            var orderItem = new OrderItem(itemOrdered, basketItem.UnitPrice, basketItem.Quantity);
            return orderItem;
        }).ToList();

        var order = new Order(basket.BuyerId, shippingAddress, items);
        await _dataMaster.SaveNewOrderAsync(order);
        

    }
}
