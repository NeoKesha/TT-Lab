using GlmSharp;
using SharpGL;
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
        public vec3 CameraPosition { get => cameraPosition; }
        public vec3 CameraDirection { get => cameraDirection; }

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
        private readonly InputController inputController;

        // Misc helper stuff
        private readonly Queue<Action> queuedRenderActions = new();
        private readonly Dictionary<LabURI, List<ModelBuffer>> modelBufferCache = new();
        //private readonly List<SceneInstance> sceneInstances = new();


        /// <summary>
        /// Constructor to setup the matrices
        /// </summary>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(OpenGL gl, GLWindow window, float width, float height) : base(gl, window, null)
        {
            inputController = Caliburn.Micro.IoC.Get<InputController>();

            resolution.x = width;
            resolution.y = height;
            projectionMat = mat4.Perspective(glm.Radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = mat4.LookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }

        /// <summary>
        /// Constructor to perform a full chunk render
        /// </summary>
        /// <param name="sceneTree">Tree of chunk resources collision data, positions, cameras, etc.</param>
        /// <param name="width">Viewport render width</param>
        /// <param name="height">Viewport render height</param>
        public Scene(OpenGL gl, GLWindow window, Caliburn.Micro.BindableCollection<ResourceTreeElementViewModel> sceneTree, float width, float height) :
            this(gl, window, width, height)
        {
            LocalTransform = mat4.Identity;

            // Collision renderer
            var colData = sceneTree.First((avm) =>
            {
                return avm.Asset.Type == typeof(Assets.Instance.Collision);
            })!.Asset.GetData<CollisionData>();
            var colRender = new Objects.Collision(gl, window, this, colData);
            AddChild(colRender);

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
            var sceneInstance = new SceneInstance(GL, Window, instData, modelBufferCache, this);
            var pRend = sceneInstance.GetRenderable();

            AddChild(pRend);
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
        [Obsolete("Use AddChild method instead", true)]
        public void AddRender(IRenderable renderObj)
        {
            queuedRenderActions.Enqueue(() =>
            {
                AddChild(renderObj);
            });
        }

        public override void AddChild(IRenderable child)
        {
            queuedRenderActions.Enqueue(() =>
            {
                base.AddChild(child);
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

        public override void Render(ShaderProgram? _s = null, bool _b = false)
        {
            foreach (var a in queuedRenderActions)
            {
                a.Invoke();
            }
            queuedRenderActions.Clear();
            UpdateMatrices();
            SetGlobalUniforms(_s);
            base.Render(_s, _b);
        }

        protected override void RenderSelf(ShaderProgram shader)
        {

        }

        public void Delete()
        {
            foreach (var modelBuffers in modelBufferCache.Values)
            {
                foreach (var buffer in modelBuffers)
                {
                    buffer.Delete();
                }
                modelBuffers.Clear();
            }
            modelBufferCache.Clear();
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
        public void RotateView(vec2 rot)
        {
            if (!canManipulateCamera || rot.Length == 0) return;

            rot = rot.Normalized;
            yaw_pitch.x += rot.x;
            yaw_pitch.y += rot.y;

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

        private void UpdateMatrices()
        {
            projectionMat = mat4.Perspective(glm.Radians(cameraZoom), resolution.x / resolution.y, 0.1f, 1000.0f);
            viewMat = mat4.LookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
    }
}
