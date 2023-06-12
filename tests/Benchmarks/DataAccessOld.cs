using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdmin.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Benchmarks;
public class DataAccessOld : IClassFixture<WebApplicationFactory<BlazorAdmin.Program>>
{
    private readonly WebApplicationFactory<BlazorAdmin.Program> _factory;

    public DataAccessOld(WebApplicationFactory<BlazorAdmin.Program> factory)
    {
        _factory = factory;
    }

    
    [Fact]
    public void DoStuff()
    {
        //var s = _factory.Services;
        var sut = _factory.Services.GetServices<CatalogItemService>();
        //var items = sut.ToList();
        //items.Count.Should().BeGreaterThan(0);

    }
    

    

}
