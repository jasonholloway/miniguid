using System.IO;
using Xunit;
using Shouldly;
using System.Linq;

namespace MiniGuids.Test
{
    public class BitWriterTests
    {
        byte[] _bytes;
        BitWriter Writer;

        public BitWriterTests()
        {
            _bytes = new byte[100];
            Writer = new BitWriter(new BinaryWriter(new MemoryStream(_bytes)));
        }

        [Fact]
        public void WritesSingleByte()
        {
            Writer.Write(0xAB, 8);
            Bytes[0].ShouldBe(0xAB);
        }

        [Fact]
        public void WritesTwoNibbles()
        {
            Writer.Write(0xD, 4);
            Writer.Write(0xE, 4);
            Bytes[0].ShouldBe(0xDE);
        }

        [Fact]
        public void WritesOddBitsAndBobs()
        {
            Writer.Write(0x2, 3);
            Writer.Write(0xED, 8);
            Writer.Write(0x5, 5);

            Bytes[0].ShouldBe(0x5D);
            Bytes[1].ShouldBe(0xA5);
        }
        

        int[] Bytes => _bytes.Select(b => (int)b).ToArray();

    }
}
