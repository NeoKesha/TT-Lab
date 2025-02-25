using GlmSharp;
using System;
using System.Drawing;
using org.ogre;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Util;
using TT_Lab.ViewModels.Editors.Instance;
using Color = Twinsanity.TwinsanityInterchange.Common.Color;

namespace TT_Lab.Rendering.Objects
{
    public class Trigger : ManualObject
    {
        private readonly SceneNode _triggerNode;
        private readonly TriggerData _data;
        private Billboard _billboard;

        public Trigger(string name, SceneNode parentNode, SceneManager sceneManager, Billboard billboard, TriggerData data, KnownColor renderColor = KnownColor.DarkOrange) : base(name)
        {
            _billboard = billboard;
            _triggerNode = parentNode.createChildSceneNode();
            var color = System.Drawing.Color.FromKnownColor(renderColor);
            var cubeMesh = BufferGeneration.GetCubeBuffer($"DefaultCube_{color}", System.Drawing.Color.FromArgb((int)(new Color(color.R, color.G, color.B, 64).ToARGB())));
            var entity = sceneManager.createEntity(cubeMesh);
            entity.setMaterial(MaterialManager.GetMaterial("ColorOnlyTransparent"));
            _triggerNode.attachObject(entity);
            
            _billboard.setPosition(-data.Position.X, data.Position.Y, data.Position.Z);
            _billboard.setColour(new ColourValue(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f));
            
            _data = data;
            Update();
        }

        private void Update()
        {
            _triggerNode.setPosition(new Vector3(-_data.Position.X, _data.Position.Y, _data.Position.Z));
            _triggerNode.setScale(new Vector3(_data.Scale.X, _data.Scale.Y, _data.Scale.Z));
            _triggerNode.setOrientation(new Quaternion(_data.Rotation.W, _data.Rotation.X, _data.Rotation.Y, _data.Rotation.Z));
        }
    }
}
