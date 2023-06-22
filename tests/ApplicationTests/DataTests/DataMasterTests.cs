using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benchmarks;
using BlazorAdmin.Pages.CatalogItemPage;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using static Microsoft.eShopWeb.ApplicationCore.Entities.CatalogItem;

namespace ApplicationTests.DataTests;
public class DataMasterTests
{
    [Fact]
    public void CreateCatalogItem() 
    {
        var sut = new Microsoft.eShopWeb.PublicApi.DataMaster();
       
        var target = new CatalogItem(1, 1, $"Test Description {Guid.NewGuid()}", $"Item - {Guid.NewGuid()}", 100m, "https://cdn.shopify.com/s/files/1/2530/7762/products/rosemary-garlic-whole-chicken-008_200x200.jpg?v=1631543769");

        sut.AddNewCatalogItem(target);

        target.Id.Should().BeGreaterThan(0);


    }

    [Fact]
    public async Task ShouldUpdateCatalogItem()
    {
        var sut = new Microsoft.eShopWeb.PublicApi.DataMaster();
        var target = new CatalogItem(1, 1, $"Test Description {Guid.NewGuid()}", $"Item - {Guid.NewGuid()}", 100m, "https://cdn.shopify.com/s/files/1/2530/7762/products/rosemary-garlic-whole-chicken-008_200x200.jpg?v=1631543769");
        sut.AddNewCatalogItem(target);

        var itemDetails = new CatalogItemDetails(target.Name, target.Description, 1m);
        target.UpdateDetails(itemDetails);

        sut.UpdateCatalogItem(target);

        var item = await sut.GetCatalogItemById(target.Id);
        item.Price.Should().Be(1m);

    }
}
