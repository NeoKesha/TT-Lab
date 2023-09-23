using System;
using System.Collections.Generic;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Camera : SerializableInstance
    {
        public Camera(String package, String subpackage, UInt32 id, String name, String chunk, Int32 layId, PS2AnyCamera camera) : base(package, subpackage, id, name, chunk, layId)
        {
            assetData = new CameraData(camera);
            Parameters = new Dictionary<string, object?>
            {
                ["MainCamera1Type"] = null,
                ["MainCamera2Type"] = null
            };
        }

        public Camera()
        {
            Parameters = new Dictionary<string, object?>
            {
                ["MainCamera1Type"] = null,
                ["MainCamera2Type"] = null
            };
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(String json)
        {
            base.Deserialize(json);
            if (Parameters["MainCamera1Type"] != null)
            {
                Parameters["MainCamera1Type"] = Type.GetType((string)Parameters["MainCamera1Type"]!, false);
            }
            if (Parameters["MainCamera2Type"] != null)
            {
                Parameters["MainCamera2Type"] = Type.GetType((string)Parameters["MainCamera2Type"]!, false);
            }
        }

        public override void Serialize()
        {
            if (assetData != null)
            {
                var camData = (CameraData)assetData;
                if (camData.MainCamera1 != null)
                {
                    Parameters["MainCamera1Type"] = camData.MainCamera1.GetType();
                }
                if (camData.MainCamera2 != null)
                {
                    Parameters["MainCamera2Type"] = camData.MainCamera2.GetType();
                }
            }
            base.Serialize();
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new CameraViewModel(URI, parent);
            return viewModel;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new CameraData((Type?)Parameters["MainCamera1Type"], (Type?)Parameters["MainCamera2Type"]);
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
