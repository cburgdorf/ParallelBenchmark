using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ParallelBenchmark.EntityFramework;

namespace ParallelBenchmark.Tests
{
    public class TwoTablesSequentialLoadTest : AbstractPerformanceTest
    {
        public override string Name
        {
            get { return "Load two tables sequential"; }
        }

        public override Action Runner
        {
            get
            {
                return () =>
                {
                    using (var context = new Database())
                        context.Test.ToList();

                    using (var context = new Database())
                        context.Test2.ToList();
                };
            }
            
        }
    }
}
