using Caliburn.Micro;
using System;
using System.Linq;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Assets;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels.Editors.Instance.Scenery
{
    public class BaseSceneryViewModel : Conductor<IScreen>.Collection.AllActive, IHaveChildrenEditors
    {
        private BindableCollection<PrimitiveWrapperViewModel<LabURI>> meshes;
        private BindableCollection<PrimitiveWrapperViewModel<LabURI>> lods;
        private BindableCollection<BoundingBoxViewModel> bbs;
        private BindableCollection<Matrix4ViewModel> meshModelMatrices;
        private BindableCollection<Matrix4ViewModel> lodModelMatrices;
        private Vector4ViewModel unkVec1;
        private Vector4ViewModel unkVec2;
        private Vector4ViewModel unkVec3;
        private Vector4ViewModel unkVec4;
        private BindableCollection<PrimitiveWrapperViewModel<Boolean>> lightsEnabler;
        private bool isDirty;
        private DirtyTracker dirtyTracker;

        public BaseSceneryViewModel(SceneryBaseData data)
        {
            dirtyTracker = new DirtyTracker(this);
            meshes = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
            dirtyTracker.AddBindableCollection(meshes);
            foreach (var m in data.MeshIDs)
            {
                meshes.Add(new PrimitiveWrapperViewModel<LabURI>(m));
            }
            
            lods = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
            dirtyTracker.AddBindableCollection(lods);
            foreach (var l in data.LodIDs)
            {
                lods.Add(new PrimitiveWrapperViewModel<LabURI>(l));
            }
            
            bbs = new BindableCollection<BoundingBoxViewModel>();
            dirtyTracker.AddBindableCollection(bbs);
            foreach (var bb in data.BoundingBoxes)
            {
                bbs.Add(new BoundingBoxViewModel(bb));
            }
            meshModelMatrices = new BindableCollection<Matrix4ViewModel>();
            dirtyTracker.AddBindableCollection(meshModelMatrices);
            foreach (var mat in data.MeshModelMatrices)
            {
                meshModelMatrices.Add(new Matrix4ViewModel(mat));
            }
            lodModelMatrices = new BindableCollection<Matrix4ViewModel>();
            dirtyTracker.AddBindableCollection(lodModelMatrices);
            foreach (var mat in data.LodModelMatrices)
            {
                lodModelMatrices.Add(new Matrix4ViewModel(mat));
            }
            unkVec1 = new Vector4ViewModel(data.UnkVec1);
            unkVec2 = new Vector4ViewModel(data.UnkVec2);
            unkVec3 = new Vector4ViewModel(data.UnkVec3);
            unkVec4 = new Vector4ViewModel(data.UnkVec4);
            dirtyTracker.AddChild(unkVec1);
            dirtyTracker.AddChild(unkVec2);
            dirtyTracker.AddChild(unkVec3);
            dirtyTracker.AddChild(unkVec4);
            lightsEnabler = new BindableCollection<PrimitiveWrapperViewModel<Boolean>>();
            lightsEnabler.CollectionChanged += (s, e) => { dirtyTracker.MarkDirty(); };
            foreach (var enabler in data.LightsEnabler)
            {
                lightsEnabler.Add(new PrimitiveWrapperViewModel<Boolean>(enabler));
            }
        }
        
        public void ResetDirty()
        {
            DirtyTracker.ResetDirty();
        }

        public BindableCollection<PrimitiveWrapperViewModel<LabURI>> Meshes { get => meshes; private set => meshes = value; }
        public BindableCollection<PrimitiveWrapperViewModel<LabURI>> Lods { get => lods; private set => lods = value; }
        public BindableCollection<BoundingBoxViewModel> Bbs { get => bbs; private set => bbs = value; }
        public BindableCollection<Matrix4ViewModel> MeshModelMatrices { get => meshModelMatrices; private set => meshModelMatrices = value; }
        public BindableCollection<Matrix4ViewModel> LodModelMatrices { get => lodModelMatrices; private set => lodModelMatrices = value; }
        public Vector4ViewModel UnkVec1 { get => unkVec1; set => unkVec1 = value; }
        public Vector4ViewModel UnkVec2 { get => unkVec2; set => unkVec2 = value; }
        public Vector4ViewModel UnkVec3 { get => unkVec3; set => unkVec3 = value; }
        public Vector4ViewModel UnkVec4 { get => unkVec4; set => unkVec4 = value; }
        public BindableCollection<PrimitiveWrapperViewModel<Boolean>> LightsEnabler { get => lightsEnabler; private set => lightsEnabler = value; }

        public bool IsDirty => DirtyTracker.IsDirty;

        public DirtyTracker DirtyTracker => dirtyTracker;
    }
}
