using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cetera.IO;

namespace Cetera.Compression
{
    public class LZSS
    {
        public static byte[] Decompress(Stream instream, long decompressedLength)
        {
            using (BinaryReaderX br = new BinaryReaderX(instream, true))
            {
                //throw new Exception(br.BaseStream.Position.ToString());
                List<byte> result = new List<byte>();

                for (int i = 0, flags = 0; ; i++)
                {
                    if (i % 8 == 0) flags = br.ReadByte();
                    if ((flags & 0x80) == 0) result.Add(br.ReadByte());
                    else
                    {
                        int lengthDist = BitConverter.ToUInt16(br.ReadBytes(2).Reverse().ToArray(), 0);
                        int offs = lengthDist % 4096 + 1;
                        int length = lengthDist / 4096 + 3;
                        while (length > 0)
                        {
                            result.Add(result[result.Count - offs]);
                            length--;
                        }
                    }

                    if (result.Count == decompressedLength)
                    {
                        return result.ToArray();
                    }
                    else if (result.Count > decompressedLength)
                    {
                        throw new InvalidDataException("Went past the end of the stream");
                    }
                    flags <<= 1;
                }
            }
        }
    }
}
