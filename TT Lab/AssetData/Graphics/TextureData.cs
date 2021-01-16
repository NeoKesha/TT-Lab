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
            Export();
        }

        public override ITwinItem Export()
        {
            Bitmap bmpWithMips = GenerateMipsForBitmap(Bitmap);
            PS2AnyTexture texture = new PS2AnyTexture();
            var fun = TextureFunction.MODULATE;
            var format = (Bitmap.Width == 256) ? TexturePixelFormat.PSMCT32 : TexturePixelFormat.PSMT8;
            var tex = new List<Twinsanity.TwinsanityInterchange.Common.Color>();
            byte mips = (Bitmap.Width == 256 || Bitmap.Width <= 16) ? 1 : (byte)((int)Math.Log2(Bitmap.Width) - 3);
            if (format == TexturePixelFormat.PSMCT32)
            {
                var bits = bmpWithMips.LockBits(new Rectangle(0, 0, bmpWithMips.Width, bmpWithMips.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
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
                Bitmap IndexedBitmap = bmpWithMips.Clone(new Rectangle(0, 0, bmpWithMips.Width, bmpWithMips.Height), PixelFormat.Format8bppIndexed);
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
                            source += 4;
                        }
                        source = scan + bits.Stride;
                    }
                }
            }
            texture.FromBitmap(tex, Bitmap.Width, mips, fun, format);

            return texture;
        }

        private Bitmap GenerateMipsForBitmap(Bitmap source)
        {
            if (source.Width == 256)
            {
                return new Bitmap(source);
            } 
            else
            {
                Bitmap mips = new Bitmap(Bitmap.Width * 2, Bitmap.Height);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(mips);
                var shift = 0;
                var mipLevels = (int)Math.Log2(source.Width) - 3;
                var w = source.Width;
                var h = source.Height;
                for (var i = 0; i < mipLevels; ++i)
                {
                    g.DrawImage(source, shift, 0,w,h);
                    shift += w;
                    w /= 2;
                    h /= 2;
                }
                g.Dispose();
                return mips;
            }
        }
    }
}
