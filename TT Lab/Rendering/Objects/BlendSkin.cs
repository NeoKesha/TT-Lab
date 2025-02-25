using org.ogre;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Rendering.Objects
{
    public class BlendSkin
    {
        private readonly ModelBufferBlendSkin _model;
        
        public BlendSkin(SceneManager sceneManager, string name, BlendSkinData blendSkin)
        {
            _model = new ModelBufferBlendSkin(sceneManager, name, blendSkin);
        }

        public void SetBlendShapeValue(int index, float value)
        {
            _model.SetBlendShapeWeight(index, value);
        }
    }
}
