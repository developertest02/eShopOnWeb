using System.Data;
using System.Data.Common;

using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using NuGet.Protocol.Plugins;
using static Microsoft.eShopWeb.PublicApi.DataMaster;

namespace ApplicationTests;

public class UnitTest1
{
    private const string CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.CatalogDb;";

    [Fact]
    public void Test1()
    {
        var contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=Microsoft.eShopOnWeb.CatalogDb;")
            .Options;
        var ctx = new CatalogContext(contextOptions);
        var catalogItems = ctx.CatalogItems.ToList();
        catalogItems.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void DapperTest()
    {
        var connection = new SqlConnection(CONNECTION_STRING);
        var procedure = "FetchCatalogItems";
        var values = new { PageNumber = 0, PageSize = 10 }; 
        var catalogItems = connection.Query<CatalogItemDto>(procedure, values, commandType: CommandType.StoredProcedure).ToList();
        catalogItems.Count().Should().Be(10);

        var newValues = new { CatalogTypeId = 1, PageNumber = 0, PageSize = 10 };
        catalogItems = connection.Query<CatalogItemDto>(procedure, newValues, commandType: CommandType.StoredProcedure).ToList();
        catalogItems.Count.Should().BeGreaterThan(0);
        catalogItems.Any(n => n.CatalogTypeId != 1).Should().BeFalse();

        int? pageNumber = null;
        int? pageSize = null;
        int? catalogTypeId = null;
        int? catalogBrandId = null;
        var newestValues = new { PageNumber = pageNumber, PageSize = pageSize, CatalogTypeId = catalogTypeId, CatalogBrandId = catalogBrandId };
        catalogItems = connection.Query<CatalogItemDto>(procedure, newestValues, commandType: CommandType.StoredProcedure).ToList();
        catalogItems.Count.Should().BeGreaterThan(0);
        catalogItems.Count.Should().BeGreaterThan(10);

        


    }
}

