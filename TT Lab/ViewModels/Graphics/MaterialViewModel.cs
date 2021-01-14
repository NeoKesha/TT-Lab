﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Command;

namespace TT_Lab.ViewModels.Graphics
{
    public class MaterialViewModel : AssetViewModel
    {
        private UInt64 _header;
        private UInt32 _dmaChainIndex;
        private String _name;
        private ObservableCollection<LabShaderViewModel> _shaders;

        public MaterialViewModel(Guid asset) : base(asset)
        {
        }

        public MaterialViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var _matData = (MaterialData)_asset.GetData();
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
            AddShaderCommand = new AddItemToListCommand(Shaders, typeof(LabShaderViewModel));
            DeleteShaderCommand = new DeleteItemFromListCommand(Shaders);
            CloneShaderCommand = new CloneItemIntoCollectionCommand<LabShaderViewModel>(Shaders);
        }

        public AddItemToListCommand AddShaderCommand { private set; get; }
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

        private void ShaderViewModel_PropertyChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange("Shaders");
            IsDirty = true;
        }
    }
}
