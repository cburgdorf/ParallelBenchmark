using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParallelBenchmark.Tests;

namespace ParallelBenchmark.TestRunner
{
    public class TestRunner<T> : ITestRunner where T : AbstractPerformanceTest, new ()
    {
        private readonly int _iterations;
        public TestRunner(int iterations)
        {
            _iterations = iterations < 0 ? 1 : iterations;
        }

        public void RunTests()
        {
            Console.WriteLine("********************************************");
            Console.WriteLine(string.Format("TEST: {0}", new T().Name));
            Console.WriteLine(string.Format("ITERATIONS: {0}", _iterations));
            Console.WriteLine("Please wait...");

            long elapsedMilliseconds = 0;
            for (int i = 0; i < _iterations; i++)
            {
                var test = new T();
                elapsedMilliseconds += test.RunTest();
            }

            var averageTime = elapsedMilliseconds/_iterations;
            Console.WriteLine(string.Format("TIME (AVG): {0}", averageTime));
            Console.WriteLine("********************************************");
            Console.WriteLine(string.Empty);
        }
    }
}
