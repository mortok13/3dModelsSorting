using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork3_DB
{
    public static class Startup
    {
        public static void Configure()
        {
            // todo migration in runtime;
            //new ApplicationContext().Database.MigrateAsync()
        }
    }
}
