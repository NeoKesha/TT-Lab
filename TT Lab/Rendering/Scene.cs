using GlmNet;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
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
    public class Scene : IRenderable
    {
        private Scene? parent = null;

        public Scene? Parent { get => parent; set => parent = value; }
        public float Opacity { get; set; } = 1.0f;
        public IRenderer Renderer { get; private set; }
        public vec3 CameraPosition { get => cameraPosition; }
        public vec3 CameraDirection { get => cameraDirection; }
        public int RenderFramebuffer { get; set; }
        public TextureBuffer ColorTextureNT { get => colorTextureNT; }

        // Rendering matrices and settings
        private mat4 projectionMat;
        private mat4 viewMat;
        private mat4 modelMat;
        private vec3 cameraPosition = new vec3(0.0f, 0.0f, 0.0f);
        private vec3 cameraDirection = new vec3(0, 0, -1);
        private vec3 cameraUp = new vec3(0, 1, 0);
        private vec2 resolution = new vec2(0, 0);
        private float cameraSpeed = 1.0f;
        private float cameraZoom = 90.0f;
        private bool canManipulateCamera = true;
        private ShaderProgram.LibShader libShader;

        // Scene rendering
        private readonly List<IRenderable> objectsTransparent = new List<IRenderable>();
        private readonly List<IRenderable> objectsOpaque = new List<IRenderable>();
        private readonly TextureBuffer colorTextureNT = new TextureBuffer(TextureTarget.Texture2DMultisample);
        private readonly FrameBuffer framebufferNT = new FrameBuffer();
        private readonly RenderBuffer depthRenderbuffer = new RenderBuffer();

        // Misc helper stuff
        private readonly Queue<Action> queuedRenderActions = new Queue<Action>();


        /// <summary>
        /// Constructor to setup the matrices
        /// </summary>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(float width, float height, ShaderProgram.LibShader libShader)
        {
            Preferences.PreferenceChanged += Preferences_PreferenceChanged;

            resolution.x = width;
            resolution.y = height;
            projectionMat = glm.perspective(glm.radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            modelMat = glm.scale(new mat4(1.0f), new vec3(1.0f));

            this.libShader = libShader;
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
            this(width, height, new ShaderProgram.LibShader { Path = "Shaders\\Light.frag", Type = ShaderType.FragmentShader })
        {
            // Collision renderer
            var colData = sceneTree.Find((avm) =>
            {
                return avm.Asset.Type == typeof(Assets.Instance.Collision);
            })!.Asset.GetData<CollisionData>();
            var colRender = new Objects.Collision(colData);
            colRender.Parent = this;
            objectsOpaque.Add(colRender);

            // Positions renderer
            var positions = sceneTree.Find(avm => avm.Alias == "Positions");
            foreach (var pos in positions!.Children)
            {
                var pRend = new Objects.Position((PositionViewModel)pos);
                pRend.Parent = this;
                objectsOpaque.Add(pRend);
            }

            // Triggers renderer
            var triggers = sceneTree.Find(avm => avm.Alias == "Triggers");
            foreach (var trg in triggers!.Children)
            {
                var trRend = new Objects.Trigger((TriggerViewModel)trg);
                trRend.Parent = this;
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
            
            renderObj.Parent = this;
        }

        public void SetCameraPosition(vec3 position)
        {
            cameraPosition = position;
        }

        /// <summary>
        /// Sets the matrix uniforms for object's rendering in 3D scene
        /// </summary>
        /// <param name="program"></param>
        public void SetPVMNShaderUniforms(ShaderProgram program)
        {
            program.SetUniformMatrix4("Projection", projectionMat.to_array());
            program.SetUniformMatrix4("View", viewMat.to_array());
            program.SetUniformMatrix4("Model", modelMat.to_array());
        }

        public void SetResolution(float width, float height)
        {
            resolution.x = width;
            resolution.y = height;
            ReallocateFramebuffer((int)width, (int)height);
        }

        public void Render()
        {
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
            // Reset modes
            GL.CullFace(CullFaceMode.Back);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Multisample);
            Unbind();
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
            direction.x = (float)Math.Cos(glm.radians(yaw_pitch.x)) * (float)Math.Cos(glm.radians(yaw_pitch.y));
            direction.y = (float)Math.Sin(glm.radians(yaw_pitch.y));
            direction.z = (float)Math.Sin(glm.radians(yaw_pitch.x)) * (float)Math.Cos(glm.radians(yaw_pitch.y));

            cameraDirection = glm.normalize(direction);
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
                        cameraPosition -= camSp * glm.cross(cameraDirection, cameraUp);
                        break;
                    case Key.D:
                        cameraPosition += camSp * glm.cross(cameraDirection, cameraUp);
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
            projectionMat = glm.perspective(glm.radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
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
                RenderSwitches.TranslucencyMethod.WBOIT => new WBOITRenderer(depthRenderbuffer, resolution.x, resolution.y, libShader),
                _ => new DDPRenderer(resolution.x, resolution.y, libShader),
            };
            Renderer.Scene = this;
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
