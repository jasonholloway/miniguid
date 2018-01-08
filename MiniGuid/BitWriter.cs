using System.IO;

namespace MiniGuids
{
    public class BitWriter
    {
        BinaryWriter _byteWriter;
        int _bitBuffer = 0;
        int _bitBufferSize = 0;

        public BitWriter(BinaryWriter byteWriter)
        {
            _byteWriter = byteWriter;
        }

        public void Write(int bits, int bitCount)   //bitCount can't be more than 16!
        {
            _bitBuffer <<= bitCount;
            _bitBuffer |= (bits & BitMask(bitCount));
            _bitBufferSize += bitCount;

            if(_bitBufferSize >= 8) {
                var nextByte = (byte)(_bitBuffer >> (_bitBufferSize - 8));
                _byteWriter.Write(nextByte);

                _bitBufferSize -= 8;
                _bitBuffer &= BitMask(_bitBufferSize);
            }            
        }


        int BitMask(int count)
            => ~(0xFFFFFFF << count);

    }
}
