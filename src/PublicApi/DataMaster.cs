using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using BlazorShared.Models;
using Dapper;
using Elfie.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

namespace Microsoft.eShopWeb.PublicApi;

public class DataMaster
{

    public List<CatalogItemDto> GetCatalogItems(int? pageNumber, int? pageSize, int? catalogTypeId, int? catalogBrandId)
    {
        var connection = GetConnection();
        var queryValues = new { PageNumber = pageNumber, PageSize = pageSize, CatalogTypeId = catalogTypeId, CatalogBrandId = catalogBrandId };
        var procedure = "FetchCatalogItems";
        var result = connection.Query<CatalogItemDto>(procedure, queryValues, commandType: CommandType.StoredProcedure).ToList();
        return result;
    }



    private SqlConnection GetConnection()
    {
        var CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.CatalogDb;";
        var connection = new SqlConnection(CONNECTION_STRING);
        return connection;
    }
}
