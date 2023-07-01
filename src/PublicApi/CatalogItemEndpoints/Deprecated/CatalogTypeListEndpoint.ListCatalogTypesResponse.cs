using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints.Deprecated;

public class ListCatalogTypesResponse : BaseResponse
{
    public ListCatalogTypesResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListCatalogTypesResponse()
    {
    }

    public List<CatalogTypeDto> CatalogTypes { get; set; } = new List<CatalogTypeDto>();
}
