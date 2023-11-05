﻿using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class CameraData : AbstractAssetData
    {
        public CameraData()
        {
        }

        public CameraData(Type? mainCam1T, Type? mainCam2T)
        {
            if (mainCam1T != null)
            {
                MainCamera1 = (CameraSubBase)Activator.CreateInstance(mainCam1T)!;
            }
            if (mainCam2T != null)
            {
                MainCamera2 = (CameraSubBase)Activator.CreateInstance(mainCam2T)!;
            }
        }

        public CameraData(ITwinCamera camera) : this()
        {
            SetTwinItem(camera);
        }

        [JsonProperty(Required = Required.Always)]
        public TriggerData Trigger { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 CameraHeader { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkShort { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVector1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVector2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt5 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt6 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat5 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat6 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat7 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt7 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt8 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt9 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat8 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public CameraSubBase? MainCamera1 { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public CameraSubBase? MainCamera2 { get; set; }

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            base.Load(dataPath, settings);
        }

        protected override void Dispose(Boolean disposing)
        {
            Trigger.Dispose();
        }
        public override void Import(LabURI package, String? variant)
        {
            ITwinCamera camera = GetTwinItem<ITwinCamera>();
            Trigger = new TriggerData(package, variant, camera.CamTrigger);
            CameraHeader = camera.CameraHeader;
            UnkShort = camera.UnkShort;
            UnkFloat1 = camera.UnkFloat1;
            UnkVector1 = CloneUtils.Clone(camera.UnkVector1);
            UnkVector2 = CloneUtils.Clone(camera.UnkVector2);
            UnkFloat2 = camera.UnkFloat2;
            UnkFloat3 = camera.UnkFloat3;
            UnkInt1 = camera.UnkInt1;
            UnkInt2 = camera.UnkInt2;
            UnkInt3 = camera.UnkInt3;
            UnkInt4 = camera.UnkInt4;
            UnkInt5 = camera.UnkInt5;
            UnkInt6 = camera.UnkInt6;
            UnkFloat4 = camera.UnkFloat4;
            UnkFloat5 = camera.UnkFloat5;
            UnkFloat6 = camera.UnkFloat6;
            UnkFloat7 = camera.UnkFloat7;
            UnkInt7 = camera.UnkInt7;
            UnkInt8 = camera.UnkInt8;
            UnkInt9 = camera.UnkInt9;
            UnkFloat8 = camera.UnkFloat8;
            UnkByte = camera.UnkByte;
            if (camera.MainCamera1 != null)
            {
                MainCamera1 = (CameraSubBase)CloneUtils.DeepClone(camera.MainCamera1, camera.MainCamera1.GetType());
            }
            if (camera.MainCamera2 != null)
            {
                MainCamera2 = (CameraSubBase)CloneUtils.DeepClone(camera.MainCamera2, camera.MainCamera2.GetType());
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            var trigger = Trigger.Export(factory);
            trigger.Write(writer);
            // Reposition to where TriggerScripts start since they do not exist for the camera
            ms.Position -= 2 * 4;
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
            writer.Write((UInt32)(MainCamera1 == null ? ITwinCamera.CameraType.Null : MainCamera1.GetCameraType()));
            writer.Write((UInt32)(MainCamera2 == null ? ITwinCamera.CameraType.Null : MainCamera2.GetCameraType()));
            writer.Write(UnkByte);
            MainCamera1?.Write(writer);
            MainCamera2?.Write(writer);

            ms.Position = 0;
            return factory.GenerateCamera(ms);
        }
    }
}
