using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ParallelBenchmark.Tests
{
    public abstract class AbstractPerformanceTest
    {
        public abstract string Name { get; }
        public abstract Action Runner { get; }
        
        public long RunTest()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Runner();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
