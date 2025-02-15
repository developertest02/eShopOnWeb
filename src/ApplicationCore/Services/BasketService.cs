﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Ardalis.Result;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public class BasketService : IBasketService
{
   // private readonly IRepository<Basket> _basketRepository;
    private readonly IAppLogger<BasketService> _logger;
    private readonly IDataMaster _dataMaster;
    public BasketService(IDataMaster dataMaster,
        IAppLogger<BasketService> logger)
    {

        _dataMaster = dataMaster;
        _logger = logger;
    }

    public async Task<Basket> AddItemToBasket(string username, int catalogItemId, decimal price, int quantity = 1)
    {
        var basketSpec = new BasketWithItemsSpecification(username);

        //var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
        var basket = await _dataMaster.FetchBasketForBuyer(username);
        
        if (basket == null)
        {
            basket = new Basket(username);
            await _dataMaster.AddNewBasket(basket);
        }

        basket.AddItem(catalogItemId, price, quantity);

        await _dataMaster.UpdateBasket(basket);
        return basket;
    }

    public async Task DeleteBasketAsync(int basketId)
    {
        var basket = await _dataMaster.FetchBasketById(basketId);
        if (basket is null)
            throw new KeyNotFoundException($"No basket found for ID {basketId}.");

        await _dataMaster.DeleteBasket(basket); 
    }

    public async Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities)
    {
        var basketSpec = new BasketWithItemsSpecification(basketId);
        var basket = await _dataMaster.FetchBasketById(basketId); 
        if (basket == null) return Result<Basket>.NotFound();

        foreach (var item in basket.Items)
        {
            if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
            {
                if (_logger != null) _logger.LogInformation($"Updating quantity of item ID:{item.Id} to {quantity}.");
                item.SetQuantity(quantity);
            }
        }
        basket.RemoveEmptyItems();
        await _dataMaster.UpdateBasket(basket);
        return basket;
    }

    public async Task TransferBasketAsync(string anonymousId, string userName)
    {
        //var anonymousBasketSpec = new BasketWithItemsSpecification(anonymousId);
        var anonymousBasket = await _dataMaster.FetchBasketForBuyer(anonymousId); //_basketRepository.FirstOrDefaultAsync(anonymousBasketSpec);
        if (anonymousBasket == null) return;
      //  var userBasketSpec = new BasketWithItemsSpecification(userName);
        var userBasket = await _dataMaster.FetchBasketForBuyer(userName);
        if (userBasket == null)
        {
            userBasket = new Basket(userName);
            await _dataMaster.AddNewBasket(userBasket);
        }
        foreach (var item in anonymousBasket.Items)
        {
            userBasket.AddItem(item.CatalogItemId, item.UnitPrice, item.Quantity);
        }
        await _dataMaster.UpdateBasket(userBasket);
        await _dataMaster.DeleteBasket(anonymousBasket);
    }
}
