using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Util;

namespace TT_Lab.ViewModels.Instance.Scenery
{
    public class SceneryNodeViewModel : BaseSceneryViewModel
    {
        private Int32[] sceneryTypes;
        private ObservableCollection<BaseSceneryViewModel> children;

        public SceneryNodeViewModel(SceneryBaseData data) : base(data)
        {
            var nodeData = (SceneryNodeData)data;
            sceneryTypes = CloneUtils.CloneArray(nodeData.SceneryTypes);
            children = new ObservableCollection<BaseSceneryViewModel>();
        }

        public void BuildTree(ref IList<SceneryBaseData> sceneryTree)
        {
            foreach (var sc in sceneryTypes)
            {
                if (sc == 0x1600)
                {
                    var node = sceneryTree[0];
                    sceneryTree = sceneryTree.Skip(1).ToList();
                    var newNode = new SceneryNodeViewModel(node);
                    children.Add(newNode);
                    newNode.BuildTree(ref sceneryTree);
                }
                else if (sc == 0x1605)
                {
                    var leaf = sceneryTree[0];
                    sceneryTree = sceneryTree.Skip(1).ToList();
                    children.Add(new SceneryLeafViewModel(leaf));
                }
            }
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

        public Int32[] SceneryTypes
        {
            get => sceneryTypes;
        }
        public ObservableCollection<BaseSceneryViewModel> Children
        {
            get => children;
        }
    }
}
