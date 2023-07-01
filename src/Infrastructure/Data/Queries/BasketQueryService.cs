using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Infrastructure.Data.Queries;

public class BasketQueryService : IBasketQueryService
{

    private readonly IDataMaster _dataMaster;
    public BasketQueryService(IDataMaster dataMaster)
    {
        _dataMaster = dataMaster;
    }

    /// <summary>
    /// This method performs the sum on the database rather than in memory
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<int> CountTotalBasketItems(string username)
    {
        int result = 0;
        var basket = await _dataMaster.FetchBasketForBuyer(username);
        if(basket != null)
        {
            result = basket.Items.Sum(n => n.Quantity);
        }
        return result;
    }
}
