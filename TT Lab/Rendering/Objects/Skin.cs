using org.ogre;
using TT_Lab.AssetData.Graphics;

namespace TT_Lab.Rendering.Objects
{
    public class Skin : ManualObject
    {
        //ModelBuffer model;

        public Skin(string name, SkinData skin) : base(name)
        {
            //model = new ModelBuffer(GL, window, root, skin);
            //AddChild(model);
        }

        public void Delete()
        {
            //model.Delete();
        }
    }
}
