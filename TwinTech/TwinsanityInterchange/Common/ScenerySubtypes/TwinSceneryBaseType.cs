﻿using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes
{
    public class TwinSceneryBaseType : ITwinSerializable
    {
        public List<UInt32> MeshIDs;
        public List<UInt32> LodIDs;
        public List<Vector4[]> BoundingBoxes;
        public List<Matrix4> MeshModelMatrices;
        public List<Matrix4> LodModelMatrices;
        public Vector4 UnkVec1;
        public Vector4 UnkVec2;
        public Vector4 UnkVec3;
        public Vector4 UnkVec4;
        public Boolean[] LightsEnabler;

        public TwinSceneryBaseType()
        {
            MeshIDs = new List<UInt32>();
            LodIDs = new List<UInt32>();
            BoundingBoxes = new List<Vector4[]>();
            MeshModelMatrices = new List<Matrix4>();
            LodModelMatrices = new List<Matrix4>();
            UnkVec1 = new Vector4();
            UnkVec2 = new Vector4();
            UnkVec3 = new Vector4();
            UnkVec4 = new Vector4();
            LightsEnabler = new Boolean[128];
        }

        public virtual Int32 GetLength()
        {
            return 4 + 5 * Constants.SIZE_VECTOR4 + (MeshIDs.Count + LodIDs.Count) * (Constants.SIZE_VECTOR4 * 2 + Constants.SIZE_MATRIX4 + 4) +
                (MeshIDs.Count != 0 || LodIDs.Count != 0 ? 4 : 0);
        }

        public void Compile()
        {
            return;
        }

        public virtual void Read(BinaryReader reader, Int32 length)
        {
            var hasMeshLods = reader.ReadInt32();
            if (hasMeshLods == 0x1613)
            {
                var meshAmt = reader.ReadInt16();
                var lodAmt = reader.ReadInt16();
                var sum = meshAmt + lodAmt;
                if (sum != 0)
                {
                    for (var i = 0; i < sum; ++i)
                    {
                        Vector4[] bb = { new Vector4(), new Vector4() };
                        bb[0].Read(reader, Constants.SIZE_VECTOR4);
                        bb[1].Read(reader, Constants.SIZE_VECTOR4);
                        BoundingBoxes.Add(bb);
                    }
                    for (var i = 0; i < sum; ++i)
                    {
                        var id = reader.ReadUInt32();
                        if (i < meshAmt)
                        {
                            MeshIDs.Add(id);
                        }
                        else
                        {
                            LodIDs.Add(id);
                        }
                    }
                    for (var i = 0; i < sum; ++i)
                    {
                        var mat = new Matrix4();
                        mat.Read(reader, Constants.SIZE_MATRIX4);
                        if (i < meshAmt)
                        {
                            MeshModelMatrices.Add(mat);
                        }
                        else
                        {
                            LodModelMatrices.Add(mat);
                        }
                    }
                }
            }
            UnkVec1.Read(reader, Constants.SIZE_VECTOR4);
            UnkVec2.Read(reader, Constants.SIZE_VECTOR4);
            UnkVec3.Read(reader, Constants.SIZE_VECTOR4);
            UnkVec4.Read(reader, Constants.SIZE_VECTOR4);
            var bytes = reader.ReadBytes(0x10);
            var index = 0;
            foreach (var b in bytes)
            {
                var currentByte = b;
                for (var i = 0; i < 8; ++i)
                {
                    LightsEnabler[i + index * 8] = (currentByte & 1) == 1;
                    currentByte >>= 1;
                }
                index++;
            }
        }

        public virtual void Write(BinaryWriter writer)
        {
            var hasMeshLods = (MeshIDs.Count != 0 || LodIDs.Count != 0);
            writer.Write(hasMeshLods ? 0x1613 : 0);
            if (hasMeshLods)
            {
                writer.Write((Int16)MeshIDs.Count);
                writer.Write((Int16)LodIDs.Count);
                foreach (var bb in BoundingBoxes)
                {
                    bb[0].Write(writer);
                    bb[1].Write(writer);
                }
                foreach (var meshID in MeshIDs)
                {
                    writer.Write(meshID);
                }
                foreach (var lodID in LodIDs)
                {
                    writer.Write(lodID);
                }
                foreach (var mat in MeshModelMatrices)
                {
                    mat.Write(writer);
                }
                foreach (var mat in LodModelMatrices)
                {
                    mat.Write(writer);
                }
            }
            UnkVec1.Write(writer);
            UnkVec2.Write(writer);
            UnkVec3.Write(writer);
            UnkVec4.Write(writer);
            var bytes = new Byte[16];
            for (var i = 0; i < 16; ++i)
            {
                for (var j = 0; j < 8; ++j)
                {
                    var num = LightsEnabler[j + i * 8] ? 1 : 0;
                    bytes[i] |= (Byte)(num << j);
                }
            }
            writer.Write(bytes);
        }

        public virtual ITwinScenery.SceneryType GetObjectIndex()
        {
            return ITwinScenery.SceneryType.Leaf;
        }
    }
}
