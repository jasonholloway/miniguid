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

        //so, need to set up some aliases



        static (char, char?)[] _charMap = "abcdefghijklmnopqrstuvwxyzABCDEF"
                                            .Zip("GHIJKLMNOPQRSTUVWXYZ".PadRight(32), 
                                                (c1, c2) => (c1, c2 != ' ' ? (char?)c2 : null))
                                            .ToArray();
                                       
        

        public override string ToString()
        {
            var guidBytes = _guid.ToByteArray();
            var guidBitReader = new BitReader(new BinaryReader(new MemoryStream(guidBytes)));

            var chars = new char[25];
            int acc = 0;

            for (int i = 0; i < 25; i++)
            {
                int chunk = guidBitReader.Read(5);                
                var (c1, c2) = _charMap[chunk];

                acc = (acc + chunk) & 0xF;
                
                chars[i] = c2.HasValue && (acc & 1) == 1
                            ? c2.Value 
                            : c1;
            }

            //and excess 3 bits!!!
            
            return new string(chars);
        }


        public static bool TryParse(string input, out MiniGuid guid)
        {
            guid = Create();
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
