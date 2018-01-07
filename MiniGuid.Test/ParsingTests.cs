using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace MiniGuid.Test
{
    public class ParsingTests
    {
        
        [Fact]
        public void CanParse_FromString()
        {
            var success = MiniGuid.TryParse("ABCDEabcdeABCDEabcdeABCDEa", out var miniGuid);
            Assert.True(success);
        }


        [Fact(Skip = "To do")]
        public void FailsToParse_BadString()
        {
            throw new NotImplementedException();
        }
        

        [Fact]
        public void ToString_Parse_RoundTrip()
        {
            for(int i = 0; i < 1000; i++)
            {
                var guid1 = MiniGuid.Create();
                var guid2 = MiniGuid.Parse(guid1.ToString());

                Assert.Equal(guid1, guid2);
            }
        }


    }
}
