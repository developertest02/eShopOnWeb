namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints.Deprecated;

public class DeleteCatalogItemRequest : BaseRequest
{
    public int CatalogItemId { get; init; }

    public DeleteCatalogItemRequest(int catalogItemId)
    {
        CatalogItemId = catalogItemId;
    }
}
