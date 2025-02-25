using org.ogre;
using TT_Lab.AssetData.Graphics;

namespace TT_Lab.Rendering.Objects
{
    public class RigidModel : ManualObject
    {
        //ModelBuffer model;

        public RigidModel(string name, RigidModelData rigid) : base(name)
        {
            //model = new ModelBuffer(GL, window, root, rigid);
            //AddChild(model);
        }

        public void Delete()
        {
            //model.Delete();
        }
    }
}
