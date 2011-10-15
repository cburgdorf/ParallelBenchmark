using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParallelBenchmark.EntityFramework;

namespace ParallelBenchmark.Tests
{
    public class TwoTablesParallelWriteTest : AbstractPerformanceTest
    {
        public override string Name
        {
            get { return "Write each row of one table sequential"; }
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
                    {
                        var entities = context.Test2.ToList();

                        Parallel.ForEach(entities, entity =>
                        {
                            using (var saveContext = new Database())
                            {
                                var entity1 = entity;
                                var original = saveContext.Test2.SingleOrDefault(x => x.Feld1 == entity1.Feld1);
                                original.Feld2 = DateTime.Now.ToLongTimeString();
                                original.Feld3 = DateTime.Now.ToShortDateString();
                                
                                //saveContext.Test2.ApplyCurrentValues(entity1);
                                saveContext.SaveChanges();
                            }
                        });
                    }
                };
            }
            
        }
    }
}
