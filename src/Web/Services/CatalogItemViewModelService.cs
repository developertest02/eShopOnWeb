﻿using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Services;

public class CatalogItemViewModelService : ICatalogItemViewModelService
{
    private readonly IDataMaster _dataMaster;

    public CatalogItemViewModelService(IDataMaster dataMaster)
    {
        _dataMaster = dataMaster;
    }

    public async Task UpdateCatalogItem(CatalogItemViewModel viewModel)
    {
        var existingCatalogItem = await _dataMaster.GetCatalogItemById(viewModel.Id);

        if (existingCatalogItem == null)
        {
            throw new ArgumentException($"Catalog item with id {viewModel.Id} not found");
        }
        //Guard.Against.Null(existingCatalogItem, nameof(existingCatalogItem));

        CatalogItem.CatalogItemDetails details = new(viewModel.Name, existingCatalogItem.Description, viewModel.Price);
        //existingCatalogItem.UpdateDetails(details);
        //await _catalogItemRepository.UpdateAsync(existingCatalogItem);
    }
}
