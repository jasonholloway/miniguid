using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace MiniGuid.Test
{
    public class StringificationTests
    {

        [Fact]
        public void IsAlphabetical_AndOf25Chars()
        {
            for (int i = 0; i < 1000; i++)
            {
                var str = MiniGuid.Create().ToString();
                Assert.True(_reAlpha25.IsMatch(str), $"{str} doesn't match {_reAlpha25}");
            }
        }


        [Fact]
        public void AllChars_AreUsed()
        {
            var allChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToHashSet();

            var usedChars = Enumerable.Range(0, 1000)
                            .SelectMany(_ => MiniGuid.Create().ToString())
                            .ToHashSet();

            Assert.True(usedChars.SetEquals(allChars));
        }


        [Fact]
        public void SameString_AlwaysProduced()
        {
            for (int i = 0; i < 100; i++)
            {
                var miniGuid = MiniGuid.Create();

                var strings = Enumerable.Range(0, 100)
                                .Select(_ => miniGuid.ToString())
                                .ToHashSet();

                Assert.Single(strings);
            }
        }
        
        static Regex _reAlpha25 = new Regex("^[a-zA-Z]{25}$", RegexOptions.Compiled);
        
    }
}
