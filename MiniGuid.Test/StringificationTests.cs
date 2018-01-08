using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace MiniGuid.Test
{
    public class StringificationTests
    {

        [Fact]
        public void IsAlphabetical_AndOf26Chars()
        {
            for (int i = 0; i < 1000; i++)
            {
                var str = MiniGuid.NewGuid().ToString();
                Assert.Matches("^[a-zA-Z]{26}$", str);
            }
        }
        

        [Fact]
        public void AllChars_AreUsed()
        {
            var allChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToHashSet();

            var usedChars = Enumerable.Range(0, 1000)
                            .SelectMany(_ => MiniGuid.NewGuid().ToString())
                            .ToHashSet();

            Assert.True(usedChars.SetEquals(allChars));
        }


        [Fact]
        public void SameString_AlwaysProduced()
        {
            for (int i = 0; i < 100; i++)
            {
                var miniGuid = MiniGuid.NewGuid();

                var strings = Enumerable.Range(0, 100)
                                .Select(_ => miniGuid.ToString())
                                .ToHashSet();

                Assert.Single(strings);
            }
        }
        
    }
}
