using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using static BlazorShared.Authorization.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microsoft.eShopWeb.PublicApi.Controllers;
[Route("api/catalog-items")]
[ApiController]
public class CatalogItemListController : ControllerBase
{
    private readonly DataMaster _dataMaster;
    private readonly IUriComposer _uriComposer;
    public CatalogItemListController(IUriComposer uriComposer, DataMaster dataMaster)
    {
        _dataMaster = dataMaster;
        _uriComposer = uriComposer;
    }
    // GET: api/<CatalogItemList>
    [HttpGet]
    public async Task<IResult> Get(int? pageSize, int? pageIndex, int? catalogBrandId, int? catalogTypeId)
    {
        return await HandleAsync(new ListPagedCatalogItemRequest(pageSize, pageIndex, catalogBrandId, catalogTypeId));
    }

    // GET api/<CatalogItemList>/5
    [HttpGet("{catalogItemId}")]
    public async Task<IResult> Get(int catalogItemId)
    {
        var request = new GetByIdCatalogItemRequest(catalogItemId);
        var response = new GetByIdCatalogItemResponse(request.CorrelationId());
        var item = await _dataMaster.GetCatalogItemById(request.CatalogItemId);
        //var item = await itemRepository.GetByIdAsync(request.CatalogItemId);
        if (item is null)
            return Results.NotFound();

        response.CatalogItem = new CatalogItemDto
        {
            Id = item.Id,
            CatalogBrandId = item.CatalogBrandId,
            CatalogTypeId = item.CatalogTypeId,
            Description = item.Description,
            Name = item.Name,
            PictureUri = _uriComposer.ComposePicUri(item.PictureUri),
            Price = item.Price
        };
        return Results.Ok(response);
    }

    // POST api/<CatalogItemList>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CatalogItemList>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CatalogItemList>/5
    [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{catalogItemId}")]
    public async Task<IResult> Delete(int catalogItemId)
    {
        var request = new DeleteCatalogItemRequest(catalogItemId);
        var response = new DeleteCatalogItemResponse(request.CorrelationId());


        var itemToDelete = await _dataMaster.GetCatalogItemById(request.CatalogItemId);
        if (itemToDelete is null)
            return Results.NotFound();

        await _dataMaster.DeleteCatalogItemByIdAsync(request.CatalogItemId);


        return Results.Ok(response);
    }

    public async Task<IResult> HandleAsync(ListPagedCatalogItemRequest request)
    {
        var response = new ListPagedCatalogItemResponse(request.CorrelationId());
        var items = _dataMaster.GetCatalogItems(request.PageIndex, request.PageSize, request.CatalogTypeId, request.CatalogBrandId);

        int totalItems = items.Count;

        response.CatalogItems.AddRange(items);
        foreach (CatalogItemDto item in response.CatalogItems)
        {
            item.PictureUri = _uriComposer.ComposePicUri(item.PictureUri);
        }

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }



        return Results.Ok(response);
    }
}
