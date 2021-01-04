﻿using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using static Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics.PS2AnyTexture;

namespace TT_Lab.AssetData.Graphics
{
    public class TextureData : AbstractAssetData
    {
        public TextureData()
        {
        }

        public TextureData(PS2AnyTexture texture) : this()
        {
            twinRef = texture;
        }
        public Bitmap Bitmap;
        protected override void Dispose(Boolean disposing)
        {
            if (!Disposed && Bitmap != null)
            {
                Bitmap.Dispose();
            }
        }
        public override void Save(string dataPath)
        {
            if (Bitmap != null && !Disposed)
            {
                Bitmap.Save(dataPath, ImageFormat.Png);
            }
        }

        public override void Load(String dataPath)
        {
            Bitmap = new Bitmap(Bitmap.FromFile(dataPath));
        }

        public override void Import()
        {
            PS2AnyTexture texture = (PS2AnyTexture)twinRef;
            if (texture.TextureFormat == TexturePixelFormat.PSMCT32 || texture.TextureFormat == TexturePixelFormat.PSMT8)
            {
                Int32 width = (Int32)Math.Pow(2, texture.ImageWidthPower);
                Int32 height = (Int32)Math.Pow(2, texture.ImageHeightPower);
                texture.CalculateData();

                var Bits = new UInt32[width * height];
                var BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
                var tmpBmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());

                for (var x = 0; x < width; ++x)
                {
                    for (var y = 0; y < height; ++y)
                    {
                        var dstx = x;
                        var dsty = height - 1 - y;
                        Bits[dstx + dsty * width] = texture.Colors[x + y * width].ToARGB();
                    }
                }

                Bitmap = new Bitmap(tmpBmp);
                tmpBmp.Dispose();
                BitsHandle.Free();
            }
        }
    }
}
