using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class TextureData : AbstractAssetData
    {
        public TextureData()
        {
        }

        public TextureData(ITwinTexture texture) : this()
        {
            SetTwinItem(texture);
        }

        public Bitmap Bitmap;

        public ITwinTexture.TexturePixelFormat TexturePixelFormat { get; set; }
        public ITwinTexture.TextureFunction TextureFunction { get; set; }
        public Boolean GenerateMipmaps { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            if (Bitmap != null && !Disposed)
            {
                Bitmap.Dispose();
            }
        }
        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            if (Bitmap != null && !Disposed)
            {
                Bitmap.Save(dataPath, ImageFormat.Png);
            }
        }

        protected override void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            Bitmap = new Bitmap(Bitmap.FromFile(dataPath));
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            ITwinTexture texture = GetTwinItem<ITwinTexture>();
            if (texture.TextureFormat == ITwinTexture.TexturePixelFormat.PSMCT32 || texture.TextureFormat == ITwinTexture.TexturePixelFormat.PSMT8)
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
            //}
            //SetTwinItem(Export());
            //{
            //    PS2AnyTexture texture = GetTwinItem<PS2AnyTexture>();
            //    if (texture.TextureFormat == TexturePixelFormat.PSMCT32 || texture.TextureFormat == TexturePixelFormat.PSMT8)
            //    {
            //        Int32 width = (Int32)Math.Pow(2, texture.ImageWidthPower);
            //        Int32 height = (Int32)Math.Pow(2, texture.ImageHeightPower);
            //        texture.CalculateData();

            //        var Bits = new UInt32[width * height];
            //        var BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            //        var tmpBmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());

            //        for (var x = 0; x < width; ++x)
            //        {
            //            for (var y = 0; y < height; ++y)
            //            {
            //                var dstx = x;
            //                var dsty = height - 1 - y;
            //                Bits[dstx + dsty * width] = texture.Colors[x + y * width].ToARGB();
            //            }
            //        }

            //        Bitmap = new Bitmap(tmpBmp);
            //        tmpBmp.Dispose();
            //        BitsHandle.Free();
            //    }
            //}
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            ITwinTexture texture = factory.GenerateTexture();
            var fun = TextureFunction;
            var format = (Bitmap.Width == 256) ? ITwinTexture.TexturePixelFormat.PSMCT32 : ITwinTexture.TexturePixelFormat.PSMT8;
            var tex = new List<Twinsanity.TwinsanityInterchange.Common.Color>();
            var bits = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* source = (byte*)(bits.Scan0 + bits.Stride * (bits.Height - 1));
                for (int i = 0; i < bits.Height; i++)
                {
                    var scan = source;
                    for (int j = 0; j < bits.Width; j++)
                    {
                        var b = source[0];
                        var g = source[1];
                        var r = source[2];
                        var a = source[3];
                        tex.Add(new Twinsanity.TwinsanityInterchange.Common.Color(r, g, b, a));
                        source += 4;
                    }
                    source = scan - bits.Stride;
                }
            }
            texture.FromBitmap(tex, Bitmap.Width, fun, format, GenerateMipmaps);
            Bitmap.UnlockBits(bits);

            return texture;
        }
    }
}
