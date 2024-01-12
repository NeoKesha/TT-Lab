using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        public SceneInstance(ObjectInstanceData instanceData, Dictionary<LabURI, List<IndexedBufferArray>> modelBufferCache, Scene parent)
        {
            this.instanceData = instanceData;
            instanceRender = new ObjectInstance(parent, instanceData, modelBufferCache);
            position = new vec3(-instanceData.Position.X, instanceData.Position.Y, instanceData.Position.Z);
            rotation = new vec3(instanceData.RotationX.GetRotation(), instanceData.RotationY.GetRotation(), instanceData.RotationZ.GetRotation());
            instanceRender.SetPositionAndRotation(position, rotation);
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

        public vec3 GetRotation()
        {
            return rotation;
        }
    }
}
