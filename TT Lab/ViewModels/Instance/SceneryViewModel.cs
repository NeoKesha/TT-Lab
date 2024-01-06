using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Instance;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Instance.Scenery;

namespace TT_Lab.ViewModels.Instance
{
    public class SceneryViewModel : AssetViewModel
    {

        private String sceneryName;
        private UInt32 unkUInt;
        private Byte unkByte;
        private LabURI skydome;
        private SceneryRootViewModel? sceneryTree;

        public SceneryViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            var data = _asset.GetData<SceneryData>();
            sceneryName = data.ChunkPath[..];
            unkUInt = data.FogColor;
            unkByte = data.UnkByte;
            skydome = data.SkydomeID;
            if (data.Sceneries.Count != 0)
            {
                sceneryTree = new SceneryRootViewModel(data.Sceneries[0], data.Sceneries.Skip(1).ToList());
                sceneryTree.BuildTree();
            }
        }

        public override void Save(object? o)
        {
            var data = _asset.GetData<SceneryData>();
            data.ChunkPath = sceneryName;
            data.FogColor = UnkUInt;
            data.UnkByte = UnkByte;
            data.SkydomeID = Skydome;
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
        public LabURI Skydome { get => skydome; set => skydome = value; }
        public SceneryRootViewModel? SceneryTree
        {
            get => sceneryTree;
        }
    }
}
