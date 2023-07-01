using System;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests;

public class TransferBasket
{
    private readonly string _nonexistentAnonymousBasketBuyerId = "nonexistent-anonymous-basket-buyer-id";
    private readonly string _existentAnonymousBasketBuyerId = "existent-anonymous-basket-buyer-id";
    private readonly string _nonexistentUserBasketBuyerId = "newuser@microsoft.com";
    private readonly string _existentUserBasketBuyerId = "testuser@microsoft.com";
    private readonly Mock<IAppLogger<BasketService>> _mockLogger = new();
    private readonly Mock<IDataMaster> _mockDataMaster = new();
    [Fact]
    public async Task InvokesBasketRepositoryFirstOrDefaultAsyncOnceIfAnonymousBasketNotExists()
    { 
        var anonymousBasket = null as Basket;
        var userBasket = new Basket(_existentUserBasketBuyerId);
        _mockDataMaster.SetupSequence(x => x.FetchBasketForBuyer(It.IsAny<string>()))
            .ReturnsAsync(anonymousBasket)
            .ReturnsAsync(userBasket);
        var basketService = new BasketService(_mockDataMaster.Object, _mockLogger.Object);
        await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
        _mockDataMaster.Verify(x => x.FetchBasketForBuyer(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task TransferAnonymousBasketItemsWhilePreservingExistingUserBasketItems()
    {

        var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
        anonymousBasket.AddItem(1, 10, 1);
        anonymousBasket.AddItem(3, 55, 7);
        var userBasket = new Basket(_existentUserBasketBuyerId);
        userBasket.AddItem(1, 10, 4);
        userBasket.AddItem(2, 99, 3);
        _mockDataMaster.SetupSequence(x => x.FetchBasketForBuyer(It.IsAny<string>()))
             .ReturnsAsync(anonymousBasket)
             .ReturnsAsync(userBasket);
        var basketService = new BasketService(_mockDataMaster.Object, _mockLogger.Object);
        await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
        _mockDataMaster.Verify(x => x.UpdateBasket(userBasket), Times.Once);
        Assert.Equal(3, userBasket.Items.Count);
        Assert.Contains(userBasket.Items, x => x.CatalogItemId == 1 && x.UnitPrice == 10 && x.Quantity == 5);
        Assert.Contains(userBasket.Items, x => x.CatalogItemId == 2 && x.UnitPrice == 99 && x.Quantity == 3);
        Assert.Contains(userBasket.Items, x => x.CatalogItemId == 3 && x.UnitPrice == 55 && x.Quantity == 7);
    }

    [Fact]
    public async Task RemovesAnonymousBasketAfterUpdatingUserBasket()
    {
       
        var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
        var userBasket = new Basket(_existentUserBasketBuyerId);
        _mockDataMaster.SetupSequence(x => x.FetchBasketForBuyer(It.IsAny<string>()))
            .ReturnsAsync(anonymousBasket)
            .ReturnsAsync(userBasket);
        var basketService = new BasketService(_mockDataMaster.Object, _mockLogger.Object);
        await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
        _mockDataMaster.Verify(x => x.UpdateBasket(userBasket), Times.Once);
        _mockDataMaster.Verify(x => x.DeleteBasket(anonymousBasket), Times.Once);
    }

    [Fact]
    public async Task CreatesNewUserBasketIfNotExists()
    {
        //Assert.Fail("Need to update test");
        var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
        var userBasket = null as Basket;
        _mockDataMaster.SetupSequence(x => x.FetchBasketForBuyer(It.IsAny<string>()))
            .ReturnsAsync(anonymousBasket)
            .ReturnsAsync(userBasket);
        var basketService = new BasketService(_mockDataMaster.Object, _mockLogger.Object);
        await basketService.TransferBasketAsync(_existentAnonymousBasketBuyerId, _nonexistentUserBasketBuyerId);
        _mockDataMaster.Verify(x => x.AddNewBasket(It.Is<Basket>(x => x.BuyerId == _nonexistentUserBasketBuyerId)), Times.Once);
    }
}
