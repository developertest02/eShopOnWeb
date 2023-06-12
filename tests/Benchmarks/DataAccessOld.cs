using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlazorAdmin.Services;
using BlazorShared.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.PublicApi;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Benchmarks;
public class DataAccessOld
{ 
    private readonly WebApplicationFactory<Microsoft.eShopWeb.Web.Program> _factory;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly IRepository<Microsoft.eShopWeb.ApplicationCore.Entities.CatalogItem> _catalogItemRepository;
    public DataAccessOld()
    {
        _factory = new WebApplicationFactory<Microsoft.eShopWeb.Web.Program>();
        var scope  = _factory.Server.Services.GetService<IServiceScopeFactory>()
            .CreateScope();
        _serviceProvider = scope.ServiceProvider;
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();
        _catalogItemRepository = _serviceProvider.GetRequiredService<IRepository<Microsoft.eShopWeb.ApplicationCore.Entities.CatalogItem>>();
    }

    
    [Fact]
    public void GetItems()
    {
       var actual =  DoBenchmark();
        actual.CatalogItems.Count.Should().BeGreaterThan(0);       
    }

    public ListPagedCatalogItemResponse DoBenchmark()
    {
        var catologSettings = new CatalogSettings();
        catologSettings.CatalogBaseUrl = "https://localhost:5001/";
        var uriComposer = new UriComposer(catologSettings);
        var sut = new CatalogItemListPagedEndpoint(uriComposer, _mapper);
        var request = new ListPagedCatalogItemRequest(null, null, null, null);
        var result = sut.HandleAsync(request, _catalogItemRepository).Result as Microsoft.AspNetCore.Http.HttpResults.Ok<ListPagedCatalogItemResponse>;
        return result.Value;
    }
    



}
