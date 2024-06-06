using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using static Twinsanity.TwinsanityInterchange.Enumerations.Enums;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class MaterialViewModel : ResourceEditorViewModel, IHandle<RendererInitializedMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SceneEditorViewModel _materialViewer;

        private AppliedShaders _activatedShaders;
        private UInt32 _dmaChainIndex;
        private String _name;
        private BindableCollection<ShaderViewModel> _shaders;

        public MaterialViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _materialViewer = IoC.Get<SceneEditorViewModel>();
        }

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<MaterialData>();
            data.ActivatedShaders = ActivatedShaders;
            data.DmaChainIndex = DmaChainIndex;
            data.Name = Name;
            data.Shaders.Clear();
            for (var i = 0; i < Shaders.Count; ++i)
            {
                var shader = new LabShader(Shaders[i]);
                data.Shaders.Add(shader);
            }
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var _matData = asset.GetData<MaterialData>();
            _activatedShaders = _matData.ActivatedShaders;
            _dmaChainIndex = _matData.DmaChainIndex;
            _name = _matData.Name[..];
            _shaders = new BindableCollection<ShaderViewModel>();
            foreach (var shader in _matData.Shaders)
            {
                var shaderViewModel = new ShaderViewModel(_eventAggregator, shader, this);
                _shaders.Add(shaderViewModel);
            }
            AddShaderCommand = new AddItemToListCommand<ShaderViewModel>(Shaders, 5);
            DeleteShaderCommand = new DeleteItemFromListCommand(Shaders);
            CloneShaderCommand = new CloneItemIntoCollectionCommand<ShaderViewModel>(Shaders, 5);
        }

        public Task HandleAsync(RendererInitializedMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void InitMaterialViewer()
        {
            MaterialViewer.Scene?.Delete();
            MaterialViewer.Scene = new Rendering.Scene((float)MaterialViewer.GlControl.ActualWidth, (float)MaterialViewer.GlControl.ActualWidth, Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TwinmaterialPass);
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();
            List<Bitmap> textures = new();
            for (var i = 0; i < Shaders.Count; ++i)
            {
                var tex = Shaders[i].TexID;
                if (tex == LabURI.Empty)
                {
                    textures.Add(MiscUtils.GetBoatGuy());
                }
                else
                {
                    var texData = AssetManager.Get().GetAssetData<TextureData>(tex);
                    textures.Add(texData.Bitmap);
                }
            }
            TwinMaterialPlane plane = new(MaterialViewer.Scene, MaterialViewer.Scene.Renderer.RenderProgram, textures.ToArray(), Shaders.ToArray(), Shaders.Count);
            MaterialViewer.Scene.AddRender(plane);
        }

        public AddItemToListCommand<ShaderViewModel> AddShaderCommand { private set; get; }
        public DeleteItemFromListCommand DeleteShaderCommand { private set; get; }
        public CloneItemIntoCollectionCommand<ShaderViewModel> CloneShaderCommand { private set; get; }

        public SceneEditorViewModel MaterialViewer
        {
            get => _materialViewer;
        }

        public AppliedShaders ActivatedShaders
        {
            get => _activatedShaders;
            set
            {
                if (value != _activatedShaders)
                {
                    _activatedShaders = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }
        public UInt32 DmaChainIndex
        {
            get => _dmaChainIndex;
            set
            {
                if (value != _dmaChainIndex)
                {
                    _dmaChainIndex = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }
        public String Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<ShaderViewModel> Shaders
        {
            get => _shaders;
        }

        public static BindableCollection<MenuItem> TreeContextMenu { get; private set; }
    }
}
