using System;
using GlmSharp;
using System.Collections.Generic;
using System.Linq;
using org.ogre;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects
{
    public class EditableObject : ManualObject
    {
        protected vec3 AmbientColor = new();
        protected vec3 Pos = new();
        protected vec3 Rot = new();
        protected bool Selected;
        protected SceneNode SceneNode;

        private OgreWindow _window;

        public EditableObject(OgreWindow window, string name) : base(name)
        {
            var sceneManager = window.GetSceneManager();
            SceneNode = sceneManager.createSceneNode();
            SceneNode.attachObject(this);
            Selected = false;
            _window = window;
        }

        public SceneNode GetSceneNode()
        {
            return SceneNode;
        }
        
        public mat4 GetTransform()
        {
            var position = OgreExtensions.FromOgre(SceneNode.getPosition());
            var rotation = SceneNode.getOrientation();
            var scale = SceneNode.getScale();
            var angle = new Degree();
            var axes = new Vector3();
            rotation.ToAngleAxis(angle, axes);
            
            var transform = mat4.Translate(position);
            transform *= mat4.Rotate(angle.valueRadians(), OgreExtensions.FromOgre(axes));
            transform *= mat4.Scale(scale.x, scale.y, scale.z);

            return transform;
        }

        public virtual void SetPositionAndRotation()
        {
            SceneNode.setPosition(OgreExtensions.FromGlm(Pos));
            SceneNode.pitch(new Radian(new Degree(Rot.x)));
            SceneNode.yaw(new Radian(new Degree(Rot.y)));
            SceneNode.roll(new Radian(new Degree(Rot.z)));
        }

        public virtual void Select()
        {
            Selected = true;
        }

        public virtual void Deselect()
        {
            Selected = false;
        }

        public void DrawImGui()
        {
            ImGui.Begin(getName());
            ImGui.SetWindowPos(new ImVec2(5, 5));
            ImGui.SetWindowSize(new ImVec2(400, 100));
            DrawImGuiInternal();
            ImGui.End();
        }

        protected virtual void DrawImGuiInternal()
        {
            ImGui.Text($"Position: {Pos}");
            ImGui.Text($"Rotation: {Rot}");
        }
    }
}
