using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Benchmarks;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Xunit.Abstractions;

namespace ApplicationTests.Benchmarks;
public class DataAccessTests : PerformanceTestBase
{
    public DataAccessTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void DoStuff()
    {
        System.Diagnostics.Debugger.Launch();
        var logger = new AccumulationLogger();

        var config = ManualConfig.Create(DefaultConfig.Instance)
            .AddLogger(logger)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);

        BenchmarkRunner.Run<DataBenchmarks>(config);

        // write benchmark summary
        _output.WriteLine(logger.GetLog());
    }
}
