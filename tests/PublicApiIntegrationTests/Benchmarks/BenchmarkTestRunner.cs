using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Xunit.Abstractions;

namespace PublicApiIntegrationTests.Benchmarks;

public class BenchmarkTestRunner
{
    protected readonly ITestOutputHelper _output;
    public BenchmarkTestRunner(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void DoMyStuff()
    {
        var logger = new AccumulationLogger();

        var config = ManualConfig.Create(DefaultConfig.Instance)
            .AddLogger(logger)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator);
        BenchmarkRunner.Run<EndPointBenchmarks>(config);

        //write benchmark summary
        _output.WriteLine(logger.GetLog());
    }
}
