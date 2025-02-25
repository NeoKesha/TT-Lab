using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Instance;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.Attributes;
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
            
            base.Save();
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
                DirtyTracker.AddChild(sceneryTree);
                sceneryTree.BuildTree();
            }
        }

        [MarkDirty]
        public String SceneryName
        {
            get => sceneryName;
            set
            {
                if (sceneryName != value)
                {
                    sceneryName = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        [MarkDirty]
        public UInt32 UnkUInt
        {
            get => unkUInt;
            set
            {
                if (value != unkUInt)
                {
                    unkUInt = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        [MarkDirty]
        public Byte UnkByte
        {
            get => unkByte;
            set
            {
                if (unkByte != value)
                {
                    unkByte = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        [MarkDirty]
        public LabURI Skydome
        {
            get => skydome;
            set
            {
                if (value != skydome)
                {
                    skydome = value;
                    NotifyOfPropertyChange();
                }
            }
        }
        public SceneryRootViewModel? SceneryTree
        {
            get => sceneryTree;
        }
    }
}
