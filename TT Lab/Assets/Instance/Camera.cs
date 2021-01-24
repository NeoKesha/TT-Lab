﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Camera : SerializableInstance
    {
        public Camera(UInt32 id, String name, String chunk, Int32 layId, PS2AnyCamera camera) : base(id, name, chunk, layId)
        {
            assetData = new CameraData(camera);
        }

        public Camera()
        {
            Parameters = new Dictionary<string, object?>();
            Parameters["MainCamera1Type"] = null;
            Parameters["MainCamera2Type"] = null;
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

        public override AssetViewModel GetViewModel(AssetViewModel parent = null)
        {
            if (viewModel == null)
            {
                viewModel = new CameraViewModel(UUID, parent);
            }
            return viewModel;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                Type? type1 = null;
                if (Parameters["MainCamera1Type"] != null)
                {
                    type1 = Type.GetType((string)Parameters["MainCamera1Type"]!, false);
                }
                Type? type2 = null;
                if (Parameters["MainCamera2Type"] != null)
                {
                    type2 = Type.GetType((string)Parameters["MainCamera2Type"]!, false);
                }
                assetData = new CameraData(type1, type2);
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
