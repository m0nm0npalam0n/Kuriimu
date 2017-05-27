﻿using System;
using System.Drawing;
using System.IO;
using Kuriimu.Contract;
using Kuriimu.IO;

namespace image_tmx
{
    public sealed class TmxAdapter : IImageAdapter
    {
        private TMX _tmx = null;

        #region Properties

        public string Name => "TMX";
        public string Description => "Atlus Texture Matrix";
        public string Extension => "*.tmx";
        public string About => "This is the TMX image adapter for Kukkii.";

        // Feature Support
        public bool FileHasExtendedProperties => false;
        public bool CanSave => true;

        public FileInfo FileInfo { get; set; }

        #endregion

        public bool Identify(string filename)
        {
            using (var br = new BinaryReaderX(File.OpenRead(filename)))
            {
                if (br.BaseStream.Length < 12) return false;
                br.BaseStream.Position = 8;
                return br.ReadString(4) == "TMX0";
            }
        }

        public void Load(string filename)
        {
            FileInfo = new FileInfo(filename);

            if (FileInfo.Exists)
                _tmx = new TMX(FileInfo.OpenRead());
        }

        public void Save(string filename = "")
        {
            if (filename.Trim() != string.Empty)
                FileInfo = new FileInfo(filename);

            try
            {
                _tmx.Save(FileInfo.FullName);
            }
            catch (Exception) { }
        }

        // Bitmaps
        public Bitmap Bitmap
        {
            get => _tmx.bmp;
            set => _tmx.bmp = value;
        }

        public bool ShowProperties(Icon icon) => false;
    }
}
