using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MiniGuids.Test
{
    public class MiniGuidTests
    {
        [Fact]
        public void GuidConversion_RoundTrip()
        {
            var guid1 = Guid.NewGuid();

            var miniGuid = (MiniGuid)guid1;

            var guid2 = (Guid)miniGuid;

            Assert.Equal(guid1, guid2);
        }

        [Fact]
        public void Create_CreatesNoDuplicates()
        {
            var counts = Enumerable.Range(0, 1000)
                            .Select(_ => MiniGuid.NewGuid())
                            .GroupBy(m => m)
                            .Select(g => g.Count())
                            .ToHashSet();

            Assert.Equal(new HashSet<int>(new[] { 1 }), counts);
        }
        

        [Fact]
        public void ImplicitlyConverts_ToString()
        {
            string str = MiniGuid.NewGuid();
        }


        [Fact]
        public void ImplicitlyConverts_FromString()
        {
            MiniGuid miniGuid = "aaaaabbbbbcccccdddddeeeeef";
        }



        [Fact]
        public void StringConversion_RoundTrip()
        {
            for(int i = 0; i < 100; i++)
            {
                var miniGuid1 = MiniGuid.NewGuid();

                var @string = (string)miniGuid1;
                var miniGuid2 = (MiniGuid)@string;

                Assert.Equal(miniGuid1, miniGuid2);
            }
        }



        //also, equality tests
        //and comparison tests
    }
}
