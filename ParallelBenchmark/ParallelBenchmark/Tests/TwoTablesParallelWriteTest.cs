using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParallelBenchmark.EntityFramework;

namespace ParallelBenchmark.Tests
{
    public class TwoTablesParallelWriteTest : AbstractPerformanceTest
    {
        private object _entityLock = new object();

        public override string Name
        {
            get { return "Write each row of one table parallel"; }
        }

        public override Action Runner
        {
            get
            {
                return () =>
                {
                    using (var context = new Database())
                    {
                        var entities = context.Test2.ToList();

                        Parallel.ForEach(entities, entity =>
                        {
                            //we *could* avoid the lock by creating a new context. However in practice
                            //you might want to inject the repositories in favor for testability
                            //and therefore can not create it on the fly

                            lock (_entityLock)
                            {
                                entity.Feld2 = DateTime.Now.ToLongTimeString();
                                entity.Feld3 = "test-p";
                            }
                            Update(entity);
                        });
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
