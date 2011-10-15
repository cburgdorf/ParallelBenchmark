using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ParallelBenchmark.EntityFramework;
using ParallelBenchmark.Tests;
using ParallelBenchmark.TestRunner;

namespace ParallelBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            ITestRunner[] testRunners = {
                                            //new TestRunner<TwoTablesSequentialWriteTest>(1)
                                            new TestRunner<TwoTablesParallelLoadTest>(10),
                                            new TestRunner<TwoTablesSequentialLoadTest>(10)
                                        };

            testRunners.ToList().ForEach(x => x.RunTests());
            
            Console.WriteLine("All tests finished...");
            Console.ReadLine();
        }
    }
}
