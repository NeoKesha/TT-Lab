using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;
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
            if (Bitmap != null && !Disposed)
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

        public override ITwinItem Export()
        {
            PS2AnyTexture texture = new PS2AnyTexture();
            var fun = TextureFunction.MODULATE;
            var format = (Bitmap.Width == 256) ? TexturePixelFormat.PSMCT32 : TexturePixelFormat.PSMT8;
            var tex = new List<Twinsanity.TwinsanityInterchange.Common.Color>();
            byte mips = (Bitmap.Width == 256 || Bitmap.Width <= 16) ? 1 : (byte)((int)Math.Log2(Bitmap.Width) - 3);
            if (format == TexturePixelFormat.PSMCT32)
            {
                var bits = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                unsafe
                {
                    byte* source = (byte*)bits.Scan0;
                    for (int i = 0; i < bits.Height; i++)
                    {
                        var scan = source;
                        for (int j = 0; j < bits.Width; j++)
                        {
                            var b = source[0];
                            var g = source[1];
                            var r = source[2];
                            var a = source[3];
                            tex.Add(new Twinsanity.TwinsanityInterchange.Common.Color(r,g,b,a));
                            source += 4;
                        }
                        source = scan + bits.Stride;
                    }
                }
                
            }
            else
            {
                Bitmap IndexedBitmap = Bitmap.Clone(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), PixelFormat.Format8bppIndexed);
                var bits = IndexedBitmap.LockBits(new Rectangle(0, 0, IndexedBitmap.Width, IndexedBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                unsafe
                {
                    byte* source = (byte*)bits.Scan0;
                    for (int i = 0; i < bits.Height; i++)
                    {
                        var scan = source;
                        for (int j = 0; j < bits.Width; j++)
                        {
                            var index = source[0];
                            var color = IndexedBitmap.Palette.Entries[index];
                            tex.Add(new Twinsanity.TwinsanityInterchange.Common.Color(color.R, color.G, color.B, color.A));
                            source += 1;
                        }
                        source = scan + bits.Stride;
                    }
                }
            }
            texture.FromBitmap(tex, Bitmap.Width, mips, fun, format);

            return texture;
        }
    }
}
