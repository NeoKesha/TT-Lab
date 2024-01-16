﻿using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Objects;

namespace TT_Lab.Rendering
{
    internal class SceneInstance
    {
        ObjectInstance instanceRender;
        ObjectInstanceData instanceData;
        vec3 position;
        vec3 rotation;
        vec3 offset;
        vec3 size;

        public SceneInstance(ObjectInstanceData instanceData, Dictionary<LabURI, List<IndexedBufferArray>> modelBufferCache, Scene parent)
        {
            this.instanceData = instanceData;
            instanceRender = new ObjectInstance(parent, instanceData, modelBufferCache);
            position = new vec3(-instanceData.Position.X, instanceData.Position.Y, instanceData.Position.Z);
            rotation = new vec3(instanceData.RotationX.GetRotation(), instanceData.RotationY.GetRotation(), instanceData.RotationZ.GetRotation());
            instanceRender.SetPositionAndRotation(position, rotation);
            var assetManager = AssetManager.Get();
            var objData = assetManager.GetAssetData<GameObjectData>(instanceData.ObjectId);
            if (objData.OGISlots[0] != LabURI.Empty)
            {
                var ogiData = assetManager.GetAssetData<OGIData>(objData.OGISlots[0]);
                size = new vec3();
                size.x = ogiData.BoundingBox[1].X - ogiData.BoundingBox[0].X;
                size.y = ogiData.BoundingBox[1].Y - ogiData.BoundingBox[0].Y;
                size.z = ogiData.BoundingBox[1].Z - ogiData.BoundingBox[0].Z;
                offset = new vec3(ogiData.BoundingBox[0].X, ogiData.BoundingBox[0].Y, ogiData.BoundingBox[0].Z);
            }
        }

        public void Select()
        {
            instanceRender.Select();
        }

        public void Deselect()
        {
            instanceRender.Deselect();
        }

        public ObjectInstance GetRenderable()
        {
            return instanceRender;
        }

        public vec3 GetPosition()
        {
            return position;
        }

        public vec3 GetOffset()
        {
            return offset;
        }
        public vec3 GetSize()
        {
            return size;
        }
        public vec3 GetRotation()
        {
            return rotation;
        }

        public mat4 GetTransform()
        {
            return instanceRender.GlobalTransform;
        }
    }
}
