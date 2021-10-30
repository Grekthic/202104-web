using System;
using System.Collections.Generic;
using System.Text;

namespace WebDeveloper.DbOperations
{
    public class TestDisposable : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Se esta destruyendo el objeto");
        }
    }
}
