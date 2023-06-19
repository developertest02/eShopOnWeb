using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;

namespace ApplicationTests;

public class UnitTest1
{
    
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
}
