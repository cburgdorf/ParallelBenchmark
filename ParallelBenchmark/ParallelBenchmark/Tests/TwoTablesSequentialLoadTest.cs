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
                    //we create two contexts even if one would be enough
                    //thats because we need two contextes for the *parallel*
                    //version and we don't want to have this affect the results

                    using (var context = new Database())
                        context.Test.ToList();

                    using (var context = new Database())
                        context.Test2.ToList();
                };
            }
            
        }
    }
}
