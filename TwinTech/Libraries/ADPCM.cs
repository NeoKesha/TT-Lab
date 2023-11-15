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
        private SampleLineFlags LineToADPCM(BinaryReader reader, BinaryWriter writer, ref float s0, ref float s1)
        {
            SampleLineFlags flags = SampleLineFlags.None;
            long dataLeft = reader.BaseStream.Length - reader.BaseStream.Position;
            long dataToProcess = Math.Min(BUFFER_SIZE, dataLeft);
            long waveCount = dataToProcess / 2; // For 16 bit PCM.
            long sampleCount = waveCount / 28;
            Int16[] waves = new short[waveCount];
            for (int i = 0; i < waveCount; ++i)
            {
                waves[i] = reader.ReadInt16();
            }
            int predict = 0;
            int factor = 0;
            for (int i = 0; i < sampleCount; ++i)
            {
                double[] d_samples = new double[28];
                short[] v_samples = new short[28];
                FindPredict(waves, i * 28, d_samples, ref predict, ref factor, ref s0, ref s1);
                PackSamples(d_samples, v_samples, predict, factor);
                writer.Write((byte)((predict << 4) | factor));
                writer.Write((byte)0);
                for (int k = 0; k < 28; k += 2)
                {
                    writer.Write((byte)(((v_samples[k + 1] >> 8) & 0xF0) | ((v_samples[k] >> 12) & 0xF)));
                }
            }
            if (dataLeft < 28)
            {
                flags = SampleLineFlags.LoopEnd;
                writer.Write((byte)((predict << 4) | factor));
                writer.Write((byte)7);
                for (var i = 0; i < 14; ++i)
                {
                    writer.Write((byte)0);
                }
            }
            return flags;
        }

        private void FindPredict(short[] samples, int sample_off, double[] d_samples, ref int predict, ref int factor, ref float s0, ref float s1)
        {
            double[] max = new double[5];
            double[][] buffer = new double[28][];
            for (int i = 0; i < 28; ++i)
                buffer[i] = new double[5];
            double s_0, s_1 = 0.0, s_2 = 0.0, min = 1e10;
            for (int i = 0; i < 5; ++i)
            {
                max[i] = 0.0;
                s_1 = s0;
                s_2 = s1;
                for (int j = 0; j < 28; ++j)
                {
                    s_0 = samples[j + sample_off];
                    if (s_0 > 30719.0)
                        s_0 = 30719.0;
                    else if (s_0 < -30719.0)
                        s_0 = -30719.0;
                    double ds = s_0 + s_1 * F[i].Key + s_2 * F[i].Value;
                    buffer[j][i] = ds;
                    if (Math.Abs(ds) > max[i])
                    {
                        max[i] = Math.Abs(ds);
                    }
                    s_2 = s_1;
                    s_1 = s_0;
                }
                if (max[i] < min)
                {
                    min = max[i];
                    predict = i;
                }
                if (min <= 7)
                {
                    predict = 0;
                    break;
                }
            }
            s0 = (float)s_1;
            s1 = (float)s_2;
            for (int i = 0; i < 28; ++i)
            {
                d_samples[i] = buffer[i][predict];
            }
            int min2 = (int)min, mask = 0x4000;
            factor = 0;
            while (factor < 12)
            {
                if ((mask & (min2 + (mask >> 3))) != 0)
                {
                    break;
                }
                factor++;
                mask >>= 1;
            }
        }

        private void PackSamples(double[] d_samples, short[] v_samples, int predict, int factor)
        {
            double s_1 = 0.0, s_2 = 0.0;

            for (int i = 0; i < 28; ++i)
            {
                double s_0 = d_samples[i] + s_1 * F[predict].Key + s_2 * F[predict].Value;
                double ds = s_0 * (1 << factor);
                int di = (int)(((int)ds + 0x800) & 0xfffff000);
                if (di > short.MaxValue)
                    di = short.MaxValue;
                else if (di < short.MinValue)
                    di = short.MinValue;
                v_samples[i] = (short)di;
                di >>= factor;
                s_2 = s_1;
                s_1 = di - s_0;
            }
        }
        public void ToADPCMMono(BinaryReader reader, BinaryWriter writer)
        {
            float s0 = 0.0f;
            float s1 = 0.0f;
            SampleLineFlags flag = 0;
            while ((flag & SampleLineFlags.LoopEnd) == 0)
            {
                flag = LineToADPCM(reader, writer, ref s0, ref s1);
            }
        }

        public void ToADPCMStereo(BinaryReader reader, BinaryWriter writer, int interleave)
        {
            throw new NotImplementedException();
            /*
            if ((interleave % 16) != 0)
                throw new ArgumentException("Interleave must be a multiple of 16.");
            byte[] silence = new byte[interleave];
            for (int i = 0; i < interleave * 2; ++i)
                silence[i] = 0;
            int sample_size = data.Length / 4;
            byte[] data_l = new byte[sample_size * 2];
            byte[] data_r = new byte[sample_size * 2];
            List<byte> vag = new List<byte>();
            for (int i = 0; i < sample_size; ++i)
            {
                data_l[i + 0] = data[i * 4 + 0];
                data_l[i + 1] = data[i * 4 + 1];
                data_r[i + 0] = data[i * 4 + 2];
                data_r[i + 1] = data[i * 4 + 3];
            }
            data_l = FromPCMMono(data_l);
            data_r = FromPCMMono(data_r);
            for (int i = 0; i < data_l.Length; i += interleave)
            {
                vag.AddRange(silence);
            }
            var vag_data = vag.ToArray();
            var ch_size = data_l.Length;
            int off = 0;
            while (ch_size > 0)
            {
                var size = (ch_size >= interleave) ? interleave : ch_size;
                Array.Copy(data_l, off, vag_data, off * 2, size);
                Array.Copy(data_r, off + interleave, vag_data, off * 2 + interleave, size);
                off += interleave;
                ch_size -= interleave;
            }
            return vag_data;*/
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

