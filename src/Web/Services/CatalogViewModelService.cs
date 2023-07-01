using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Services;

/// <summary>
/// This is a UI-specific service so belongs in UI project. It does not contain any business logic and works
/// with UI-specific types (view models and SelectListItem types).
/// </summary>
public class CatalogViewModelService : ICatalogViewModelService
{
    private readonly ILogger<CatalogViewModelService> _logger;
    private readonly IDataMaster _dataMaster;
    //private readonly IRepository<CatalogItem> _itemRepository;
    //private readonly IRepository<CatalogBrand> _brandRepository;
    //private readonly IRepository<CatalogType> _typeRepository;
    private readonly IUriComposer _uriComposer;

    public CatalogViewModelService(
        IDataMaster dataMaster,
        ILoggerFactory loggerFactory,     
        IUriComposer uriComposer)
    {
        _logger = loggerFactory.CreateLogger<CatalogViewModelService>();
        _dataMaster = dataMaster;
        _uriComposer = uriComposer;
    }

    public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
    {
        _logger.LogInformation("GetCatalogItems called.");

        var filterSpecification = new CatalogFilterSpecification(brandId, typeId);
        var filterPaginatedSpecification =
            new CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, brandId, typeId);

        // the implementation below using ForEach and Count. We need a List.
        var itemsOnPage = await _dataMaster.GetCatalogItems(itemsPage * pageIndex, itemsPage, brandId, typeId); // _itemRepository.ListAsync(filterPaginatedSpecification);
        var totalItemsRecords = await _dataMaster.GetCatalogItems(null,null,null,null);
        var totalItems = totalItemsRecords.Count(); 

        var vm = new CatalogIndexViewModel()
        {
            CatalogItems = itemsOnPage.Select(i => new CatalogItemViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                Price = i.Price
            }).ToList(),
            Brands = (await GetBrands()).ToList(),
            Types = (await GetTypes()).ToList(),
            BrandFilterApplied = brandId ?? 0,
            TypesFilterApplied = typeId ?? 0,
            PaginationInfo = new PaginationInfoViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = itemsOnPage.Count,
                TotalItems = totalItems,
                TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
            }
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        return vm;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        _logger.LogInformation("GetBrands called.");
        var brands =  _dataMaster.GetCatalogBrands();//_brandRepository.ListAsync();

        var items = brands
            .Select(brand => new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Name })
            .OrderBy(b => b.Text)
            .ToList();

        var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
        items.Insert(0, allItem);

        return items;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        _logger.LogInformation("GetTypes called.");
        var types = _dataMaster.GetCatalogTypes(); //_typeRepository.ListAsync();

        var items = types
            .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Name })
            .OrderBy(t => t.Text)
            .ToList();

        var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
        items.Insert(0, allItem);

        return items;
    }
}
