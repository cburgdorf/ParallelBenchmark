using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ParallelBenchmark.EntityFramework;

namespace ParallelBenchmark.Tests
{
    public class TwoTablesSequentialWriteTest : AbstractPerformanceTest
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

                        foreach (var entity in entities)
                        {
                            //entity.Feld2 = DateTime.Now.ToLongTimeString();
                            entity.Feld2 = "test-s";
                            entity.Feld3 = DateTime.Now.ToShortDateString();

                            Update(entity);
                        }
                    }
                        
                };
            }
            
        }

        public void Update(Test2 entity)
        {
            using (var saveContext = new Database())
            {
                var original = saveContext.Test2.SingleOrDefault(x => x.Feld1 == entity.Feld1);

                saveContext.Test2.ApplyCurrentValues(entity);
                saveContext.SaveChanges();
            }
        }
    }
}
