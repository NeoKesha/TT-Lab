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
    public class BaseSceneryViewModel : ObservableObject
    {
        public ObservableCollection<Guid> meshes;
        public ObservableCollection<Guid> lods;
        public ObservableCollection<Vector4ViewModel[]> bbs;
        public ObservableCollection<Vector4ViewModel[]> meshModelMatrices;
        public ObservableCollection<Vector4ViewModel[]> lodModelMatrices;
        public Vector4ViewModel unkVec1;
        public Vector4ViewModel unkVec2;
        public Vector4ViewModel unkVec3;
        public Vector4ViewModel unkVec4;
        public Vector4ViewModel unkVec5;

        public BaseSceneryViewModel(SceneryBaseData data)
        {
            meshes = new ObservableCollection<Guid>();
            foreach (var m in data.MeshIDs)
            {
                meshes.Add(m);
            }
            lods = new ObservableCollection<Guid>();
            foreach (var l in data.LodIDs)
            {
                lods.Add(l);
            }
            bbs = new ObservableCollection<Vector4ViewModel[]>();
            foreach (var bb in data.BoundingBoxes)
            {
                bbs.Add(new Vector4ViewModel[] { new Vector4ViewModel(bb[0]), new Vector4ViewModel(bb[0]) });
            }
            meshModelMatrices = new ObservableCollection<Vector4ViewModel[]>();
            foreach (var mat in data.MeshModelMatrices)
            {
                meshModelMatrices.Add(new Vector4ViewModel[]
                {
                    new Vector4ViewModel(mat[0]),
                    new Vector4ViewModel(mat[1]),
                    new Vector4ViewModel(mat[2]),
                    new Vector4ViewModel(mat[3]),
                });
            }
            lodModelMatrices = new ObservableCollection<Vector4ViewModel[]>();
            foreach (var mat in data.LodModelMatrices)
            {
                lodModelMatrices.Add(new Vector4ViewModel[]
                {
                    new Vector4ViewModel(mat[0]),
                    new Vector4ViewModel(mat[1]),
                    new Vector4ViewModel(mat[2]),
                    new Vector4ViewModel(mat[3]),
                });
            }
            unkVec1 = new Vector4ViewModel(data.UnkVec1);
            unkVec2 = new Vector4ViewModel(data.UnkVec2);
            unkVec3 = new Vector4ViewModel(data.UnkVec3);
            unkVec4 = new Vector4ViewModel(data.UnkVec4);
            unkVec5 = new Vector4ViewModel(data.UnkVec5);
        }
    }
}
