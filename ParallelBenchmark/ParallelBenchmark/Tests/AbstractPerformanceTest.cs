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
            var action = Runner;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
