using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code
{
    public class PS2AnyOGI : BaseTwinItem, ITwinOGI
    {

        public enum HeaderInfo
        {
            TYPE1_AMOUNT = 0,
            TYPE2_AMOUNT = 1,
            RIGID_MODELS_AMOUNT = 5,
            HAS_SKIN = 6,
            HAS_BLEND_SKIN = 7,
            TYPE3_AMOUNT = 8,
        }

        Byte[] headerData;
        public List<OGIType1> Type1List { get; set; }
        public List<OGIType2> Type2List { get; set; }
        public Vector4[] BoundingBox { get; set; }
        public List<Byte> RigidRelatedList { get; set; }
        public List<UInt32> RigidModelIds { get; set; }
        public List<Matrix4> Type1RelatedMatrix { get; set; }
        public UInt32 SkinID { get; set; }
        public UInt32 BlendSkinID { get; set; }
        public List<BoundingBoxBuilder> Type3List { get; set; }
        public List<Byte> Type3RelatedList { get; set; }

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
            Type3List = new List<BoundingBoxBuilder>();
            Type3RelatedList = new List<byte>();
        }

        public override int GetLength()
        {
            int dynamic_size = 0;
            foreach (ITwinSerializable e in Type3List)
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

        public override void Read(BinaryReader reader, int length)
        {
            reader.Read(headerData, 0, headerData.Length);
            Byte t1cnt = headerData[(int)HeaderInfo.TYPE1_AMOUNT];
            Byte t2cnt = headerData[(int)HeaderInfo.TYPE2_AMOUNT];
            Byte rigidcnt = headerData[(int)HeaderInfo.RIGID_MODELS_AMOUNT];
            Byte skinFlag = headerData[(int)HeaderInfo.HAS_SKIN];
            Byte blendFlag = headerData[(int)HeaderInfo.HAS_BLEND_SKIN];
            Byte t3cnt = headerData[(int)HeaderInfo.TYPE3_AMOUNT];
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
                BoundingBoxBuilder type3 = new BoundingBoxBuilder();
                type3.Read(reader, type3.GetLength());
                Type3List.Add(type3);
            }
            Type3RelatedList.Clear();
            for (int i = 0; i < t3cnt; ++i)
            {
                Type3RelatedList.Add(reader.ReadByte());
            }
        }

        public override void Write(BinaryWriter writer)
        {
            headerData[(int)HeaderInfo.TYPE1_AMOUNT] = (Byte)Type1List.Count;
            headerData[(int)HeaderInfo.TYPE2_AMOUNT] = (Byte)Type2List.Count;
            headerData[(int)HeaderInfo.RIGID_MODELS_AMOUNT] = (Byte)RigidModelIds.Count;
            headerData[(int)HeaderInfo.HAS_SKIN] = (Byte)((SkinID == 0) ? 0 : 1);
            headerData[(int)HeaderInfo.HAS_BLEND_SKIN] = (Byte)((BlendSkinID == 0) ? 0 : 1);
            headerData[(int)HeaderInfo.TYPE3_AMOUNT] = (Byte)Type3List.Count;
            writer.Write(headerData);
            BoundingBox[0].Write(writer);
            BoundingBox[1].Write(writer);
            foreach(ITwinSerializable item in Type1List)
            {
                item.Write(writer);
            }
            foreach (ITwinSerializable item in Type2List)
            {
                item.Write(writer);
            }
            foreach (Byte item in RigidRelatedList)
            {
                writer.Write(item);
            }
            foreach (UInt32 item in RigidModelIds)
            {
                writer.Write(item);
            }
            foreach (ITwinSerializable item in Type1RelatedMatrix)
            {
                item.Write(writer);
            }
            writer.Write(SkinID);
            writer.Write(BlendSkinID);
            foreach (ITwinSerializable item in Type3List)
            {
                item.Write(writer);
            }
            foreach (Byte item in Type3RelatedList)
            {
                writer.Write(item);
            }
        }

        public override String GetName()
        {
            return $"OGI {id:X}";
        }
    }
}
