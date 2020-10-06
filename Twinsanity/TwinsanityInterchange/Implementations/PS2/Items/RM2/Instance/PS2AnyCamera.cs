using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Instance
{
    public class PS2AnyCamera : ITwinCamera
    {
        UInt32 id;
        Dictionary<UInt32, Type> subCamIdToCamera = new Dictionary<UInt32, Type>();
        public TwinTrigger CamTrigger { get; }
        public UInt32 CameraHeader { get; set; }
        public UInt16 UnkShort { get; set; }
        public Single UnkFloat1 { get; set; } // 10
        public Vector4 UnkVector1 { get; private set; }
        public Vector4 UnkVector2 { get; private set; } // 42
        public Single UnkFloat2 { get; set; } 
        public Single UnkFloat3 { get; set; } // 50
        public UInt32 UnkInt1 { get; set; }
        public UInt32 UnkInt2 { get; set; }
        public UInt32 UnkInt3 { get; set; }
        public UInt32 UnkInt4 { get; set; } // 66
        public UInt32 UnkInt5 { get; set; }
        public UInt32 UnkInt6 { get; set; } // 74
        public Single UnkFloat4 { get; set; }
        public Single UnkFloat5 { get; set; }
        public Single UnkFloat6 { get; set; }
        public Single UnkFloat7 { get; set; } // 90
        public UInt32 UnkInt7 { get; set; }
        public UInt32 UnkInt8 { get; set; } // 98
        public UInt32 UnkInt9 { get; set; } 
        public Single UnkFloat8 { get; set; } // 106
        public UInt32 TypeIndex1 { get; set; }
        public UInt32 TypeIndex2 { get; set; } // 114
        public Byte UnkByte { get; set; } // 115
        public CameraSubBase MainCamera1 { get; private set; }
        public CameraSubBase MainCamera2 { get; private set; }
        public PS2AnyCamera()
        {
            CamTrigger = new TwinTrigger();
            UnkVector1 = new Vector4();
            UnkVector2 = new Vector4();
            subCamIdToCamera.Add(0xA19, typeof(CameraSub0A19));
            subCamIdToCamera.Add(0x1C02, typeof(CameraSub1C02));
            subCamIdToCamera.Add(0x1C03, typeof(CameraSub1C03));
            subCamIdToCamera.Add(0x1C04, typeof(CameraSub1C04));
            subCamIdToCamera.Add(0x1C06, typeof(CameraSub1C06));
            subCamIdToCamera.Add(0x1C09, typeof(CameraSub1C09));
            subCamIdToCamera.Add(0x1C0B, typeof(CameraSub1C0B));
            subCamIdToCamera.Add(0x1C0C, typeof(CameraSub1C0C));
            subCamIdToCamera.Add(0x1C0D, typeof(CameraSub1C0D));
            subCamIdToCamera.Add(0x1C0F, typeof(CameraSub1C0F));
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            var mainCam1Len = MainCamera1 == null ? 0 : MainCamera1.GetLength();
            var mainCam2Len = MainCamera2 == null ? 0 : MainCamera2.GetLength();
            return CamTrigger.GetLength() + 115 + mainCam1Len + mainCam2Len;
        }

        public void Read(BinaryReader reader, int length)
        {
            CamTrigger.Header1 = reader.ReadUInt32();
            CamTrigger.Enabled = reader.ReadUInt32();
            CamTrigger.HeaderT = reader.ReadSingle();
            CamTrigger.Rotation.Read(reader, Constants.SIZE_VECTOR4);
            CamTrigger.Position.Read(reader, Constants.SIZE_VECTOR4);
            CamTrigger.Scale.Read(reader, Constants.SIZE_VECTOR4);
            reader.ReadUInt32();
            UInt32 instances_cnt = reader.ReadUInt32();
            CamTrigger.HeaderH = reader.ReadUInt32();
            CamTrigger.Instances.Clear();
            for (int i = 0; i < instances_cnt; ++i)
            {
                CamTrigger.Instances.Add(reader.ReadUInt16());
            }
            // Camera
            CameraHeader = reader.ReadUInt32();
            UnkShort = reader.ReadUInt16();
            UnkFloat1 = reader.ReadSingle();
            UnkVector1.Read(reader, Constants.SIZE_VECTOR4);
            UnkVector2.Read(reader, Constants.SIZE_VECTOR4);
            UnkFloat2 = reader.ReadSingle();
            UnkFloat3 = reader.ReadSingle();
            UnkInt1 = reader.ReadUInt32();
            UnkInt2 = reader.ReadUInt32();
            UnkInt3 = reader.ReadUInt32();
            UnkInt4 = reader.ReadUInt32();
            UnkInt5 = reader.ReadUInt32();
            UnkInt6 = reader.ReadUInt32();
            UnkFloat4 = reader.ReadSingle();
            UnkFloat5 = reader.ReadSingle();
            UnkFloat6 = reader.ReadSingle();
            UnkFloat7 = reader.ReadSingle();
            UnkInt7 = reader.ReadUInt32();
            UnkInt8 = reader.ReadUInt32();
            UnkInt9 = reader.ReadUInt32();
            UnkFloat8 = reader.ReadSingle();
            TypeIndex1 = reader.ReadUInt32();
            TypeIndex2 = reader.ReadUInt32();
            UnkByte = reader.ReadByte();
            if (TypeIndex1 != 3 && subCamIdToCamera.ContainsKey(TypeIndex1))
            {
                MainCamera1 = (CameraSubBase)Activator.CreateInstance(subCamIdToCamera[TypeIndex1]);
                MainCamera1.Read(reader, MainCamera1.GetLength());
            }
            if (TypeIndex2 != 3 && subCamIdToCamera.ContainsKey(TypeIndex2))
            {
                MainCamera2 = (CameraSubBase)Activator.CreateInstance(subCamIdToCamera[TypeIndex2]);
                MainCamera2.Read(reader, MainCamera2.GetLength());
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(CamTrigger.Header1);
            writer.Write(CamTrigger.Enabled);
            writer.Write(CamTrigger.HeaderT);
            CamTrigger.Rotation.Write(writer);
            CamTrigger.Position.Write(writer);
            CamTrigger.Scale.Write(writer);
            writer.Write(CamTrigger.Instances.Count);
            writer.Write(CamTrigger.Instances.Count);
            writer.Write(CamTrigger.HeaderH);
            for (int i = 0; i < CamTrigger.Instances.Count; ++i)
            {
                writer.Write(CamTrigger.Instances[i]);
            }
            //
            writer.Write(CameraHeader);
            writer.Write(UnkShort);
            writer.Write(UnkFloat1);
            UnkVector1.Write(writer);
            UnkVector2.Write(writer);
            writer.Write(UnkFloat2);
            writer.Write(UnkFloat3);
            writer.Write(UnkInt1);
            writer.Write(UnkInt2);
            writer.Write(UnkInt3);
            writer.Write(UnkInt4);
            writer.Write(UnkInt5);
            writer.Write(UnkInt6);
            writer.Write(UnkFloat4);
            writer.Write(UnkFloat5);
            writer.Write(UnkFloat6);
            writer.Write(UnkFloat7);
            writer.Write(UnkInt7);
            writer.Write(UnkInt8);
            writer.Write(UnkInt9);
            writer.Write(UnkFloat8);
            writer.Write(TypeIndex1);
            writer.Write(TypeIndex2);
            writer.Write(UnkByte);
            if (MainCamera1 != null)
            {
                MainCamera1.Write(writer);
            }
            if (MainCamera2 != null)
            {
                MainCamera2.Write(writer);
            }
        }
    }
}
