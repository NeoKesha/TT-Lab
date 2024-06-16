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
using TT_Lab.Rendering;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using static Twinsanity.TwinsanityInterchange.Enumerations.Enums;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class MaterialViewModel : ResourceEditorViewModel
    {
        private AppliedShaders _activatedShaders;
        private UInt32 _dmaChainIndex;
        private String _name;
        private BindableCollection<ShaderViewModel> _shaders;

        public MaterialViewModel()
        {
            Scenes.Add(IoC.Get<SceneEditorViewModel>());

            InitMaterialViewer();
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
                var shaderViewModel = new ShaderViewModel(shader, this);
                _shaders.Add(shaderViewModel);
            }
            AddShaderCommand = new AddItemToListCommand<ShaderViewModel>(Shaders, 5);
            DeleteShaderCommand = new DeleteItemFromListCommand(Shaders);
            CloneShaderCommand = new CloneItemIntoCollectionCommand<ShaderViewModel>(Shaders, 5);
        }

        private void InitMaterialViewer()
        {
            MaterialViewer.SceneCreator = (GLWindow glControl) =>
            {
                glControl.SetRendererLibraries(Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TwinmaterialPass);

                var scene = new Scene(glControl.RenderContext, glControl, (float)glControl.RenderControl.Width, (float)glControl.RenderControl.Height);
                scene.SetCameraSpeed(0);
                scene.DisableCameraManipulation();

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
                TwinMaterialPlane plane = new(glControl.RenderContext, glControl, scene, glControl.Renderer.RenderProgram, textures.ToArray(), Shaders.ToArray(), Shaders.Count);
                scene.AddChild(plane);

                return scene;
            };
        }

        public AddItemToListCommand<ShaderViewModel> AddShaderCommand { private set; get; }
        public DeleteItemFromListCommand DeleteShaderCommand { private set; get; }
        public CloneItemIntoCollectionCommand<ShaderViewModel> CloneShaderCommand { private set; get; }

        public SceneEditorViewModel MaterialViewer
        {
            get => Scenes[0];
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
