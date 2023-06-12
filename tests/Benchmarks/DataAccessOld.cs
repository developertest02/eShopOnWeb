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
public class DataAccessOld
{ 
    private readonly WebApplicationFactory<Microsoft.eShopWeb.Web.Program> _factory;
    private readonly IServiceProvider _serviceProvider;
    private readonly CatalogItemService _catalogItemService;
    public DataAccessOld()
    {
        _factory = new WebApplicationFactory<Microsoft.eShopWeb.Web.Program>();
        var scope  = _factory.Server.Services.GetService<IServiceScopeFactory>()
            .CreateScope();
        _serviceProvider = scope.ServiceProvider;
        _catalogItemService = _serviceProvider.GetRequiredService<CatalogItemService>();
    }

    
    [Fact]
    public async Task GetItems()
    {
        var result = await _catalogItemService.List();
        result.Count.Should().BeGreaterThan(0);

    }




}
