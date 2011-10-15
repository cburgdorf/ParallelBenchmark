using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParallelBenchmark.EntityFramework;

namespace ParallelBenchmark.Tests
{
    public class TwoTablesParallelLoadTest : AbstractPerformanceTest
    {
        public override string Name
        {
            get { return "Load two tables parallel"; }
        }

        public override Action Runner
        {
            get {
                return () =>
                           {
                               //we need to contexts because the context is not thread safe
                               //and throws funny erros otherwise

                               Parallel.Invoke(
                                       () =>
                                       {
                                           using (var context = new Database())
                                               context.Test.ToList();
                                       },
                                       () =>
                                       {
                                           using (var context = new Database())
                                               context.Test2.ToList();
                                       });
                               //Parallel.Invoke is a blocking call. So, at this point
                               //we fetched both tables as if the previous lines were
                               //sychronous calls.
                           };
            }
        }
    }
}
