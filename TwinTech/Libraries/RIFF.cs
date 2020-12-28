using System.IO;

namespace Twinsanity.Libraries
{
    public static class RIFF
    {
        public static void SaveRiff(BinaryWriter writer, byte[] pcm, short channels, int samplerate)
        {
            writer.Write("RIFF".ToCharArray());
            writer.Write(36 + pcm.Length);
            writer.Write("WAVE".ToCharArray());
            writer.Write("fmt ".ToCharArray());
            writer.Write(16);
            writer.Write((ushort)1);
            writer.Write(channels);
            writer.Write(samplerate);
            writer.Write(samplerate * channels * 2);
            writer.Write((short)(channels * 2));
            writer.Write((ushort)16);
            writer.Write("data".ToCharArray());
            writer.Write(pcm.Length);
            writer.Write(pcm);
        }
    }
}
