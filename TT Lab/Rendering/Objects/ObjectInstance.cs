using GlmSharp;
using OpenTK.Mathematics;
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
        List<ModelBuffer> modelBuffers = new List<ModelBuffer>();
        Dictionary<LabURI, List<ModelBuffer>> modelBufferCache;

        Vector3 ambientColor = new Vector3();
        private vec3 pos = new vec3();
        private vec3 rot = new vec3();
        bool selected;

        public ObjectInstance(Scene root, ObjectInstanceData instance, Dictionary<LabURI, List<ModelBuffer>> modelBufferCache) : base(root)
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
            ambientColor.X = 0.5f;
            ambientColor.Y = 0.5f;
            ambientColor.Z = 0.5f;
            Opacity = 0.5f;
            selected = true;
        }

        public void Deselect()
        {
            ambientColor.X = 0.0f;
            ambientColor.Y = 0.0f;
            ambientColor.Z = 0.0f;
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
            shader.SetUniform3("AmbientMaterial", ambientColor.X, ambientColor.Y, ambientColor.Z);
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
                modelBuffers.Add(new ModelBuffer(Root, rigidModelData));
            }
            if (ogiData.Skin != LabURI.Empty)
            {
                var skin = assetManager.GetAssetData<SkinData>(ogiData.Skin);
                modelBuffers.Add(new ModelBuffer(Root, skin));
            }
            if (ogiData.BlendSkin != LabURI.Empty)
            {
                var blendSkin = assetManager.GetAssetData<BlendSkinData>(ogiData.BlendSkin);
                modelBuffers.Add(new ModelBufferBlendSkin(Root, blendSkin));
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
