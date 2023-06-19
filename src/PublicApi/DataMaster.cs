using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using BlazorShared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;

namespace Microsoft.eShopWeb.PublicApi;

public class DataMaster
{
    private readonly CatalogContext _catalogConetext;
    public DataMaster(CatalogContext catalogConetext)
    {
        _catalogConetext = catalogConetext;
    }

    public async Task<List<CatalogItem>> GetCatalogItems()
    {
        var items = await  _catalogConetext.CatalogItems.ToListAsync();
        var result = new List<CatalogItem>();
        items.ForEach(source => {

            var model = new CatalogItem();
            model.PictureUri = source.PictureUri;
        
        });
        return result;
    }
}
