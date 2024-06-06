using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Instance;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Editors.Instance.Scenery;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class SceneryViewModel : InstanceSectionResourceEditorViewModel
    {

        private String sceneryName = "new_scenery";
        private UInt32 unkUInt;
        private Byte unkByte;
        private LabURI skydome = LabURI.Empty;
        private SceneryRootViewModel? sceneryTree;

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<SceneryData>();
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
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<SceneryData>();
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
