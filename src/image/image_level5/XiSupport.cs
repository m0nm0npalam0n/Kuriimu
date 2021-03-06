﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Kuriimu.Kontract;
using Kuriimu.IO;
using Kuriimu.Compression;

namespace image_level5.imgc
{
    public enum Format : byte
    {
        RGBA8888, RGBA4444,
        RGBA5551, RGB888, RGB565,
        LA88 = 11, LA44, L8, HL88, A8,
        L4 = 26, A4, ETC1, ETC1A4
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header
    {
        public Magic magic; // IMGC
        public int const1; // 30 30 00 00
        public short const2; // 30 00
        public Format imageFormat;
        public byte const3; // 01
        public byte combineFormat;
        public byte bitDepth;
        public short bytesPerTile;
        public short width;
        public short height;
        public int const4; // 30 00 00 00
        public int const5; // 30 00 01 00
        public int tableDataOffset; // always 0x48
        public int const6; // 03 00 00 00
        public int const7; // 00 00 00 00
        public int const8; // 00 00 00 00
        public int const9; // 00 00 00 00
        public int const10; // 00 00 00 00
        public int tableSize1;
        public int tableSize2;
        public int imgDataSize;
        public int const11; // 00 00 00 00
        public int const12; // 00 00 00 00
    }

    public class ImgcSupport
    {
        public static byte[] Decomp(BinaryReaderX br)
        {
            // above to be restored eventually with some changes to Cetera
            return Level5.Decompress(br.BaseStream);
        }
    }
}
