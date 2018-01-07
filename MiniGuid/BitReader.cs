using System;
using System.IO;
using System.Text;

namespace MiniGuid
{
    
    public class BitReader
    {
        BinaryReader _byteReader;
        byte[] _byteBuffer = new byte[1];

        int _bitBuffer = 0;
        int _bitBufferLength = 0;
        
        public BitReader(BinaryReader byteReader)
        {
            _byteReader = byteReader;
        }

        public int Read(int count)
        {
            int output = 0;
            int bitsToRead = count;

            while (bitsToRead > 0)
            {
                if (_bitBufferLength == 0)
                {
                    var bytesRead = _byteReader.Read(_byteBuffer, 0, 1);
                    if (bytesRead == 0) throw new EndOfStreamException();

                    _bitBuffer = _byteBuffer[0];
                    _bitBufferLength = 8;
                }

                var chunkSize = Math.Min(_bitBufferLength, bitsToRead);
                _bitBufferLength -= chunkSize;

                var chunk = _bitBuffer >> _bitBufferLength;
                _bitBuffer &= BitMask(_bitBufferLength);
                
                chunk <<= (bitsToRead - chunkSize);
                output |= chunk;
                
                bitsToRead -= chunkSize;                
            }

            return output;
        }


        int BitMask(int count)
            => ~(0xFFFFFFF << count);
    }

}
