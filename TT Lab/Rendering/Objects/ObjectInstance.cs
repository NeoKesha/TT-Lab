using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class ObjectInstance : BaseRenderable
    {
        List<IndexedBufferArray> modelBuffers = new List<IndexedBufferArray>();
        Dictionary<LabURI, List<IndexedBufferArray>> modelBufferCache;

        Vector3 ambientColor = new Vector3();
        private vec3 pos = new vec3();
        private vec3 rot = new vec3();
        bool selected;

        public ObjectInstance(Scene root, ObjectInstanceData instance, Dictionary<LabURI, List<IndexedBufferArray>> modelBufferCache) : base(root)
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
            ambientColor.X = 0.1f;
            ambientColor.Y = 0.6f;
            ambientColor.Z = 0.1f;
            selected = true;
        }

        public void Deselect()
        {
            ambientColor.X = 0.5f;
            ambientColor.Y = 0.5f;
            ambientColor.Z = 0.5f;
            selected = false;
        }

        public void Bind()
        {
            Root.Renderer.RenderProgram.SetUniform1("Opacity", Opacity);
            Root.Renderer.RenderProgram.SetUniform3("AmbientMaterial", ambientColor.X, ambientColor.Y, ambientColor.Z);
            Root.Renderer.RenderProgram.SetUniform3("LightPosition", Root.CameraPosition.x, Root.CameraPosition.y, Root.CameraPosition.z);
            Root.Renderer.RenderProgram.SetUniform3("LightDirection", -Root.CameraDirection.x, Root.CameraDirection.y, Root.CameraDirection.z);
        }

        public void Delete()
        {
            modelBuffers.Clear();
        }

        protected override void RenderSelf()
        {
            Bind();
            foreach (var modelBuffer in modelBuffers)
            {
                modelBuffer.Bind();
                GL.DrawElements(PrimitiveType.Triangles, modelBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
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
                var modelData = assetManager.GetAssetData<ModelData>(rigidModelData.Model);
                for (var i = 0; i < modelData.Vertexes.Count; ++i)
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(modelData.Vertexes[i].Select(v => new vec3(v.Position.X, v.Position.Y, v.Position.Z)).ToList(), modelData.Faces[i],
                    modelData.Vertexes[i].Select((v) =>
                    {
                        var col = v.Color.GetColor();
                        var emit = v.EmitColor;
                        col.R = (Byte)Math.Min(col.R + emit.X, 255);
                        col.G = (Byte)Math.Min(col.G + emit.Y, 255);
                        col.B = (Byte)Math.Min(col.B + emit.Z, 255);
                        return System.Drawing.Color.FromArgb((int)col.ToARGB());
                    }).ToList()));
                }
            }

            var cacheList = new List<IndexedBufferArray>();
            foreach (var buffer in modelBuffers)
            {
                cacheList.Add(buffer);
            }
            modelBufferCache.Add(uri, cacheList);
        }
    }
}
