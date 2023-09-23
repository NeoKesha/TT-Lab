using System;
using System.Collections.Generic;
using System.IO;
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
            JOINT_AMOUNT = 0,
            EXIT_POINT_AMOUNT = 1,
            REACT_JOINT_AMOUNT = 2,
            RIGID_MODELS_AMOUNT = 5,
            HAS_SKIN = 6,
            HAS_BLEND_SKIN = 7,
            COLLISIONS_AMOUNT = 8,
        }

        Byte[] headerData;
        public List<Joint> Joints { get; set; }
        public List<ExitPoint> ExitPoints { get; set; }
        public Vector4[] BoundingBox { get; set; }
        public List<Byte> JointIndices { get; set; }
        public List<UInt32> RigidModelIds { get; set; }
        public List<Matrix4> SkinInverseBindMatrices { get; set; }
        public UInt32 SkinID { get; set; }
        public UInt32 BlendSkinID { get; set; }
        public List<BoundingBoxBuilder> Collisions { get; set; }
        public List<Byte> CollisionJointIndices { get; set; }

        public PS2AnyOGI()
        {
            headerData = new byte[0x10];
            BoundingBox = new Vector4[2];
            BoundingBox[0] = new Vector4();
            BoundingBox[1] = new Vector4();
            Joints = new List<Joint>();
            ExitPoints = new List<ExitPoint>();
            JointIndices = new List<byte>();
            RigidModelIds = new List<uint>();
            SkinInverseBindMatrices = new List<Matrix4>();
            Collisions = new List<BoundingBoxBuilder>();
            CollisionJointIndices = new List<byte>();
        }

        public override int GetLength()
        {
            int dynamic_size = 0;
            foreach (ITwinSerializable e in Collisions)
            {
                dynamic_size += e.GetLength();
            }
            return headerData.Length + Constants.SIZE_VECTOR4 * 2
                + Constants.SIZE_JOINT * Joints.Count
                + Constants.SIZE_EXIT_POINT * ExitPoints.Count
                + JointIndices.Count
                + Constants.SIZE_UINT32 * RigidModelIds.Count
                + Constants.SIZE_MATRIX4 * SkinInverseBindMatrices.Count
                + 8 + dynamic_size + CollisionJointIndices.Count;
        }

        public override void Read(BinaryReader reader, int length)
        {
            reader.Read(headerData, 0, headerData.Length);
            Byte jointAmount = headerData[(int)HeaderInfo.JOINT_AMOUNT];
            Byte exitPointAmount = headerData[(int)HeaderInfo.EXIT_POINT_AMOUNT];
            Byte rigidModelsAmount = headerData[(int)HeaderInfo.RIGID_MODELS_AMOUNT];
            Byte skinFlag = headerData[(int)HeaderInfo.HAS_SKIN];
            Byte blendFlag = headerData[(int)HeaderInfo.HAS_BLEND_SKIN];
            Byte collidersAmount = headerData[(int)HeaderInfo.COLLISIONS_AMOUNT];
            BoundingBox[0].Read(reader, Constants.SIZE_VECTOR4);
            BoundingBox[1].Read(reader, Constants.SIZE_VECTOR4);
            Joints.Clear();
            for (int i = 0; i < jointAmount; ++i)
            {
                Joint joint = new Joint();
                joint.Read(reader, joint.GetLength());
                Joints.Add(joint);
            }
            ExitPoints.Clear();
            for (int i = 0; i < exitPointAmount; ++i)
            {
                ExitPoint exitPoint = new ExitPoint();
                exitPoint.Read(reader, exitPoint.GetLength());
                ExitPoints.Add(exitPoint);
            }
            JointIndices.Clear();
            for (int i = 0; i < rigidModelsAmount; ++i)
            {
                JointIndices.Add(reader.ReadByte());
            }
            RigidModelIds.Clear();
            for (int i = 0; i < rigidModelsAmount; ++i)
            {
                RigidModelIds.Add(reader.ReadUInt32());
            }
            SkinInverseBindMatrices.Clear();
            for (int i = 0; i < jointAmount; ++i)
            {
                Matrix4 m = new Matrix4();
                m.Read(reader, m.GetLength());
                SkinInverseBindMatrices.Add(m);
            }
            SkinID = reader.ReadUInt32();
            BlendSkinID = reader.ReadUInt32();
            Collisions.Clear();
            for (int i = 0; i < collidersAmount; ++i)
            {
                BoundingBoxBuilder bbBuilder = new BoundingBoxBuilder();
                bbBuilder.Read(reader, bbBuilder.GetLength());
                Collisions.Add(bbBuilder);
            }
            CollisionJointIndices.Clear();
            for (int i = 0; i < collidersAmount; ++i)
            {
                CollisionJointIndices.Add(reader.ReadByte());
            }
        }

        public override void Write(BinaryWriter writer)
        {
            headerData[(int)HeaderInfo.JOINT_AMOUNT] = (Byte)Joints.Count;
            headerData[(int)HeaderInfo.EXIT_POINT_AMOUNT] = (Byte)ExitPoints.Count;
            headerData[(int)HeaderInfo.RIGID_MODELS_AMOUNT] = (Byte)RigidModelIds.Count;
            headerData[(int)HeaderInfo.HAS_SKIN] = (Byte)((SkinID == 0) ? 0 : 1);
            headerData[(int)HeaderInfo.HAS_BLEND_SKIN] = (Byte)((BlendSkinID == 0) ? 0 : 1);
            headerData[(int)HeaderInfo.COLLISIONS_AMOUNT] = (Byte)Collisions.Count;
            writer.Write(headerData);
            BoundingBox[0].Write(writer);
            BoundingBox[1].Write(writer);
            foreach (ITwinSerializable item in Joints)
            {
                item.Write(writer);
            }
            foreach (ITwinSerializable item in ExitPoints)
            {
                item.Write(writer);
            }
            foreach (Byte item in JointIndices)
            {
                writer.Write(item);
            }
            foreach (UInt32 item in RigidModelIds)
            {
                writer.Write(item);
            }
            foreach (ITwinSerializable item in SkinInverseBindMatrices)
            {
                item.Write(writer);
            }
            writer.Write(SkinID);
            writer.Write(BlendSkinID);
            foreach (ITwinSerializable item in Collisions)
            {
                item.Write(writer);
            }
            foreach (Byte item in CollisionJointIndices)
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
