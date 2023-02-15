using GlmNet;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Renderers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using System.Windows.Input;

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

        // Rendering matrices and settings
        private mat4 projectionMat;
        private mat4 viewMat;
        private mat4 modelMat;
        private mat3 normalMat;
        private vec3 cameraPosition = new vec3(0.0f, 0.0f, 0.0f);
        private vec3 cameraDirection = new vec3(0, 0, -1);
        private vec3 cameraUp = new vec3(0, 1, 0);
        private vec2 resolution = new vec2(0, 0);
        private float cameraSpeed = 1.0f;
        private float cameraZoom = 90.0f;
        private bool canManipulateCamera = true;
        private ShaderProgram.LibShader libShader;

        // Scene rendering
        private readonly List<IRenderable> objects = new List<IRenderable>();

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
            var modelView = viewMat * modelMat;
            normalMat = modelView.to_mat3();

            this.libShader = libShader;
            ReallocateFramebuffer((int)resolution.x, (int)resolution.y);
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
            var colData = (CollisionData)sceneTree.Find((avm) =>
            {
                return avm.Asset.Type == typeof(Assets.Instance.Collision);
            })!.Asset.GetData();
            var colRender = new Objects.Collision(colData);
            colRender.Parent = this;
            objects.Add(colRender);

            // Positions renderer
            var positions = sceneTree.Find(avm => avm.Alias == "Positions");
            foreach (var pos in positions!.Children)
            {
                var pRend = new Objects.Position((PositionViewModel)pos);
                pRend.Parent = this;
                objects.Add(pRend);
            }

            // Triggers renderer
            var triggers = sceneTree.Find(avm => avm.Alias == "Triggers");
            foreach (var trg in triggers!.Children)
            {
                var trRend = new Objects.Trigger((TriggerViewModel)trg);
                trRend.Parent = this;
                objects.Add(trRend);
            }
        }

        public void AddRender(IRenderable renderObj)
        {
            objects.Add(renderObj);
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
            program.SetUniformMatrix3("NormalMatrix", normalMat.to_array());
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
            GL.Enable(EnableCap.Blend);
            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Lequal);
            // Opaque rendering color
            Bind();
            float[] clearColorNT = System.Drawing.Color.LightGray.ToArray();
            float clearDepth = 1f;
            GL.ClearBuffer(ClearBuffer.Color, 0, clearColorNT);
            GL.ClearBuffer(ClearBuffer.Depth, 0, ref clearDepth);
            // Transparency rendering
            Renderer.Render(objects);

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
            Renderer.Delete();
            foreach (var @object in objects)
            {
                @object.Delete();
            }
            objects.Clear();
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
            foreach (var @object in objects)
            {
                @object.PreRender();
            }
        }

        public void PostRender()
        {
            foreach (var @object in objects)
            {
                @object.PostRender();
            }
        }

        private void UpdateMatrices()
        {
            projectionMat = glm.perspective(glm.radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = glm.lookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
            var modelView = viewMat * modelMat;
            normalMat = modelView.to_mat3();
        }

        private void ReallocateFramebuffer(int width, int height)
        {
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
            switch (method)
            {
                default:
                    Renderer = new BasicRenderer(libShader);
                    break;
            }
            Renderer.Scene = this;
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
