using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints.Deprecated;

namespace Benchmarks;
public class DataBenchmarks
{
    private readonly DataAccess _dataAccessOld = new DataAccess();
    [Benchmark]
    public ListPagedCatalogItemResponse GetCatalogItems() => _dataAccessOld.DoOldBenchmark();
}
