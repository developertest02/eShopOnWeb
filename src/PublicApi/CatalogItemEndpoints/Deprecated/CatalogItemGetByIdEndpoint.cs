﻿//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.eShopWeb.ApplicationCore.Entities;
//using Microsoft.eShopWeb.ApplicationCore.Interfaces;
//using MinimalApi.Endpoint;

//namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

///// <summary>
///// Get a Catalog Item by Id
///// </summary>
//public class CatalogItemGetByIdEndpoint
//{
//    private readonly IUriComposer _uriComposer;
//    private readonly DataMaster _dataMaster;
//    public CatalogItemGetByIdEndpoint(IUriComposer uriComposer, DataMaster dataMaster)
//    {
//        _uriComposer = uriComposer;
//        _dataMaster = dataMaster;
//    }

//    public void AddRoute(IEndpointRouteBuilder app)
//    {
//        app.MapGet("api/catalog-items/{catalogItemId}",
//            async (int catalogItemId, IRepository<CatalogItem> itemRepository) =>
//            {
//                return await HandleAsync(new GetByIdCatalogItemRequest(catalogItemId), itemRepository);
//            })
//            .Produces<GetByIdCatalogItemResponse>()
//            .WithTags("CatalogItemEndpoints");
//    }

//    public async Task<IResult> HandleAsync(GetByIdCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
//    {
//        var response = new GetByIdCatalogItemResponse(request.CorrelationId());
//        var item = await _dataMaster.GetCatalogItemById(request.CatalogItemId);
//        //var item = await itemRepository.GetByIdAsync(request.CatalogItemId);
//        if (item is null)
//            return Results.NotFound();

//        response.CatalogItem = new CatalogItemDto
//        {
//            Id = item.Id,
//            CatalogBrandId = item.CatalogBrandId,
//            CatalogTypeId = item.CatalogTypeId,
//            Description = item.Description,
//            Name = item.Name,
//            PictureUri = _uriComposer.ComposePicUri(item.PictureUri),
//            Price = item.Price
//        };
//        return Results.Ok(response);
//    }
//}
