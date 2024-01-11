﻿using GlmNet;
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
    internal class ObjectInstance : IRenderable
    {

        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        List<IndexedBufferArray> modelBuffers = new List<IndexedBufferArray>();
        Dictionary<LabURI, List<IndexedBufferArray>> modelBufferCache;

        float[]? transform;
        Vector3 ambientColor = new Vector3();

        public ObjectInstance(ObjectInstanceData instance, Dictionary<LabURI, List<IndexedBufferArray>> modelBufferCache)
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
            Deselect();
        }

        public void SetPositionAndRotation(vec3 pos, vec3 rot)
        {
            Matrix4 matrixPosition = Matrix4.CreateTranslation(pos.x, pos.y, pos.z);
            Matrix4 matrixRotationX, matrixRotationY, matrixRotationZ;
            Matrix4.CreateRotationX(rot.x, out matrixRotationX);
            Matrix4.CreateRotationY(rot.y, out matrixRotationY);
            Matrix4.CreateRotationZ(rot.z, out matrixRotationZ);
            Matrix4 modelTransform = matrixRotationZ * matrixRotationY * matrixRotationX;
            modelTransform *= matrixPosition;
            transform = MathExtension.Matrix4ToArray(modelTransform);
        }

        public void Select()
        {
            ambientColor.X = 0.1f;
            ambientColor.Y = 0.6f;
            ambientColor.Z = 0.1f;
        }

        public void Deselect()
        {
            ambientColor.X = 0.5f;
            ambientColor.Y = 0.5f;
            ambientColor.Z = 0.5f;
        }

        public void Bind()
        {
            Parent?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            Parent?.Renderer.RenderProgram.SetUniform3("AmbientMaterial", ambientColor.X, ambientColor.Y, ambientColor.Z);
            Parent?.Renderer.RenderProgram.SetUniform3("LightPosition", Parent.CameraPosition.x, Parent.CameraPosition.y, Parent.CameraPosition.z);
            Parent?.Renderer.RenderProgram.SetUniform3("LightDirection", -Parent.CameraDirection.x, Parent.CameraDirection.y, Parent.CameraDirection.z);
            Parent?.Renderer.RenderProgram.SetUniformMatrix4("Model", transform!);
        }

        public void Delete()
        {
            modelBuffers.Clear();
        }

        public void Render()
        {
            if (transform == null)
            {
                return;
            }

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
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(modelData.Vertexes[i].Select(v => v.Position).ToList(), modelData.Faces[i],
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
