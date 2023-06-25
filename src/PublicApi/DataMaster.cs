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
using Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.eShopWeb.PublicApi.CatalogTypeEndpoints;

namespace Microsoft.eShopWeb.PublicApi;

public class DataMaster
{
    
    public ApplicationCore.Entities.CatalogItem AddNewCatalogItem(ApplicationCore.Entities.CatalogItem source)
    {
        var connection = GetConnection();
        // Define a parameter for the inserted Id value
        var insertedId = new SqlParameter("@InsertedId", SqlDbType.Int)
        {
            Direction = ParameterDirection.ReturnValue
        };

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var parameters = new
        {
            Name = source.Name,
            Description = source.Description,
            Price = source.Price,
            PictureUri = source.PictureUri,
            CatalogTypeId = source.CatalogTypeId,
            CatalogBrandId = source.CatalogBrandId
        };
        dynamicParameters.AddDynamicParams(parameters);

        // Execute the stored procedure using Dapper
        var result = connection.Execute("AddCatalogItem",
            dynamicParameters,
            null,
            null,
            CommandType.StoredProcedure);
        var id = dynamicParameters.Get<int>("@InsertedId");
        source.SetId(id);
        return source;


    }
    
    public async Task DeleteCatalogItemByIdAsync(int catalogItemId)
    {
        var connection = GetConnection();
        _ = await connection.ExecuteAsync("DeleteCatalogItem", new { Id = catalogItemId }, commandType: CommandType.StoredProcedure);

    }
    public List<CatalogItemDto> GetCatalogItems(int? pageNumber, int? pageSize, int? catalogTypeId, int? catalogBrandId)
    {
        var connection = GetConnection();
        var queryValues = new { PageNumber = pageNumber, PageSize = pageSize, CatalogTypeId = catalogTypeId, CatalogBrandId = catalogBrandId };
        var procedure = "FetchCatalogItems";
        var result = connection.Query<CatalogItemDto>(procedure, queryValues, commandType: CommandType.StoredProcedure).ToList();
        return result;
    }

    
    public async Task<CatalogItemDto> GetCatalogItemById(int id)
    {
        var connection = GetConnection();
        var result = await connection.QuerySingleOrDefaultAsync<CatalogItemDto>("SELECT * FROM Catalog WHERE Id = @Id", new { Id = id });
        return result;
    }
    public List<CatalogBrandDto> GetCatalogBrands()
    {
        var connection = GetConnection();
        var result = connection.Query<CatalogBrandDto>("SELECT Id, Brand as Name FROM CatalogBrands");
        return result.ToList();
        
    }

    public List<CatalogTypeDto> GetCatalogTypes()
    {
        var connection = GetConnection();
        var result = connection.Query<CatalogTypeDto>("SELECT Id, Type as Name FROM CatalogTypes").ToList();
        return result;
    }

    public void UpdateCatalogItem(ApplicationCore.Entities.CatalogItem source)
    {
        var connection = GetConnection();
        var dynamicParameters = new DynamicParameters();
        var parameters = new
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            Price = source.Price,
            PictureUri = source.PictureUri,
            CatalogTypeId = source.CatalogTypeId,
            CatalogBrandId = source.CatalogBrandId
        };
        dynamicParameters.AddDynamicParams(parameters);

        // Execute the stored procedure using Dapper
        var result = connection.Execute("UpdateCatalogItem",
            dynamicParameters,
            null,
            null,
            CommandType.StoredProcedure);


    }
    private SqlConnection GetConnection()
    {
        var CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.CatalogDb;";
        var connection = new SqlConnection(CONNECTION_STRING);
        return connection;
    }
}
