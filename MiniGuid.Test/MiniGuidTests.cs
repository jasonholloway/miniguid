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

#pragma warning disable CS1718 // Comparison made to same variable

        [Fact]
        public void Equatable_WithOtherMiniGuids_ViaOperator()
        {
            var miniGuid1 = MiniGuid.NewGuid();
            var miniGuid2 = MiniGuid.NewGuid();

            Assert.True(miniGuid1 == miniGuid1);
            Assert.False(miniGuid1 == miniGuid2);
            Assert.True(miniGuid1 != miniGuid2);
            Assert.False(miniGuid1 != miniGuid1);
        }
        
        [Fact]
        public void Equatable_WithOtherMiniGuids_ViaEquals()
        {
            object miniGuid1 = MiniGuid.NewGuid();
            object miniGuid2 = MiniGuid.NewGuid();

            Assert.True(miniGuid1.Equals(miniGuid1));
            Assert.False(miniGuid1.Equals(miniGuid2));
        }


        [Fact]
        public void Equatable_WithNormalGuids_ViaOperator()
        {
            var miniGuid = MiniGuid.NewGuid();

            var guid1 = (Guid)miniGuid;
            var guid2 = Guid.NewGuid();
            
            Assert.True(miniGuid == guid1);
            Assert.False(miniGuid == guid2);
            Assert.True(miniGuid != guid2);
            Assert.False(miniGuid != guid1);
        }

        [Fact]
        public void Equatable_WithNormalGuids_ViaEquals()
        {
            var miniGuid = MiniGuid.NewGuid();

            object guid1 = (Guid)miniGuid;
            object guid2 = Guid.NewGuid();

            Assert.True(miniGuid.Equals(guid1));
            Assert.False(miniGuid.Equals(guid2));
        }



        [Fact]
        public void Equatable_WithString_ViaOperator()
        {
            var miniGuid = MiniGuid.NewGuid();

            var str1 = miniGuid.ToString();
            var str2 = MiniGuid.NewGuid().ToString();

            Assert.True(miniGuid == str1);
            Assert.False(miniGuid == str2);
            Assert.True(miniGuid != str2);
            Assert.False(miniGuid != str1);
        }

        [Fact]
        public void Equatable_WithString_ViaEquals()
        {
            var miniGuid = MiniGuid.NewGuid();

            object str1 = miniGuid.ToString();
            object str2 = MiniGuid.NewGuid().ToString();

            Assert.True(miniGuid.Equals(str1));
            Assert.False(miniGuid.Equals(str2));
        }


#pragma warning restore CS1718 // Comparison made to same variable
        
    }
}
