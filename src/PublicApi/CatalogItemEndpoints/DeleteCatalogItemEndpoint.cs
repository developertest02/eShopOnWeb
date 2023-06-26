using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

/// <summary>
/// Deletes a Catalog Item
/// </summary>
public class DeleteCatalogItemEndpoint 
{
    private readonly DataMaster _dataMaster;
    
    public DeleteCatalogItemEndpoint(DataMaster dataMaster)
    {
        _dataMaster = dataMaster;          
    }
    //public void AddRoute(IEndpointRouteBuilder app)
    //{
    //    app.MapDelete("api/catalog-items/{catalogItemId}",
    //        [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
    //        (int catalogItemId, IRepository<CatalogItem> itemRepository) =>
    //        {
    //            return await HandleAsync(new DeleteCatalogItemRequest(catalogItemId), itemRepository);
    //        })
    //        .Produces<DeleteCatalogItemResponse>()
    //        .WithTags("CatalogItemEndpoints");
    //}

    public async Task<IResult> HandleAsync(DeleteCatalogItemRequest request, IRepository<CatalogItem> itemRepository)
    {
        var response = new DeleteCatalogItemResponse(request.CorrelationId());


        var itemToDelete = await _dataMaster.GetCatalogItemById(request.CatalogItemId);
        if (itemToDelete is null)
            return Results.NotFound();

        await _dataMaster.DeleteCatalogItemByIdAsync(request.CatalogItemId);


        return Results.Ok(response);
    }
}
