using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Assets;
using TT_Lab.Command;

namespace TT_Lab.ViewModels.Graphics
{
    public class MaterialViewModel : AssetViewModel
    {
        public event EventHandler<System.ComponentModel.PropertyChangedEventArgs>? PropChanged;

        private UInt64 _header;
        private UInt32 _dmaChainIndex;
        private String _name;
        private ObservableCollection<LabShaderViewModel> _shaders;

        public MaterialViewModel(LabURI asset) : base(asset)
        {
        }

        public MaterialViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            var _matData = _asset.GetData<MaterialData>();
            _header = _matData.Header;
            _dmaChainIndex = _matData.DmaChainIndex;
            _name = _matData.Name[..];
            _shaders = new ObservableCollection<LabShaderViewModel>();
            foreach (var shader in _matData.Shaders)
            {
                var shaderViewModel = new LabShaderViewModel(shader);
                _shaders.Add(shaderViewModel);
                shaderViewModel.PropertyChanged += ShaderViewModel_PropertyChanged;
            }
            AddShaderCommand = new AddItemToListCommand<LabShaderViewModel>(Shaders, 5);
            DeleteShaderCommand = new DeleteItemFromListCommand(Shaders);
            CloneShaderCommand = new CloneItemIntoCollectionCommand<LabShaderViewModel>(Shaders, 5);
            Shaders.CollectionChanged += Shaders_Changed;
        }

        public override void Save(object? o)
        {
            var data = Asset.GetData<MaterialData>();
            data.Header = Header;
            data.DmaChainIndex = DmaChainIndex;
            data.Name = Name;
            data.Shaders.Clear();
            for (var i = 0; i < Shaders.Count; ++i)
            {
                var shader = new LabShader(Shaders[i]);
                data.Shaders.Add(shader);
            }
            base.Save(o);
        }

        public AddItemToListCommand<LabShaderViewModel> AddShaderCommand { private set; get; }
        public DeleteItemFromListCommand DeleteShaderCommand { private set; get; }
        public CloneItemIntoCollectionCommand<LabShaderViewModel> CloneShaderCommand { private set; get; }
        public UInt64 Header
        {
            get => _header;
            set
            {
                if (value != _header)
                {
                    _header = value;
                    IsDirty = true;
                    NotifyChange();
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
                    NotifyChange();
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
                    NotifyChange();
                }
            }
        }

        public ObservableCollection<LabShaderViewModel> Shaders
        {
            get
            {
                return _shaders;
            }
            private set
            {
                _shaders = value;
                NotifyChange();
            }
        }

        public static ObservableCollection<MenuItem> TreeContextMenu { get; private set; }

        private void Shaders_Changed(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var addedShader = Shaders[e.NewStartingIndex];
                addedShader.PropertyChanged += ShaderViewModel_PropertyChanged;
            }
            PropChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Shaders)));
        }

        private void ShaderViewModel_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(e.PropertyName!);
            IsDirty = true;
            PropChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(e.PropertyName));
        }
    }
}
