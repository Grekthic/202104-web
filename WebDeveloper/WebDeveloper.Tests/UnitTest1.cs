using System;
using Xunit;

namespace WebDeveloper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ProbarVariasCosasObvias()
        {
            // Comprobar que 1 + 1 es 2
            Assert.Equal(1 + 1, 2);

            // Comprobar que 1 + 3 es mayor a 2
            Assert.Equal(1 + 3 > 2, true);
        }
    }
}
