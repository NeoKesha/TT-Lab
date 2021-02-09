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
    public class BaseSceneryViewModel : SavebleViewModel
    {
        private ObservableCollection<Guid> meshes;
        private ObservableCollection<Guid> lods;
        private ObservableCollection<Vector4ViewModel[]> bbs;
        private ObservableCollection<Vector4ViewModel[]> meshModelMatrices;
        private ObservableCollection<Vector4ViewModel[]> lodModelMatrices;
        private Vector4ViewModel unkVec1;
        private Vector4ViewModel unkVec2;
        private Vector4ViewModel unkVec3;
        private Vector4ViewModel unkVec4;
        private Vector4ViewModel unkVec5;

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

        public override void Save(object? o)
        {
            var baseD = (SceneryBaseData)o!;

        }

        public ObservableCollection<Guid> Meshes { get => meshes; set => meshes = value; }
        public ObservableCollection<Guid> Lods { get => lods; set => lods = value; }
        public ObservableCollection<Vector4ViewModel[]> Bbs { get => bbs; set => bbs = value; }
        public ObservableCollection<Vector4ViewModel[]> MeshModelMatrices { get => meshModelMatrices; set => meshModelMatrices = value; }
        public ObservableCollection<Vector4ViewModel[]> LodModelMatrices { get => lodModelMatrices; set => lodModelMatrices = value; }
        public Vector4ViewModel UnkVec1 { get => unkVec1; set => unkVec1 = value; }
        public Vector4ViewModel UnkVec2 { get => unkVec2; set => unkVec2 = value; }
        public Vector4ViewModel UnkVec3 { get => unkVec3; set => unkVec3 = value; }
        public Vector4ViewModel UnkVec4 { get => unkVec4; set => unkVec4 = value; }
        public Vector4ViewModel UnkVec5 { get => unkVec5; set => unkVec5 = value; }
    }
}
