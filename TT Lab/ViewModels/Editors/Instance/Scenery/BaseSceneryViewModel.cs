using Caliburn.Micro;
using System;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Composite;

namespace TT_Lab.ViewModels.Editors.Instance.Scenery
{
    public class BaseSceneryViewModel : Conductor<IScreen>.Collection.AllActive
    {
        private BindableCollection<LabURI> meshes;
        private BindableCollection<LabURI> lods;
        private BindableCollection<Vector4ViewModel[]> bbs;
        private BindableCollection<Vector4ViewModel[]> meshModelMatrices;
        private BindableCollection<Vector4ViewModel[]> lodModelMatrices;
        private Vector4ViewModel unkVec1;
        private Vector4ViewModel unkVec2;
        private Vector4ViewModel unkVec3;
        private Vector4ViewModel unkVec4;
        private BindableCollection<Boolean> lightsEnabler;

        public BaseSceneryViewModel(SceneryBaseData data)
        {
            meshes = new BindableCollection<LabURI>();
            foreach (var m in data.MeshIDs)
            {
                meshes.Add(m);
            }
            lods = new BindableCollection<LabURI>();
            foreach (var l in data.LodIDs)
            {
                lods.Add(l);
            }
            bbs = new BindableCollection<Vector4ViewModel[]>();
            foreach (var bb in data.BoundingBoxes)
            {
                bbs.Add(new Vector4ViewModel[] { new(bb[0]), new(bb[0]) });
            }
            meshModelMatrices = new BindableCollection<Vector4ViewModel[]>();
            foreach (var mat in data.MeshModelMatrices)
            {
                meshModelMatrices.Add(new Vector4ViewModel[]
                {
                    new(mat[0]),
                    new(mat[1]),
                    new(mat[2]),
                    new(mat[3]),
                });
            }
            lodModelMatrices = new BindableCollection<Vector4ViewModel[]>();
            foreach (var mat in data.LodModelMatrices)
            {
                lodModelMatrices.Add(new Vector4ViewModel[]
                {
                    new(mat[0]),
                    new(mat[1]),
                    new(mat[2]),
                    new(mat[3]),
                });
            }
            unkVec1 = new Vector4ViewModel(data.UnkVec1);
            unkVec2 = new Vector4ViewModel(data.UnkVec2);
            unkVec3 = new Vector4ViewModel(data.UnkVec3);
            unkVec4 = new Vector4ViewModel(data.UnkVec4);
            lightsEnabler = new BindableCollection<Boolean>();
            foreach (var enabler in data.LightsEnabler)
            {
                lightsEnabler.Add(enabler);
            }
        }

        public BindableCollection<LabURI> Meshes { get => meshes; set => meshes = value; }
        public BindableCollection<LabURI> Lods { get => lods; set => lods = value; }
        public BindableCollection<Vector4ViewModel[]> Bbs { get => bbs; set => bbs = value; }
        public BindableCollection<Vector4ViewModel[]> MeshModelMatrices { get => meshModelMatrices; set => meshModelMatrices = value; }
        public BindableCollection<Vector4ViewModel[]> LodModelMatrices { get => lodModelMatrices; set => lodModelMatrices = value; }
        public Vector4ViewModel UnkVec1 { get => unkVec1; set => unkVec1 = value; }
        public Vector4ViewModel UnkVec2 { get => unkVec2; set => unkVec2 = value; }
        public Vector4ViewModel UnkVec3 { get => unkVec3; set => unkVec3 = value; }
        public Vector4ViewModel UnkVec4 { get => unkVec4; set => unkVec4 = value; }
        public BindableCollection<Boolean> LightsEnabler { get => lightsEnabler; set => lightsEnabler = value; }
    }
}
