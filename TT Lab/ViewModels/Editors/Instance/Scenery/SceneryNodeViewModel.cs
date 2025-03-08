using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.ViewModels.Editors.Instance.Scenery
{
    public class SceneryNodeViewModel : BaseSceneryViewModel
    {
        private BindableCollection<PrimitiveWrapperViewModel<ITwinScenery.SceneryType>> sceneryTypes;
        private BindableCollection<BaseSceneryViewModel> children;

        public SceneryNodeViewModel(SceneryBaseData data) : base(data)
        {
            var nodeData = (SceneryNodeData)data;
            sceneryTypes = new BindableCollection<PrimitiveWrapperViewModel<ITwinScenery.SceneryType>>();
            DirtyTracker.AddBindableCollection(sceneryTypes);
            foreach (var sceneryType in nodeData.SceneryTypes)
            {
                sceneryTypes.Add(new PrimitiveWrapperViewModel<ITwinScenery.SceneryType>(sceneryType));
            }
            children = new BindableCollection<BaseSceneryViewModel>();
            DirtyTracker.AddBindableCollection(children);
        }

        public void BuildTree(ref IList<SceneryBaseData> sceneryTree)
        {
            foreach (var sc in sceneryTypes)
            {
                if (sc.Value == ITwinScenery.SceneryType.Node)
                {
                    var node = sceneryTree[0];
                    sceneryTree = sceneryTree.Skip(1).ToList();
                    var newNode = new SceneryNodeViewModel(node);
                    children.Add(newNode);
                    newNode.BuildTree(ref sceneryTree);
                }
                else if (sc.Value == ITwinScenery.SceneryType.Leaf)
                {
                    var leaf = sceneryTree[0];
                    sceneryTree = sceneryTree.Skip(1).ToList();
                    children.Add(new SceneryLeafViewModel(leaf));
                }
            }
            ResetDirty();
        }

        public void CompileTree(ref IList<SceneryBaseData> sceneryTree)
        {
            foreach (var child in Children)
            {
                if (child is SceneryNodeViewModel model)
                {
                    var nodeData = new SceneryNodeData(model);
                    sceneryTree.Add(nodeData);
                    model.CompileTree(ref sceneryTree);
                }
                else
                {
                    var leafData = new SceneryLeafData(child);
                    sceneryTree.Add(leafData);
                }
            }
        }

        public BindableCollection<PrimitiveWrapperViewModel<ITwinScenery.SceneryType>> SceneryTypes
        {
            get => sceneryTypes;
        }

        public BindableCollection<BaseSceneryViewModel> Children
        {
            get => children;
        }
    }
}
