using System;
using System.IO;
using System.Linq;

namespace MiniGuid
{
    public struct MiniGuid
    {
        readonly Guid _guid;

        public MiniGuid(Guid guid) {
            _guid = guid;
        }
        
        public static implicit operator MiniGuid(Guid guid)
            => new MiniGuid(guid);

        public static implicit operator Guid(MiniGuid miniGuid)
            => miniGuid._guid;



        static (char, char?)[] _bin2Char;
        static int?[] _char2Bin;

        static MiniGuid()
        {
            _bin2Char = "abcdefghijklmnopqrstuvwxyzABCDEF"
                            .Zip("GHIJKLMNOPQRSTUVWXYZ".PadRight(32),
                                (c1, c2) => (c1, c2 != ' ' ? (char?)c2 : null))
                            .ToArray();

            _char2Bin = new int?[256];

            for(int i = 0; i < _bin2Char.Length; i++)
            {
                var (c1, c2) = _bin2Char[i];

                _char2Bin[c1] = i;
                if (c2.HasValue) _char2Bin[c2.Value] = i;
            }
        }
        
                                       
        

        public override string ToString()
        {
            var guidBytes = _guid.ToByteArray();
            var guidBitReader = new BitReader(new BinaryReader(new MemoryStream(guidBytes)));

            var chars = new char[26];
            int acc = 0;

            for (int i = 0; i < 25; i++)
            {
                int chunk = guidBitReader.Read(5);                
                var (c1, c2) = _bin2Char[chunk];

                acc = (acc + chunk) & 0xF;
                
                chars[i] = c2.HasValue && (acc & 1) == 1
                            ? c2.Value 
                            : c1;
            }

            {
                int chunk = guidBitReader.Read(3);
                var (c1, c2) = _bin2Char[chunk];
                chars[25] = c1;
            }
                        
            return new string(chars);
        }


        public static bool TryParse(string input, out MiniGuid guid)
        {
            if (input.Length != 26) return false;

            var guidBytes = new byte[16];
            var bitWriter = new BitWriter(new BinaryWriter(new MemoryStream(guidBytes)));

            for(int i = 0; i < input.Length - 1; i++)
            {
                var c = input[i];
                var chunk = _char2Bin[c];
                if (!chunk.HasValue) return false;

                bitWriter.Write(chunk.Value, 5);
            }

            {
                var c = input[25];
                var chunk = _char2Bin[c];
                if (!chunk.HasValue) return false;

                bitWriter.Write(chunk.Value, 3);
            }
            
            guid = new MiniGuid(new Guid(guidBytes));

            return true;
        }

        public static MiniGuid Parse(string input)
        {
            if (TryParse(input, out var miniGuid)) return miniGuid;
            else throw new InvalidOperationException();
        }



        public static MiniGuid Create()
            => Guid.NewGuid();
    }
}
