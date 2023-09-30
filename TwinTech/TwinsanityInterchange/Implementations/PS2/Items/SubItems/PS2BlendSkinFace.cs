using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems
{
    public class PS2BlendSkinFace : ITwinBlendSkinFace
    {
        Vector3 blendShape;
        Byte[] faceData;
        public UInt32 VertexesAmount { get; set; }
        public List<VertexBlendShape> Vertices { get; set; }

        UInt16 qwcAmount { get => (UInt16)(faceData.Length >> 4); }

        public PS2BlendSkinFace(Vector3 blendShape)
        {
            this.blendShape = blendShape;
        }

        public Int32 GetLength()
        {
            return 4 + 4 + faceData.Length;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            var blobSize = reader.ReadInt32();
            VertexesAmount = reader.ReadUInt32();
            faceData = reader.ReadBytes(blobSize << 4);
        }

        public void CalculateData()
        {
            var dma = new DMATag
            {
                QWC = qwcAmount,
                Extra = (0x0) | ((VertexesAmount + 1) << 0x10) | 0x6E000000 // Unpack vectors with V4_8 format
            };

            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            dma.Write(writer);
            writer.Write(faceData);
            ms.Position = 0;

            using var reader = new BinaryReader(ms);
            var interpreter = VIFInterpreter.InterpretCode(reader);
            var data = interpreter.GetMem();
            Vertices = new((int)VertexesAmount);
            for (Int32 i = 0; i < VertexesAmount; i++)
            {
                var vec = data[0][i + 1];
                var xComp = (int)vec.GetBinaryX();
                var yComp = (int)vec.GetBinaryY();
                var zComp = (int)vec.GetBinaryZ();
                Vertices.Add(new VertexBlendShape
                {
                    Offset = new Vector4(xComp * blendShape.X, yComp * blendShape.Y, zComp * blendShape.Z, 1.0f)
                });
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(faceData.Length << 4);
            writer.Write(VertexesAmount);
            writer.Write(faceData);
        }
    }
}
