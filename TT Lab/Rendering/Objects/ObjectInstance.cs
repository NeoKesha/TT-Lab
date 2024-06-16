using GlmSharp;
using SharpGL;
using System.Collections.Generic;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Objects
{
    public class ObjectInstance : BaseRenderable
    {
        private List<ModelBuffer> modelBuffers = new();
        private Dictionary<LabURI, List<ModelBuffer>> modelBufferCache;

        private vec3 ambientColor = new();
        private vec3 pos = new();
        private vec3 rot = new();
        private bool selected;

        public ObjectInstance(OpenGL gl, GLWindow window, Scene root, ObjectInstanceData instance, Dictionary<LabURI, List<ModelBuffer>> modelBufferCache) : base(gl, window, root)
        {
            this.modelBufferCache = modelBufferCache;
            var objURI = instance.ObjectId;
            if (modelBufferCache.ContainsKey(objURI))
            {
                SetupModelBufferFromCache(objURI);
            }
            else
            {
                SetupModelBuffer(objURI);
            }
            pos = new vec3(-instance.Position.X, instance.Position.Y, instance.Position.Z);
            rot = new vec3(instance.RotationX.GetRotation(), instance.RotationY.GetRotation(), instance.RotationZ.GetRotation());
            SetPositionAndRotation(pos, rot);
            Deselect();
        }

        public void SetPositionAndRotation(vec3 pos, vec3 rot)
        {
            mat4 matrixPosition = mat4.Translate(pos.x, pos.y, pos.z);
            mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
            rot = rot * 3.14f / 180.0f;
            matrixRotationX = mat4.RotateX(rot.x);
            matrixRotationY = mat4.RotateY(rot.y);
            matrixRotationZ = mat4.RotateZ(rot.z);
            LocalTransform = matrixPosition;
            LocalTransform *= matrixRotationZ * matrixRotationY * matrixRotationX;
        }

        public void Select()
        {
            ambientColor.x = 0.5f;
            ambientColor.y = 0.5f;
            ambientColor.z = 0.5f;
            Opacity = 0.5f;
            selected = true;
        }

        public void Deselect()
        {
            ambientColor.x = 0.0f;
            ambientColor.y = 0.0f;
            ambientColor.z = 0.0f;
            Opacity = 1.0f;
            selected = false;
        }

        public void Bind()
        {
        }

        public void Delete()
        {
            modelBuffers.Clear();
        }

        public override void SetUniforms(ShaderProgram shader)
        {
            base.SetUniforms(shader);

            shader.SetUniform1("Opacity", Opacity);
            shader.SetUniform3("AmbientMaterial", ambientColor.x, ambientColor.y, ambientColor.z);
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            Bind();
            // Have to blend buffers at render time because of caching
            var blendBuffers = Opacity < 1.0f;
            foreach (var modelBuffer in modelBuffers)
            {
                modelBuffer.Bind();
                if (blendBuffers)
                {
                    modelBuffer.EnableAlphaBlending();
                }
                modelBuffer.Render(shader, HasAlphaBlending());
                if (blendBuffers)
                {
                    modelBuffer.DisableAlphaBlending();
                }
                modelBuffer.Unbind();
            }
            Unbind();
        }

        public void Unbind()
        {
        }

        private void SetupModelBufferFromCache(LabURI uri)
        {
            var cache = modelBufferCache.GetValueOrDefault(uri);
            if (cache == null)
            {
                return;
            }

            foreach (var buffer in cache)
            {
                modelBuffers.Add(buffer);
            }
            modelBufferCache.Remove(uri);
        }
        private void ClearCacheEntry(LabURI uri)
        {
            var cache = modelBufferCache.GetValueOrDefault(uri);
            if (cache == null)
            {
                return;
            }

            modelBuffers.Clear();
            foreach (var buffer in cache)
            {
                buffer.Delete();
            }
        }

        private void SetupModelBuffer(LabURI uri)
        {
            var assetManager = AssetManager.Get();
            var objData = assetManager.GetAssetData<GameObjectData>(uri);
            if (objData.OGISlots[0] == LabURI.Empty)
            {
                return;
            }
            var ogiData = assetManager.GetAssetData<OGIData>(objData.OGISlots[0]);
            foreach (var rigidModel in ogiData.RigidModelIds)
            {
                if (rigidModel == LabURI.Empty)
                {
                    continue;
                }
                var rigidModelData = assetManager.GetAssetData<RigidModelData>(rigidModel);
                modelBuffers.Add(new ModelBuffer(GL, Window, Root, rigidModelData));
            }
            if (ogiData.Skin != LabURI.Empty)
            {
                var skin = assetManager.GetAssetData<SkinData>(ogiData.Skin);
                modelBuffers.Add(new ModelBuffer(GL, Window, Root, skin));
            }
            if (ogiData.BlendSkin != LabURI.Empty)
            {
                var blendSkin = assetManager.GetAssetData<BlendSkinData>(ogiData.BlendSkin);
                modelBuffers.Add(new ModelBufferBlendSkin(GL, Window, Root, blendSkin));
            }

            var cacheList = new List<ModelBuffer>();
            foreach (var buffer in modelBuffers)
            {
                cacheList.Add(buffer);
            }
            modelBufferCache.Add(uri, cacheList);
        }
    }
}
