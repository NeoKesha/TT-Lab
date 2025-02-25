using System.Linq;
using GlmSharp;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;

namespace TT_Lab.Rendering.Objects.SceneInstances
{
    public abstract class SceneInstance
    {
        protected EditableObject InstanceRender;
        protected AbstractAssetData InstanceData;
        
        protected vec3 Position;
        protected vec3 Rotation;
        protected vec3 Offset;
        protected vec3 Size;

        protected SceneInstance(OgreWindow window, AbstractAssetData instanceData)
        {
            this.InstanceData = instanceData;
        }

        public T GetData<T>() where T : AbstractAssetData
        {
            return (T)InstanceData;
        }

        public AbstractAssetData GetData()
        {
            return InstanceData;
        }
        
        public abstract void SetPositionRotation(vec3 position, dvec3 rotation);

        public void Select()
        {
            InstanceRender.Select();
        }

        public void Deselect()
        {
            InstanceRender.Deselect();
        }

        public EditableObject GetRenderable()
        {
            return InstanceRender;
        }

        public vec3 GetPosition()
        {
            return Position;
        }

        public vec3 GetOffset()
        {
            return Offset;
        }
        public vec3 GetSize()
        {
            return Size;
        }
        public vec3 GetRotation()
        {
            return Rotation;
        }

        public mat4 GetTransform()
        {
            return InstanceRender.GetTransform();
        }
    }
}
