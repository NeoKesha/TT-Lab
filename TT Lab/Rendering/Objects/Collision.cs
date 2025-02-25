using GlmSharp;
using org.ogre;
using System;
using System.Drawing.Drawing2D;
using System.Linq;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets.Instance;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Collision : ManualObject
    {
        public Collision(string name, SceneManager sceneManager, CollisionData colData) : base(name)
        {
            var collisionBuffer = BufferGeneration.GetModelBuffer(colData.GetHashCode().ToString(),
                colData.Vectors.Select(v => new vec3(v.X, v.Y, v.Z)).ToList(),
                colData.Triangles.Select(t => t.Face).ToList(),
                CollisionSurface.DefaultColors.ToList().Select(c => System.Drawing.Color.FromArgb((int)c.ToARGB())).ToList(),
                RenderOperation.OperationType.OT_TRIANGLE_LIST,
                null,
                null,
                (colors, i) => colors[colData.Triangles[i].SurfaceIndex].ToArray());

            var collisionNode = sceneManager.getRootSceneNode().createChildSceneNode();
            var entity = sceneManager.createEntity(collisionBuffer);
            entity.setMaterial(MaterialManager.GetMaterial("ColorOnly"));
            collisionNode.attachObject(entity);
            collisionNode.attachObject(this);
        }
    }
}
