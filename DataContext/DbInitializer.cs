using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext
{
    public static class DbInitializer
    {

        public static void Initialize(PLSODb context)
        {
            context.Database.EnsureCreated();
        }

    }
}
