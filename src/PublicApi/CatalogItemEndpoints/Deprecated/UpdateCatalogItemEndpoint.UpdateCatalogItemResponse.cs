using System;
using Microsoft.eShopWeb.ApplicationCore;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints.Deprecated;

public class UpdateCatalogItemResponse : BaseResponse
{
    public UpdateCatalogItemResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateCatalogItemResponse()
    {
    }

    public CatalogItemDto CatalogItem { get; set; }
}
