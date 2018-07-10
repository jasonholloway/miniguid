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
        [InlineData("6fa7b0bf65774e998f73f64e867fe236")]
        [InlineData("6fa7b0bf-6577-4e99-8f73-f64e867fe236")]
        [InlineData("{6fa7b0bf-6577-4e99-8f73-f64e867fe236}")]
        [InlineData("(6fa7b0bf-6577-4e99-8f73-f64e867fe236)")]
        [InlineData("{0x6fa7b0bf,0x6577,0x4e99,{0x8f,0x73,0xf6,0x4e,0x86,0x7f,0xe2,0x36}}")]
        public void CanParseSystemGuid_FromString(string str)
        {
            var expected = (MiniGuid)Guid.Parse("6fa7b0bf-6577-4e99-8f73-f64e867fe236");

            var success = MiniGuid.TryParse(str, out var miniGuid);

            Assert.True(success);
            Assert.Equal(expected, miniGuid);
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
            for (int i = 0; i < 1000; i++)
            {
                var guid1 = MiniGuid.NewGuid();
                var guid2 = MiniGuid.Parse(guid1.ToString());

                Assert.Equal(guid1, guid2);
            }
        }
    }
}
