﻿using System;
using System.Collections.Generic;
using System.Linq;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.Libraries
{
    public static class EzSwizzle
    {
        #region Constants
        static readonly int[] block32 = new int[32] {
             0,  1,  4,  5, 16, 17, 20, 21,
             2,  3,  6,  7, 18, 19, 22, 23,
             8,  9, 12, 13, 24, 25, 28, 29,
            10, 11, 14, 15, 26, 27, 30, 31
        };

        static readonly int[] columnWord32 = new int[16] {
             0,  1,  4,  5,  8,  9, 12, 13,
             2,  3,  6,  7, 10, 11, 14, 15
        };

        static readonly int[] block16 = new int[32] {
             0,  2,  8, 10,
             1,  3,  9, 11,
             4,  6, 12, 14,
             5,  7, 13, 15,
            16, 18, 24, 26,
            17, 19, 25, 27,
            20, 22, 28, 30,
            21, 23, 29, 31
        };

        static readonly int[] columnWord16 = new int[32] {
             0,  1,  4,  5,  8,  9, 12, 13,   0,  1,  4,  5,  8,  9, 12, 13,
             2,  3,  6,  7, 10, 11, 14, 15,   2,  3,  6,  7, 10, 11, 14, 15
        };

        static readonly int[] columnHalf16 = new int[32] {
            0, 0, 0, 0, 0, 0, 0, 0,  1, 1, 1, 1, 1, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 0, 0,  1, 1, 1, 1, 1, 1, 1, 1
        };


        static readonly int[] block8 = new int[32] {
             0,  1,  4,  5, 16, 17, 20, 21,
             2,  3,  6,  7, 18, 19, 22, 23,
             8,  9, 12, 13, 24, 25, 28, 29,
            10, 11, 14, 15, 26, 27, 30, 31
        };

        static readonly int[][] columnWord8 = new int[2][] {
            new int[64] {
                 0,  1,  4,  5,  8,  9, 12, 13,   0,  1,  4,  5,  8,  9, 12, 13,
                 2,  3,  6,  7, 10, 11, 14, 15,   2,  3,  6,  7, 10, 11, 14, 15,

                 8,  9, 12, 13,  0,  1,  4,  5,   8,  9, 12, 13,  0,  1,  4,  5,
                10, 11, 14, 15,  2,  3,  6,  7,  10, 11, 14, 15,  2,  3,  6,  7
            },
            new int[64] {
                 8,  9, 12, 13,  0,  1,  4,  5,   8,  9, 12, 13,  0,  1,  4,  5,
                10, 11, 14, 15,  2,  3,  6,  7,  10, 11, 14, 15,  2,  3,  6,  7,

                 0,  1,  4,  5,  8,  9, 12, 13,   0,  1,  4,  5,  8,  9, 12, 13,
                 2,  3,  6,  7, 10, 11, 14, 15,   2,  3,  6,  7, 10, 11, 14, 15
            }
        };

        static readonly int[] columnByte8 = new int[64] {
            0, 0, 0, 0, 0, 0, 0, 0,  2, 2, 2, 2, 2, 2, 2, 2,
            0, 0, 0, 0, 0, 0, 0, 0,  2, 2, 2, 2, 2, 2, 2, 2,

            1, 1, 1, 1, 1, 1, 1, 1,  3, 3, 3, 3, 3, 3, 3, 3,
            1, 1, 1, 1, 1, 1, 1, 1,  3, 3, 3, 3, 3, 3, 3, 3
        };
        #endregion

        public static byte[] readTexPSMT8(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, bool useSrcLen = true)
        {
            dbw >>= 1;
            int src = 0;
            int startBlockPos = dbp * 64;
            byte[] destination = new byte[useSrcLen ? source.Length : rrw * rrh];

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    int cb = 0;
                    int dst = startBlockPos + MapCoords8(x, y, dbw, ref cb);
                    destination[src] = source[4 * dst + cb];
                    src++;
                }
            }
            return destination;
        }

        public static byte[] writeTexPSMT8(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, bool useSrcLen = true)
        {
            dbw >>= 1;
            int src = 0;
            int startBlockPos = dbp * 64;
            byte[] destination = new byte[useSrcLen ? source.Length : rrw * rrh];

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    int cb = 0;
                    int dst = startBlockPos + MapCoords8(x, y, dbw, ref cb);
                    destination[4 * dst + cb] = source[src];
                    src++;
                }
            }
            return destination;
        }
        public static byte[] writeTexPSMT8To(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, byte[] destination)
        {
            dbw >>= 1;
            int src = 0;
            int startBlockPos = dbp * 64;

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    int cb = 0;
                    int dst = startBlockPos + MapCoords8(x, y, dbw, ref cb);
                    destination[4 * dst + cb] = source[src];
                    src++;
                }
            }
            return destination;
        }

        public static byte[] writeTexPSMCT16(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, bool useSrcLen = true)
        {
            int src = 0;
            int startBlockPos = dbp * 64;
            byte[] destination = new byte[useSrcLen ? source.Length : rrw * rrh * 2];

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    int ch = 0;
                    int dst = startBlockPos + MapCoords16(x, y, dbw, ref ch);
                    for (int i = 0; i < 2; i++)
                    {
                        destination[4 * dst + 2 * ch + i] = source[src + i];
                    }
                    src += 2;
                }
            }

            return destination;
        }

        public static byte[] readTexPSMCT16(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, bool useSrcLen = true)
        {
            int src = 0;
            int startBlockPos = dbp * 64;
            byte[] destination = new byte[useSrcLen ? source.Length : rrw * rrh * 2];

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    int ch = 0;
                    int dst = startBlockPos + MapCoords16(x, y, dbw, ref ch);
                    for (int i = 0; i < 2; i++)
                    {
                        destination[src + i] = source[4 * dst + 2 * ch + i];
                    }
                    src += 2;
                }
            }

            return destination;
        }

        public static byte[] writeTexPSMCT32(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, bool useSrcLen = true)
        {
            int src = 0;
            int startBlockPos = dbp * 64;
            byte[] destination = new byte[useSrcLen ? source.Length : rrw * rrh * 4];

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    var dst = startBlockPos + MapCoords32(x, y, dbw);
                    for (int i = 0; i < 4; i++)
                    {
                        destination[4 * dst + i] = source[src + i];
                    }
                    src += 4;
                }
            }
            return destination;
        }

        public static byte[] writeTexPSMCT32To(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, byte[] destination)
        {
            int src = 0;
            int startBlockPos = dbp * 64;

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    var dst = startBlockPos + MapCoords32(x, y, dbw);
                    for (int i = 0; i < 4; i++)
                    {
                        destination[4 * dst + i] = source[src + i];
                    }
                    src += 4;
                }
            }
            return destination;
        }

        public static byte[] readTexPSMCT32(int dbp, int dbw, int dsax, int dsay, int rrw, int rrh, byte[] source, bool useSrcLen = true)
        {
            int src = 0;
            int startBlockPos = dbp * 64;
            byte[] destination = new byte[useSrcLen ? source.Length : rrw * rrh * 4];

            for (int y = dsay; y < dsay + rrh; y++)
            {
                for (int x = dsax; x < dsax + rrw; x++)
                {
                    var dst = startBlockPos + MapCoords32(x, y, dbw);
                    for (int i = 0; i < 4; i++)
                    {
                        destination[src + i] = source[4 * dst + i];
                    }
                    src += 4;
                }
            }
            return destination;
        }
        public static List<Color> TagToColors(GIFTag tag, List<Color> colors)
        {
            List<UInt64> data = tag.Data.Select(d => d.Output).ToList();
            for (var i = 0; i < data.Count - 1; i += 2)
            {
                UInt64 output1 = data[i + 1];
                UInt64 output2 = data[i];
                Color c1 = new Color();
                Color c2 = new Color();
                Color c3 = new Color();
                Color c4 = new Color();
                c1.FromABGR((UInt32)((output1 >> 0) & 0xFFFFFFFF));
                c2.FromABGR((UInt32)((output1 >> 32) & 0xFFFFFFFF));
                c3.FromABGR((UInt32)((output2 >> 0) & 0xFFFFFFFF));
                c4.FromABGR((UInt32)((output2 >> 32) & 0xFFFFFFFF));
                colors.Add(c1);
                colors.Add(c2);
                colors.Add(c3);
                colors.Add(c4);
            }
            return colors;
        }

        public static GIFTag ColorsToTag(List<Color> colors)
        {
            GIFTag tag = new GIFTag();
            tag.NREG = 16;
            tag.EOP = 1;
            tag.NLOOP = (ushort)(colors.Count / 4);
            tag.REGS = new REGSEnum[16];
            tag.FLG = GIFModeEnum.IMAGE;
            tag.Data = new List<RegOutput>();
            for (var i = 0; i < colors.Count - 3; i += 4)
            {
                UInt64 col0 = colors[i + 0].ToABGR();
                UInt64 col1 = colors[i + 1].ToABGR();
                UInt64 col2 = colors[i + 2].ToABGR();
                UInt64 col3 = colors[i + 3].ToABGR();
                UInt64 long1 = (col1 << 32) | (col0);
                UInt64 long2 = (col3 << 32) | (col2);
                RegOutput reg1 = new RegOutput();
                reg1.REG = REGSEnum.HWREG;
                reg1.Output = long1;
                RegOutput reg2 = new RegOutput();
                reg2.REG = REGSEnum.HWREG;
                reg2.Output = long2;
                tag.Data.Add(reg2);
                tag.Data.Add(reg1);
            }
            return tag;
        }
        public static byte[] TagToBytes(GIFTag tag)
        {
            List<UInt64> data = tag.Data.Select(d => d.Output).ToList();
            byte[] bytes = new byte[data.Count * 8];
            for (var i = 0; i < data.Count / 2; ++i)
            {
                UInt64 output1 = data[i * 2];
                UInt64 output2 = data[i * 2 + 1];
                Array.Copy(BitConverter.GetBytes(output2), 0, bytes, i * 16, 8);
                Array.Copy(BitConverter.GetBytes(output1), 0, bytes, i * 16 + 8, 8);
            }
            return bytes;
        }

        public static void ColorsToByte(Color color, byte[] array, int index)
        {
            UInt32 abrg = color.ToABGR();
            array[index * 4 + 3] = (Byte)((abrg >> 24) & 0xFF);
            array[index * 4 + 2] = (Byte)((abrg >> 16) & 0xFF);
            array[index * 4 + 1] = (Byte)((abrg >> 8) & 0xFF);
            array[index * 4 + 0] = (Byte)((abrg >> 0) & 0xFF);
        }
        public static Color BytesToColor(byte[] array, int index)
        {
            Color color = new Color();
            color.FromABGR((UInt32)((array[index + 3] << 24) | (array[index + 2] << 16) | (array[index + 1] << 8) | (array[index + 0] << 0)));
            return color;
        }

        public static List<Color> BytesToColors(byte[] array)
        {
            List<Color> colors = new List<Color>(array.Length / 4);
            for (var i = 0; i < array.Length / 4; ++i)
            {
                colors.Add(BytesToColor(array, i * 4));
            }

            return colors;
        }

        public static Int32 MapCoords32(Int32 x, Int32 y, Int32 width)
        {
            int pageX = x / 64;
            int pageY = y / 32;
            int page = pageX + pageY * width;

            int px = x - (pageX * 64);
            int py = y - (pageY * 32);

            int blockX = px / 8;
            int blockY = py / 8;
            int block = block32[blockX + blockY * 8];

            int bx = px - blockX * 8;
            int by = py - blockY * 8;

            int column = by / 2;

            int cx = bx;
            int cy = by - column * 2;
            int cw = columnWord32[cx + cy * 8];

            return page * 2048 + block * 64 + column * 16 + cw;
        }
        public static Int32 MapCoords16(Int32 x, Int32 y, Int32 width, ref Int32 ch)
        {
            int pageX = x / 64;
            int pageY = y / 64;
            int page = pageX + pageY * width;

            int px = x - (pageX * 64);
            int py = y - (pageY * 64);

            int blockX = px / 16;
            int blockY = py / 8;
            int block = block16[blockX + blockY * 4];

            int bx = px - blockX * 16;
            int by = py - blockY * 8;

            int column = by / 2;

            int cx = bx;
            int cy = by - column * 2;
            int cw = columnWord16[cx + cy * 16];
            ch = columnHalf16[cx + cy * 16];

            return page * 2048 + block * 64 + column * 16 + cw;
        }
        public static Int32 MapCoords8(Int32 x, Int32 y, Int32 width, ref Int32 cb)
        {
            int pageX = x / 128;
            int pageY = y / 64;
            int page = pageX + pageY * width;

            int px = x - (pageX * 128);
            int py = y - (pageY * 64);

            int blockX = px / 16;
            int blockY = py / 16;
            int block = block8[blockX + blockY * 8];

            int bx = px - (blockX * 16);
            int by = py - (blockY * 16);

            int column = by / 4;

            int cx = bx;
            int cy = by - column * 4;
            int cw = columnWord8[column & 1][cx + cy * 16];
            cb = columnByte8[cx + cy * 16];

            return page * 2048 + block * 64 + column * 16 + cw;
        }
    }
}