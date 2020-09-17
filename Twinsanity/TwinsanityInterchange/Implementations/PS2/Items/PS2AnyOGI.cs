﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items
{
    public class PS2AnyOGI : ITwinTrigger
    {
        UInt32 id;
        Byte[] headerData;
        public List<OGIType1> Type1List { get; private set; }
        public List<OGIType2> Type2List { get; private set; }
        public Vector4[] BoundingBox { get; private set; }
        public List<Byte> RigidRelatedList { get; private set; }
        public List<UInt32> RigidModelIds { get; private set; }
        public List<Matrix4> Type1RelatedMatrix { get; private set; }
        public UInt32 SkinID { get; set; }
        public UInt32 BlendSkinID { get; set; }
        public List<OGIType3> Type3List { get; private set; }
        public List<Byte> Type3RelatedList { get; private set; }

        public PS2AnyOGI()
        {
            headerData = new byte[0x10];
            BoundingBox = new Vector4[2];
            BoundingBox[0] = new Vector4();
            BoundingBox[1] = new Vector4();
            Type1List = new List<OGIType1>();
            Type2List = new List<OGIType2>();
            RigidRelatedList = new List<byte>();
            RigidModelIds = new List<uint>();
            Type1RelatedMatrix = new List<Matrix4>();
            Type3List = new List<OGIType3>();
            Type3RelatedList = new List<byte>();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            int dynamic_size = 0;
            foreach (ITwinSerializeable e in Type3List)
            {
                dynamic_size += e.GetLength();
            }
            return headerData.Length + Constants.SIZE_VECTOR4 * 2
                + Constants.SIZE_OGI_TYPE1 * Type1List.Count
                + Constants.SIZE_OGI_TYPE2 * Type2List.Count
                + RigidRelatedList.Count
                + Constants.SIZE_UINT32 * RigidModelIds.Count
                + Constants.SIZE_MATRIX4 * Type1RelatedMatrix.Count
                + 8 + dynamic_size + Type3RelatedList.Count;
        }

        public void Read(BinaryReader reader, int length)
        {
            reader.Read(headerData, 0, headerData.Length);
            Byte t1cnt = headerData[0];
            Byte t2cnt = headerData[1];
            Byte rigidcnt = headerData[5];
            Byte skinFlag = headerData[6];
            Byte blendFlag = headerData[7];
            Byte t3cnt = headerData[8];
            BoundingBox[0].Read(reader, Constants.SIZE_VECTOR4);
            BoundingBox[1].Read(reader, Constants.SIZE_VECTOR4);
            Type1List.Clear();
            for (int i = 0; i < t1cnt; ++i)
            {
                OGIType1 type1 = new OGIType1();
                type1.Read(reader, type1.GetLength());
                Type1List.Add(type1);
            }
            Type2List.Clear();
            for (int i = 0; i < t2cnt; ++i)
            {
                OGIType2 type2 = new OGIType2();
                type2.Read(reader, type2.GetLength());
                Type2List.Add(type2);
            }
            RigidRelatedList.Clear();
            for (int i = 0; i < rigidcnt; ++i)
            {
                RigidRelatedList.Add(reader.ReadByte());
            }
            RigidModelIds.Clear();
            for (int i = 0; i < rigidcnt; ++i)
            {
                RigidModelIds.Add(reader.ReadUInt32());
            }
            Type1RelatedMatrix.Clear();
            for (int i = 0; i < t1cnt; ++i)
            {
                Matrix4 m = new Matrix4();
                m.Read(reader, m.GetLength());
                Type1RelatedMatrix.Add(m);
            }
            SkinID = reader.ReadUInt32();
            BlendSkinID = reader.ReadUInt32();
            Type3List.Clear();
            for (int i = 0; i < t3cnt; ++i)
            {
                OGIType3 type3 = new OGIType3();
                type3.Read(reader, type3.GetLength());
                Type3List.Add(type3);
            }
            Type3RelatedList.Clear();
            for (int i = 0; i < t3cnt; ++i)
            {
                Type3RelatedList.Add(reader.ReadByte());
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            
        }
    }
}