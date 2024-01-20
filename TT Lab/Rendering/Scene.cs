﻿using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using TT_Lab.AssetData.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Renderers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Rendering
{
    /// <summary>
    /// A 3D scene to render to
    /// </summary>
    public class Scene : BaseRenderable
    {
        public IRenderer Renderer { get; private set; }
        public vec3 CameraPosition { get => cameraPosition; }
        public vec3 CameraDirection { get => cameraDirection; }
        public int RenderFramebuffer { get; set; }
        public TextureBuffer ColorTextureNT { get => colorTextureNT; }

        // Rendering matrices and settings
        private mat4 projectionMat;
        private mat4 viewMat;
        private mat4 modelMat;
        private vec3 cameraPosition = new(0.0f, 0.0f, 0.0f);
        private vec3 cameraDirection = new(0, 0, -1);
        private vec3 cameraUp = new(0, 1, 0);
        private vec2 resolution = new(1, 1);
        private float time = 0.0f;
        private float cameraSpeed = 1.0f;
        private float cameraZoom = 90.0f;
        private bool canManipulateCamera = true;
        private ShaderStorage.LibraryFragmentShaders fragmentLibraryShader;
        private ShaderStorage.LibraryVertexShaders vertexLibraryShader;
        private Stopwatch timer = new();

        // Scene rendering
        private readonly List<IRenderable> objectsTransparent = new();
        private readonly List<IRenderable> objectsOpaque = new();
        private readonly TextureBuffer colorTextureNT = new(TextureTarget.Texture2DMultisample);
        private readonly FrameBuffer framebufferNT = new();
        private readonly RenderBuffer depthRenderbuffer = new();

        // Misc helper stuff
        private readonly Queue<Action> queuedRenderActions = new();


        /// <summary>
        /// Constructor to setup the matrices
        /// </summary>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(float width, float height, ShaderStorage.LibraryFragmentShaders fragmentLibraryShader, ShaderStorage.LibraryVertexShaders vertexLibraryShader = ShaderStorage.LibraryVertexShaders.VertexShading) : base(null)
        {
            Preferences.PreferenceChanged += Preferences_PreferenceChanged;

            ShaderStorage.BuildShaderCache();

            resolution.x = width;
            resolution.y = height;
            projectionMat = mat4.Perspective(glm.Radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = mat4.LookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            modelMat = mat4.Scale(new vec3(1.0f));

            this.fragmentLibraryShader = fragmentLibraryShader;
            this.vertexLibraryShader = vertexLibraryShader;
            ReallocateFramebuffer((int)resolution.x, (int)resolution.y);
            framebufferNT.Bind();
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, colorTextureNT.Buffer, 0);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthRenderbuffer.Buffer);
            SetupTransparencyRender();
        }

        /// <summary>
        /// Constructor to perform a full chunk render
        /// </summary>
        /// <param name="sceneTree">Tree of chunk resources collision data, positions, cameras, etc.</param>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(List<AssetViewModel> sceneTree, float width, float height) :
            this(width, height, ShaderStorage.LibraryFragmentShaders.Light)
        {
            // Collision renderer
            var colData = sceneTree.Find((avm) =>
            {
                return avm.Asset.Type == typeof(Assets.Instance.Collision);
            })!.Asset.GetData<CollisionData>();
            var colRender = new Objects.Collision(this, colData);
            objectsOpaque.Add(colRender);

            // Positions renderer
            var positions = sceneTree.Find(avm => avm.Alias == "Positions");
            foreach (var pos in positions!.Children)
            {
                var pRend = new Objects.Position(this, (PositionViewModel)pos);
                objectsOpaque.Add(pRend);
            }

            // Triggers renderer
            var triggers = sceneTree.Find(avm => avm.Alias == "Triggers");
            foreach (var trg in triggers!.Children)
            {
                var trRend = new Objects.Trigger(this, (TriggerViewModel)trg);
                objectsTransparent.Add(trRend);
            }
        }

        /// <summary>
        /// Adds new renderable object to the scene. Safe to do anywhere. Object will be added to the render on next render frame.
        /// </summary>
        /// <param name="renderObj">Object to add</param>
        /// <param name="transparent">Whether the object is transparent and goes through translucency pipeline</param>
        public void AddRender(IRenderable renderObj, bool transparent = true)
        {
            queuedRenderActions.Enqueue(() => {
                if (transparent)
                {
                    objectsTransparent.Add(renderObj);
                }
                else
                {
                    objectsOpaque.Add(renderObj);
                }
            });
        }

        public void SetCameraPosition(vec3 position)
        {
            cameraPosition = position;
        }

        /// <summary>
        /// Sets the uniforms that are accessible throughout every shader
        /// </summary>
        /// <param name="program"></param>
        public void SetGlobalUniforms(ShaderProgram program)
        {
            program.SetUniformMatrix4("Projection", projectionMat.Values1D);
            program.SetUniformMatrix4("View", viewMat.Values1D);
            program.SetUniformMatrix4("Model", modelMat.Values1D);
            program.SetUniform1("Time", time);
            program.SetUniform2("Resolution", resolution.x, resolution.y);
        }

        public void SetResolution(float width, float height)
        {
            resolution.x = width;
            resolution.y = height;
            ReallocateFramebuffer((int)width, (int)height);
        }

        public override void Render()
        {
            timer = Stopwatch.StartNew();
            foreach (var a in queuedRenderActions)
            {
                a.Invoke();
            }
            queuedRenderActions.Clear();
            UpdateMatrices();
            Bind();
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Multisample);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);
            framebufferNT.Bind();
            Bind();
            float[] clearColorNT = System.Drawing.Color.LightGray.ToArray();
            float clearDepth = 1f;
            GL.ClearBuffer(ClearBuffer.Color, 0, clearColorNT);
            GL.ClearBuffer(ClearBuffer.Depth, 0, ref clearDepth);
            // Render all objects
            Renderer.RenderOpaque(objectsOpaque);
            Renderer.Render(objectsTransparent);
            // Post process effects
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Multisample);
            Renderer.PostProcess();
            // Reset modes
            GL.CullFace(CullFaceMode.Back);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Multisample);
            Renderer.PostProcess();
            Unbind();
            timer.Stop();
            time += timer.Elapsed.Microseconds;
        }

        public void Bind()
        {
        }

        public void Unbind()
        {
        }

        public void Delete()
        {
            colorTextureNT.Delete();
            framebufferNT.Delete();
            depthRenderbuffer.Delete();
            Renderer.Delete();
            foreach (var @object in objectsOpaque)
            {
                @object.Delete();
            }
            foreach (var @object in objectsTransparent)
            {
                @object.Delete();
            }
            objectsTransparent.Clear();
            objectsOpaque.Clear();
            Preferences.PreferenceChanged -= Preferences_PreferenceChanged;
        }

        public void SetCameraSpeed(float s)
        {
            cameraSpeed = s;
        }

        public void DisableCameraManipulation()
        {
            canManipulateCamera = false;
        }

        private vec2 yaw_pitch;
        public void RotateView(Vector2 rot)
        {
            if (!canManipulateCamera || rot.Length == 0) return;

            rot.Normalize();
            yaw_pitch.x += rot.X;
            yaw_pitch.y += rot.Y;

            // Avoid gimbal lock
            if (yaw_pitch.y > 89f)
            {
                yaw_pitch.y = 89f;
            }
            if (yaw_pitch.y < -89f)
            {
                yaw_pitch.y = -89f;
            }

            vec3 direction;
            direction.x = (float)Math.Cos(glm.Radians(yaw_pitch.x)) * (float)Math.Cos(glm.Radians(yaw_pitch.y));
            direction.y = (float)Math.Sin(glm.Radians(yaw_pitch.y));
            direction.z = (float)Math.Sin(glm.Radians(yaw_pitch.x)) * (float)Math.Cos(glm.Radians(yaw_pitch.y));

            cameraDirection = glm.Normalized(direction);
        }

        public void ZoomView(float z)
        {
            if (!canManipulateCamera) return;

            z *= -0.01f;
            cameraZoom = (z + cameraZoom).Clamp(10.0f, 100.0f);
        }

        public void HandleInputs(List<Key> keysPressed)
        {

        }

        public void Move(List<Key> keysPressed)
        {
            if (!canManipulateCamera) return;

            var camSp = cameraSpeed;

            if (keysPressed.Contains(Key.LeftShift) || keysPressed.Contains(Key.RightShift))
            {
                camSp *= 5;
            }

            foreach (var keyPressed in keysPressed)
            {
                switch (keyPressed)
                {
                    case Key.W:
                        cameraPosition += camSp * cameraDirection;
                        break;
                    case Key.S:
                        cameraPosition -= camSp * cameraDirection;
                        break;
                    case Key.A:
                        cameraPosition -= camSp * glm.Cross(cameraDirection, cameraUp);
                        break;
                    case Key.D:
                        cameraPosition += camSp * glm.Cross(cameraDirection, cameraUp);
                        break;
                }
            }
        }

        public void PreRender()
        {
            foreach (var @object in objectsOpaque)
            {
                @object.PreRender();
            }
            foreach (var @object in objectsTransparent)
            {
                @object.PreRender();
            }
        }

        public void PostRender()
        {
            foreach (var @object in objectsOpaque)
            {
                @object.PostRender();
            }
            foreach (var @object in objectsTransparent)
            {
                @object.PostRender();
            }
        }

        private void UpdateMatrices()
        {
            projectionMat = mat4.Perspective(glm.Radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = mat4.LookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }

        private void ReallocateFramebuffer(int width, int height)
        {
            colorTextureNT.Bind();
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Rgb16f, width, height, true);
            depthRenderbuffer.Bind();
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, 4, RenderbufferStorage.DepthComponent, width, height);
            Renderer?.ReallocateFramebuffer(width, height);
        }

        private void Preferences_PreferenceChanged(Object? sender, Preferences.PreferenceChangedArgs e)
        {
            if (e.PreferenceName == Preferences.TranslucencyMethod)
            {
                // Update renderer on next render frame
                queuedRenderActions.Enqueue(() =>
                {
                    SetupTransparencyRender();
                });
            }
        }

        private void SetupTransparencyRender()
        {
            var method = Preferences.GetPreference<RenderSwitches.TranslucencyMethod>(Preferences.TranslucencyMethod);
            Renderer?.Delete();
            Renderer = method switch
            {
                RenderSwitches.TranslucencyMethod.WBOIT => new WBOITRenderer(depthRenderbuffer, resolution.x, resolution.y, fragmentLibraryShader, vertexLibraryShader),
                _ => new BasicRenderer(fragmentLibraryShader, vertexLibraryShader)
            };
            Renderer.Scene = this;
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
