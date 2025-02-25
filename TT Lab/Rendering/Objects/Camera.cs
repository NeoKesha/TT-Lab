using System.Drawing;
using org.ogre;
using TT_Lab.AssetData.Instance;

namespace TT_Lab.Rendering.Objects;

public class Camera : ManualObject
{
    public Camera(string name, SceneNode parentNode, SceneManager sceneManager, Billboard cameraBillboard, CameraData cameraData) : base(name)
    {
        var cameraNode = parentNode.createChildSceneNode();
        cameraNode.attachObject(new Trigger($"CameraTrigger_{name}", cameraNode, sceneManager, cameraBillboard, cameraData.Trigger, KnownColor.Blue));
    }
}