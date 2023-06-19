using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ApplicationTests.Benchmarks;
public class PerformanceTestBase
{
    protected readonly ITestOutputHelper _output;
    public PerformanceTestBase(ITestOutputHelper output)
    {
        _output = output;
    }
}
