using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.ViewModels.Instance.Scenery;

namespace TT_Lab.ViewModels.Instance
{
    public class SceneryViewModel : AssetViewModel
    {

        private String sceneryName;
        private UInt32 unkUInt;
        private Byte unkByte;
        private Guid skydome;
        private Boolean lightFlag1;
        private Boolean lightFlag2;
        private Boolean lightFlag3;
        private Boolean lightFlag4;
        private Boolean lightFlag5;
        private Boolean lightFlag6;
        private SceneryRootViewModel? sceneryTree;

        public SceneryViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (SceneryData)_asset.GetData();
            sceneryName = data.SceneryName[..];
            unkUInt = data.UnkUInt;
            unkByte = data.UnkByte;
            skydome = data.SkydomeID;
            lightFlag1 = data.UnkLightFlags[0];
            lightFlag2 = data.UnkLightFlags[1];
            lightFlag3 = data.UnkLightFlags[2];
            lightFlag4 = data.UnkLightFlags[3];
            lightFlag5 = data.UnkLightFlags[4];
            lightFlag6 = data.UnkLightFlags[5];
            if (data.Sceneries.Count != 0)
            {
                sceneryTree = new SceneryRootViewModel(data.Sceneries[0], data.Sceneries.Skip(1).ToList());
                sceneryTree.BuildTree();
            }
        }

        public override void Save(object? o)
        {
            var data = (SceneryData)_asset.GetData();
            data.SceneryName = sceneryName;
            data.UnkUInt = UnkUInt;
            data.UnkByte = UnkByte;
            data.SkydomeID = Skydome;
            data.UnkLightFlags[0] = LightFlag1;
            data.UnkLightFlags[1] = LightFlag2;
            data.UnkLightFlags[2] = LightFlag3;
            data.UnkLightFlags[3] = LightFlag4;
            data.UnkLightFlags[4] = LightFlag5;
            data.UnkLightFlags[5] = LightFlag6;
            data.Sceneries.Clear();
            if (SceneryTree != null)
            {
                var root = new SceneryRootData(SceneryTree);
                data.Sceneries.Add(root);
                IList<SceneryBaseData> list = data.Sceneries;
                SceneryTree.CompileTree(ref list);
                data.Sceneries = (List<SceneryBaseData>)list;
            }
            base.Save(o);
        }

        public String SceneryName { get => sceneryName; set => sceneryName = value; }
        public UInt32 UnkUInt { get => unkUInt; set => unkUInt = value; }
        public Byte UnkByte { get => unkByte; set => unkByte = value; }
        public Guid Skydome { get => skydome; set => skydome = value; }
        public Boolean LightFlag1 { get => lightFlag1; set => lightFlag1 = value; }
        public Boolean LightFlag2 { get => lightFlag2; set => lightFlag2 = value; }
        public Boolean LightFlag3 { get => lightFlag3; set => lightFlag3 = value; }
        public Boolean LightFlag4 { get => lightFlag4; set => lightFlag4 = value; }
        public Boolean LightFlag5 { get => lightFlag5; set => lightFlag5 = value; }
        public Boolean LightFlag6 { get => lightFlag6; set => lightFlag6 = value; }
        public SceneryRootViewModel? SceneryTree
        {
            get => sceneryTree;
        }
    }
}
