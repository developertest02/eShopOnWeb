//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.eShopWeb.ApplicationCore;
//using Microsoft.eShopWeb.ApplicationCore.Entities;
//using Microsoft.eShopWeb.ApplicationCore.Interfaces;
//using MinimalApi.Endpoint;

//namespace Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;

///// <summary>
///// List Catalog Brands
///// </summary>
//public class CatalogBrandListEndpoint : IEndpoint<IResult, IRepository<CatalogBrand>>
//{

//    private readonly IDataMaster _dataMaster;
//    public CatalogBrandListEndpoint(IDataMaster dataMaster)
//    {
//        _dataMaster = dataMaster;
//    }

//    public void AddRoute(IEndpointRouteBuilder app)
//    {
//        app.MapGet("api/catalog-brands",
//            async (IRepository<CatalogBrand> catalogBrandRepository) =>
//            {
//                return await HandleAsync(catalogBrandRepository);
//            })
//           .Produces<ListCatalogBrandsResponse>()
//           .WithTags("CatalogBrandEndpoints");
//    }

//    public async Task<IResult> HandleAsync(IRepository<CatalogBrand> catalogBrandRepository)
//    {
//        var response = new ListCatalogBrandsResponse();

//        var items = _dataMaster.GetCatalogBrands();

//        response.CatalogBrands.AddRange(items);

//        return Results.Ok(response);
//    }
//}
