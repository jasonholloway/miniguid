using System;
using System.Linq;
using Xunit;

namespace MiniGuids.Test
{
    public class ParsingTests
    {
        
        [Fact]
        public void CanParse_FromString()
        {
            var success = MiniGuid.TryParse("ABCDEabcdeABCDEabcdeABCDEa", out var miniGuid);
            Assert.True(success);
        }

        [Theory]
        [InlineData("00000000000000000000000000000000")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("{00000000-0000-0000-0000-000000000000}")]
        [InlineData("(00000000-0000-0000-0000-000000000000)")]
        [InlineData("{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}")]
        public void CanParseSystemGuid_FromString(string str)
        {
            var success = MiniGuid.TryParse(str, out var miniGuid);
            Assert.True(success);
        }

        [Theory]
        [InlineData("withNumber123abcdeABCDEa")]
        [InlineData("ABCDEabcdeABCDEabcdeABCDEaTOOBIG")]
        [InlineData("ABCDEabcdeABCDETOOSMALLa")]
        [InlineData("strangeChars_deABCDE+bcdeA")]
        public void FailsToParse_BadString(string str)
        {
            var success = MiniGuid.TryParse(str, out var _);
            Assert.False(success);
        }

        [Theory]
        [InlineData("ABCDEabcdeABCDEabcdeABCDEa")]
        [InlineData("ewdEWDdffewfFEFfweffWwfecS")]
        [InlineData("sfdEWRerFefwFFFFCCSCASOKak")]
        [InlineData("LPOsadJOETPEOvmvQPzPKQZZQk")]
        public void SameString_ParsedAsSameGuid(string str)
        {
            var guids = Enumerable.Range(0, 10)
                        .Select(_ => (Guid)MiniGuid.Parse(str))
                        .ToHashSet();

            Assert.Single(guids);
        }

        [Fact]
        public void ToString_Parse_RoundTrip()
        {
            for(int i = 0; i < 1000; i++)
            {
                var guid1 = MiniGuid.NewGuid();
                var guid2 = MiniGuid.Parse(guid1.ToString());

                Assert.Equal(guid1, guid2);
            }
        }
    }
}
