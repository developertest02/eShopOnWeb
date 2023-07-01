using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints.Deprecated;

public class ListCatalogBrandsResponse : BaseResponse
{
    public ListCatalogBrandsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListCatalogBrandsResponse()
    {
    }

    public List<CatalogBrandDto> CatalogBrands { get; set; } = new List<CatalogBrandDto>();
}
