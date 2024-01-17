using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Editors.Graphics;
using static Twinsanity.TwinsanityInterchange.Common.TwinShader;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Graphics
{
    public class MaterialShaderViewModel : ObservableObject
    {
        private LabShaderViewModel viewModel;
        private MaterialEditor materialEditor;
        private ObservableCollection<object> shaderTypes;
        private ObservableCollection<object> alphaTestMethods;
        private ObservableCollection<object> processAfterFailed;
        private ObservableCollection<object> destinationAlphaTest;
        private ObservableCollection<object> depthTestMethods;
        private ObservableCollection<object> shadingMethods;
        private ObservableCollection<object> textureCoordinates;
        private ObservableCollection<object> textureFilters;
        private ObservableCollection<object> zvalueDrawMasks;
        private ObservableCollection<object> colorSpecs;
        private ObservableCollection<object> alphaSpecs;
        private ObservableCollection<object> alphaBlendingPresets;

        public MaterialShaderViewModel(LabShaderViewModel shaderViewModel, MaterialEditor materialEditor)
        {
            viewModel = shaderViewModel;
            this.materialEditor = materialEditor;
            shaderTypes = new ObservableCollection<object>(Enum.GetValues(typeof(TwinShader.Type)).Cast<object>());
            alphaTestMethods = new ObservableCollection<object>(Enum.GetValues(typeof(AlphaTestMethod)).Cast<object>());
            processAfterFailed = new ObservableCollection<object>(Enum.GetValues(typeof(ProcessAfterAlphaTestFailed)).Cast<object>());
            destinationAlphaTest = new ObservableCollection<object>(Enum.GetValues(typeof(DestinationAlphaTestMode)).Cast<object>());
            depthTestMethods = new ObservableCollection<object>(Enum.GetValues(typeof(DepthTestMethod)).Cast<object>());
            shadingMethods = new ObservableCollection<object>(Enum.GetValues(typeof(ShadingMethod)).Cast<object>());
            textureCoordinates = new ObservableCollection<object>(Enum.GetValues(typeof(TextureCoordinatesSpecification)).Cast<object>());
            textureFilters = new ObservableCollection<object>(Enum.GetValues(typeof(TextureFilter)).Cast<object>());
            zvalueDrawMasks = new ObservableCollection<object>(Enum.GetValues(typeof(ZValueDrawMask)).Cast<object>());
            colorSpecs = new ObservableCollection<object>(Enum.GetValues(typeof(ColorSpecMethod)).Cast<object>());
            alphaSpecs = new ObservableCollection<object>(Enum.GetValues(typeof(AlphaSpecMethod)).Cast<object>());
            alphaBlendingPresets = new ObservableCollection<object>(Enum.GetValues(typeof(AlphaBlendPresets)).Cast<object>());
        }

        public LabShaderViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                NotifyChange();
            }
        }
        public MaterialEditor MaterialEditor
        {
            get => materialEditor;
            set
            {
                materialEditor = value;
                NotifyChange();
            }
        }
        public ObservableCollection<object> ShaderTypes
        {
            get => shaderTypes;
            private set
            {
                shaderTypes = value;
                NotifyChange();
            }
        }
        public ObservableCollection<object> AlphaTestMethods
        {
            get => alphaTestMethods;
            private set
            { 
                alphaTestMethods = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> ProcessAfterFailed
        {
            get => processAfterFailed;
            private set
            {
                processAfterFailed = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> DestinationAlphaTest
        {
            get => destinationAlphaTest;
            private set
            {
                destinationAlphaTest = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> DepthTestMethods
        {
            get => depthTestMethods;
            private set
            {
                depthTestMethods = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> ShadingMethods
        {
            get => shadingMethods;
            private set
            {
                shadingMethods = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> TextureCoordinates
        {
            get => textureCoordinates;
            private set
            {
                textureCoordinates = value;
                NotifyChange();
            }
        }
        public ObservableCollection<object> TextureFilters
        {
            get => textureFilters;
            private set
            {
                textureFilters = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> ZvalueDrawMasks
        {
            get => zvalueDrawMasks;
            private set
            {
                zvalueDrawMasks = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> ColorSpecs
        {
            get => colorSpecs;
            private set
            {
                colorSpecs = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> AlphaSpecs
        {
            get => alphaSpecs;
            private set
            {
                alphaSpecs = value;
                NotifyChange();
            }

        }
        public ObservableCollection<object> AlphaBlendingPresets
        {
            get => alphaBlendingPresets;
            private set
            {
                alphaBlendingPresets = value;
                NotifyChange();
            }

        }
    }
}
