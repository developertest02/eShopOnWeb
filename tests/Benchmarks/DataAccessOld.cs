using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdmin.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Benchmarks;
public class DataAccessOld : IClassFixture<WebApplicationFactory<Microsoft.eShopWeb.Web.Program>>
{

    [Fact]
    public void DoStuff()
    {
        var x = "Y";
        Assert.True(true);

    }
    

    

}
