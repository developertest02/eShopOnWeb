using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

namespace Benchmarks;
public class DataBenchmarks
{
    private readonly DataAccessOld _dataAccessOld = new DataAccessOld();
    [Benchmark]
    public ListPagedCatalogItemResponse GetCatalogItems() => _dataAccessOld.DoBenchmark();
}
