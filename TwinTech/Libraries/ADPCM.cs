using System;
using System.Collections.Generic;
using System.IO;

namespace Twinsanity.Libraries
{
    [Flags]
    public enum SampleLineFlags : byte
    {
        None = 0,
        LoopEnd = 1,
        Unknown = 2,
        LoopStart = 4
    }
    public class ADPCM
    {
        // Based on code by bITmASTER and nextvolume
        // https://github.com/simias/psxsdk/blob/master/tools/vag2wav.c
        // https://github.com/simias/psxsdk/blob/master/tools/wav2vag.c

        private static readonly int BUFFER_SIZE = 128 * 28;
        private static readonly List<KeyValuePair<float, float>> F = new List<KeyValuePair<float, float>>
        {
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.9375f, 0.0f),
            new KeyValuePair<float, float>(1.796875f, -0.8125f),
            new KeyValuePair<float, float>(1.53125f, -0.859375f),
            new KeyValuePair<float, float>(1.90625f, -0.9375f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f),
            new KeyValuePair<float, float>(0.0f, 0.0f)
        };

        private short SampleToPCM(int sample, int factor, int predict, ref float s0, ref float s1)
        {
            sample <<= 12;
            sample = (short)sample;
            sample >>= factor;
            float value = sample;
            value += s0 * F[predict].Key;
            value += s1 * F[predict].Value;
            s1 = s0;
            s0 = value;
            return (short)Math.Round(value);
        }
        private SampleLineFlags LineToPCM(BinaryReader reader, BinaryWriter writer, ref float s0, ref float s1)
        {
            byte[] o = new byte[28 * 2];
            Byte startByte = reader.ReadByte();
            SampleLineFlags flags = (SampleLineFlags)reader.ReadByte();
            int factor = startByte & 0xF;
            int predict = (startByte >> 4) & 0xF;
            if ((flags & SampleLineFlags.LoopEnd) == 0)
            {
                for (int i = 0; i < 14; i++)
                {
                    Byte src = reader.ReadByte();
                    int low = src & 0xF;
                    int high = (src & 0xF0) >> 4;
                    short l = SampleToPCM(low, factor, predict, ref s0, ref s1);
                    short h = SampleToPCM(high, factor, predict, ref s0, ref s1);
                    writer.Write(l);
                    writer.Write(h);
                }
            }
            return flags;
        }

        public void ToPCMMono(BinaryReader reader, BinaryWriter writer)
        {
            float s0 = 0.0f;
            float s1 = 0.0f;
            SampleLineFlags flag = 0;
            while ((flag & SampleLineFlags.LoopEnd) == 0)
            {
                flag = LineToPCM(reader, writer, ref s0, ref s1);
            }
        }

        public void ToPCMStereo(BinaryReader reader, BinaryWriter writer, int interleave)
        {
            if ((interleave % 16) != 0)
                throw new ArgumentException("Stereo interleave is not a multiple of 16.");
            int blocks = interleave /= 16;
            float s0_l = 0;
            float s1_l = 0;
            float s0_r = 0;
            float s1_r = 0;
            SampleLineFlags flag_l = 0;
            SampleLineFlags flag_r = 0;
            while ((flag_l & SampleLineFlags.LoopEnd) == 0 && (flag_r & SampleLineFlags.LoopEnd) == 0)
            {
                for (var i = 0; i < blocks; ++i)
                {
                    flag_l = LineToPCM(reader, writer, ref s0_l, ref s1_l);
                    if ((flag_l & SampleLineFlags.LoopEnd) != 0)
                    {
                        break;
                    }
                }
                for (var i = 0; i < blocks; ++i)
                {
                    flag_r = LineToPCM(reader, writer, ref s0_r, ref s1_r);
                    if ((flag_r & SampleLineFlags.LoopEnd) != 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}

