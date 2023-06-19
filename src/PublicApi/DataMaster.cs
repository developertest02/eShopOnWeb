using System.Collections.Generic;
using System.Linq;
using BlazorShared.Models;
using Microsoft.eShopWeb.Infrastructure.Data;

namespace Microsoft.eShopWeb.PublicApi;

public class DataMaster
{
    private readonly CatalogContext _catalogConetext;
    public DataMaster(CatalogContext catalogConetext)
    {
        _catalogConetext = catalogConetext;
    }

    public List<CatalogItem> GetCatalogItems()
    {
        var items = _catalogConetext.CatalogItems.ToList();
        var result = new List<CatalogItem>();
        items.ForEach(source => {

            var model = new CatalogItem();
            model.PictureUri = source.PictureUri;
            
        
        
        });
        return result;
    }
}
