using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Renderers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Editors.Instance;

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
        private readonly InputController inputController;

        // Scene rendering
        private readonly List<IRenderable> renderableObjects = new();
        private readonly TextureBuffer colorTextureNT = new(TextureTarget.Texture2DMultisample);
        private readonly FrameBuffer framebufferNT = new();
        private readonly RenderBuffer depthRenderbuffer = new();

        // Misc helper stuff
        private readonly Queue<Action> queuedRenderActions = new();
        private readonly Dictionary<LabURI, List<ModelBuffer>> modelBufferCache = new();
        //private readonly List<SceneInstance> sceneInstances = new();
        private readonly PrimitiveRenderer primitiveRenderer = new PrimitiveRenderer();


        /// <summary>
        /// Constructor to setup the matrices
        /// </summary>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(float width, float height, ShaderStorage.LibraryFragmentShaders fragmentLibraryShader, ShaderStorage.LibraryVertexShaders vertexLibraryShader = ShaderStorage.LibraryVertexShaders.VertexShading) : base(null)
        {
            Preferences.PreferenceChanged += Preferences_PreferenceChanged;

            ShaderStorage.BuildShaderCache();

            inputController = Caliburn.Micro.IoC.Get<InputController>();

            resolution.x = width;
            resolution.y = height;
            projectionMat = mat4.Perspective(glm.Radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = mat4.LookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);

            this.fragmentLibraryShader = fragmentLibraryShader;
            this.vertexLibraryShader = vertexLibraryShader;
            ReallocateFramebuffer((int)resolution.x, (int)resolution.y);
            framebufferNT.Bind();
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, colorTextureNT.Buffer, 0);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthRenderbuffer.Buffer);
            SetupTransparencyRender();

            primitiveRenderer.Init(this);
        }

        /// <summary>
        /// Constructor to perform a full chunk render
        /// </summary>
        /// <param name="sceneTree">Tree of chunk resources collision data, positions, cameras, etc.</param>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(Caliburn.Micro.BindableCollection<ResourceTreeElementViewModel> sceneTree, float width, float height) :
            this(width, height, ShaderStorage.LibraryFragmentShaders.Light)
        {
            LocalTransform = mat4.Identity;

            // Collision renderer
            var colData = sceneTree.First((avm) =>
            {
                return avm.Asset.Type == typeof(Assets.Instance.Collision);
            })!.Asset.GetData<CollisionData>();
            var colRender = new Objects.Collision(this, colData);
            AddRender(colRender);

            // Positions renderer
            //var positions = sceneTree.First(avm => avm.Alias == "Positions");
            //foreach (var pos in positions!.Children)
            //{
            //    var pRend = new Objects.Position(this, (PositionViewModel)pos);
            //    AddRender(pRend);
            //}

            //// Triggers renderer
            //var triggers = sceneTree.First(avm => avm.Alias == "Triggers");
            //foreach (var trg in triggers!.Children)
            //{
            //    var trRend = new Objects.Trigger(this, (TriggerViewModel)trg);
            //    AddRender(trRend);
            //}
        }

        public SceneInstance AddObjectInstance(ObjectInstanceData instData)
        {
            var sceneInstance = new SceneInstance(instData, modelBufferCache, this);
            var pRend = sceneInstance.GetRenderable();

            AddRender(pRend);
            return sceneInstance;
        }

        public vec3 GetCameraPosition()
        {
            return cameraPosition;
        }

        public vec3 GetRayFromViewport(float x, float y)
        {
            var win = new vec3(x, resolution.y - y, 0.0f);
            var view = new vec4(0, 0, resolution.x, resolution.y);
            var worldPos = mat4.UnProject(win, viewMat, projectionMat, view);
            return glm.Normalized(worldPos - cameraPosition);
        }

        /// <summary>
        /// Adds new renderable object to the scene. Safe to do anywhere. Object will be added to the render on next render frame.
        /// </summary>
        /// <param name="renderObj">Object to add</param>
        public void AddRender(IRenderable renderObj)
        {
            queuedRenderActions.Enqueue(() =>
            {
                renderableObjects.Add(renderObj);
                AddChild(renderObj);
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
            program.SetUniformMatrix4("StartProjection", projectionMat.Values1D);
            program.SetUniformMatrix4("StartView", viewMat.Values1D);
            program.SetUniformMatrix4("StartModel", WorldTransform.Values1D);
            program.SetUniform1("Time", time);
            program.SetUniform2("Resolution", resolution.x, resolution.y);
            program.SetUniform3("LightPosition", CameraPosition.x, CameraPosition.y, CameraPosition.z);
            program.SetUniform3("LightDirection", -CameraDirection.x, CameraDirection.y, CameraDirection.z);
        }

        public void SetResolution(float width, float height)
        {
            resolution.x = width;
            resolution.y = height;
            ReallocateFramebuffer((int)width, (int)height);
        }

        public override void Render(ShaderProgram? _s = null, bool _b = false)
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
            Renderer.Render(renderableObjects);
            // Render HUD

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
            Unbind();
            timer.Stop();
            time += timer.Elapsed.Microseconds;
        }

        protected override void RenderSelf(ShaderProgram shader)
        {

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
            foreach (var @object in renderableObjects)
            {
                @object.Delete();
            }
            renderableObjects.Clear();
            foreach (var modelBuffers in modelBufferCache.Values)
            {
                foreach (var buffer in modelBuffers)
                {
                    buffer.Delete();
                }
                modelBuffers.Clear();
            }
            modelBufferCache.Clear();
            primitiveRenderer.Terminate();
            Preferences.PreferenceChanged -= Preferences_PreferenceChanged;
        }

        public void DrawBox(vec3 position)
        {
            DrawBox(position, vec3.Zero, vec3.Ones);
        }

        public void DrawBox(vec3 position, vec3 rotation, vec3 scale)
        {
            DrawBox(position, rotation, scale, vec4.Ones);
        }

        public void DrawBox(vec3 position, vec3 rotation, vec3 scale, vec4 color)
        {
            rotation = rotation * 3.14f / 180.0f;
            mat4 matrixPosition = mat4.Translate(position.x, position.y, position.z);
            mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
            matrixRotationX = mat4.RotateX(rotation.x);
            matrixRotationY = mat4.RotateY(rotation.y);
            matrixRotationZ = mat4.RotateZ(rotation.z);
            mat4 matrixScale = mat4.Scale(scale);
            mat4 transform = WorldTransform;

            transform *= matrixPosition;
            transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
            transform *= matrixScale;
            DrawBox(transform, color);
        }

        public void DrawBox(mat4 transform, vec4 color)
        {
            primitiveRenderer.DrawBox(transform, color);
        }

        public void DrawCircle(vec3 position)
        {
            DrawCircle(position, vec3.Zero, vec3.Ones);
        }

        public void DrawCircle(vec3 position, vec3 rotation, vec3 scale)
        {
            DrawCircle(position, rotation, scale, vec4.Ones);
        }

        public void DrawCircle(vec3 position, vec3 rotation, vec3 scale, vec4 color)
        {
            rotation = rotation * 3.14f / 180.0f;
            mat4 matrixPosition = mat4.Translate(position.x, position.y, position.z);
            mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
            matrixRotationX = mat4.RotateX(rotation.x);
            matrixRotationY = mat4.RotateY(rotation.y);
            matrixRotationZ = mat4.RotateZ(rotation.z);
            mat4 matrixScale = mat4.Scale(scale);
            mat4 transform = WorldTransform;

            transform *= matrixPosition;
            transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
            transform *= matrixScale;
            DrawCircle(transform, color);
        }

        public void DrawCircle(mat4 transform, vec4 color)
        {
            primitiveRenderer.DrawCircle(transform, color);
        }

        public void DrawLine(vec3 point1, vec3 point2, vec4 color)
        {
            DrawLine(point1, point2, color, WorldTransform);
        }

        public void DrawLine(vec3 point1, vec3 point2, vec4 color, mat4 parent)
        {
            var scaleX = (point2 - point1).Length;
            vec3 direction = (point2 - point1).Normalized;
            vec3 scale = new vec3(scaleX, 1.0f, 1.0f);
            mat4 matrixPosition = mat4.Translate(point1.x, point1.y, point1.z);
            mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
            var angleXZ = (float)glm.Angle(new vec2(direction.x, direction.z));
            var angleY = glm.Acos(glm.Dot(direction, new vec3(direction.x, 0, direction.z)));
            matrixRotationX = mat4.RotateX(0);
            matrixRotationY = mat4.RotateY(angleXZ);
            matrixRotationZ = mat4.RotateZ(-angleY);

            mat4 transform = parent;
            transform *= matrixPosition;
            transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
            transform *= mat4.Scale(scale);
            primitiveRenderer.DrawLine(transform, color);
        }

        public void DrawSimpleAxis(vec3 position)
        {
            DrawSimpleAxis(position, vec3.Zero);
        }

        public void DrawSimpleAxis(vec3 position, vec3 rotation)
        {
            DrawSimpleAxis(position, rotation, vec3.Ones);
        }

        public void DrawSimpleAxis(vec3 position, vec3 rotation, vec3 scale)
        {
            rotation = rotation * 3.14f / 180.0f;
            mat4 matrixPosition = mat4.Translate(position.x, position.y, position.z);
            mat4 matrixRotationX, matrixRotationY, matrixRotationZ;
            matrixRotationX = mat4.RotateX(rotation.x);
            matrixRotationY = mat4.RotateY(rotation.y);
            matrixRotationZ = mat4.RotateZ(rotation.z);
            mat4 matrixScale = mat4.Scale(scale);
            mat4 transform = mat4.Identity;

            transform *= matrixPosition;
            transform *= matrixRotationZ * matrixRotationY * matrixRotationX;
            transform *= matrixScale;
            DrawSimpleAxis(transform);
        }

        public void DrawSimpleAxis(mat4 transform)
        {
            primitiveRenderer.DrawSimpleAxis(transform);
        }

        public void SetCameraSpeed(float s)
        {
            cameraSpeed = s;
        }

        public void DisableCameraManipulation()
        {
            canManipulateCamera = false;
        }

        private vec2 yaw_pitch = new vec2(-90, 0);
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

        public void HandleInputs(InputController inputController)
        {

        }

        public void Move()
        {
            if (!canManipulateCamera) return;

            if (inputController.Alt || inputController.Ctrl)
            {
                return;
            }

            var camSp = cameraSpeed * (inputController.Shift ? 5.0f : 1.0f);
            if (inputController.IsKeyPressed(Key.W))
            {
                cameraPosition += camSp * cameraDirection;
            }
            if (inputController.IsKeyPressed(Key.S))
            {
                cameraPosition -= camSp * cameraDirection;
            }
            if (inputController.IsKeyPressed(Key.A))
            {
                cameraPosition -= camSp * glm.Cross(cameraDirection, cameraUp);
            }
            if (inputController.IsKeyPressed(Key.D))
            {
                cameraPosition += camSp * glm.Cross(cameraDirection, cameraUp);
            }
            if (inputController.IsKeyPressed(Key.Q))
            {
                cameraPosition.y -= camSp;
            }
            if (inputController.IsKeyPressed(Key.E))
            {
                cameraPosition.y += camSp;
            }
        }

        public void PreRender()
        {
            foreach (var @object in renderableObjects)
            {
                @object.PreRender();
            }
        }

        public void PostRender()
        {
            foreach (var @object in renderableObjects)
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
