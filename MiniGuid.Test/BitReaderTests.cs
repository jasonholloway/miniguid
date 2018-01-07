using System.IO;
using Xunit;
using Shouldly;
using System;

namespace MiniGuid.Test
{
    public class BitReaderTests
    {

        [Fact]
        public void ReadsFullByte()
        {
            var reader = NewReader(new byte[] { 123 });
                        
            reader.Read(8).ShouldBe(123);
        }


        [Fact]
        public void ReadsANibble()
        {
            var reader = NewReader(new byte[] { 0xF0 });

            reader.Read(4).ShouldBe(0xF);
        }

        [Fact]
        public void ReadsTwoNibbles()
        {
            var reader = NewReader(new byte[] { 0xFA });

            reader.Read(4).ShouldBe(0xF);            
            reader.Read(4).ShouldBe(0xA);
        }


        [Fact]
        public void ReadsNibbles_AfterByteBoundary()
        {
            var reader = NewReader(new byte[] { 0xAB, 0xCD });

            reader.Read(8);
            reader.Read(4);
            reader.Read(4).ShouldBe(0xD);
        }


        [Fact]
        public void ReadsOddPortions_FromByte()
        {
            var reader = NewReader(new byte[] { 0xAB });
            
            reader.Read(3).ShouldBe(5);
            reader.Read(5).ShouldBe(11);
        }
        

        [Fact]
        public void ReadsOddPortion_AcrossBytes()
        {
            var reader = NewReader(new byte[] { 0xAB, 0xCD });

            reader.Read(3);
            reader.Read(8).ShouldBe(0x5E);
        }
        

        BitReader NewReader(byte[] bytes)
            => new BitReader(new BinaryReader(new MemoryStream(bytes)));
        
    }
}
