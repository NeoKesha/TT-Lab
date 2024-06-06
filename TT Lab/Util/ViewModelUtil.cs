using Caliburn.Micro;
using System;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using static Twinsanity.TwinsanityInterchange.Common.TwinShader;

namespace TT_Lab.Util
{
    public static class ViewModelUtil
    {
        public readonly static BindableCollection<object> Layers;
        public readonly static BindableCollection<object> ShaderTypes;
        public readonly static BindableCollection<object> AlphaTestMethods;
        public readonly static BindableCollection<object> ProcessAfterFailed;
        public readonly static BindableCollection<object> DestinationAlphaTest;
        public readonly static BindableCollection<object> DepthTestMethods;
        public readonly static BindableCollection<object> ShadingMethods;
        public readonly static BindableCollection<object> TextureCoordinates;
        public readonly static BindableCollection<object> TextureFilters;
        public readonly static BindableCollection<object> ZvalueDrawMasks;
        public readonly static BindableCollection<object> ColorSpecs;
        public readonly static BindableCollection<object> AlphaSpecs;
        public readonly static BindableCollection<object> AlphaBlendingPresets;

        static ViewModelUtil()
        {
            Layers = new BindableCollection<object>(Enum.GetValues(typeof(Enums.Layouts)).Cast<object>());
            ShaderTypes = new BindableCollection<object>(Enum.GetValues(typeof(TwinShader.Type)).Cast<object>());
            AlphaTestMethods = new BindableCollection<object>(Enum.GetValues(typeof(AlphaTestMethod)).Cast<object>());
            ProcessAfterFailed = new BindableCollection<object>(Enum.GetValues(typeof(ProcessAfterAlphaTestFailed)).Cast<object>());
            DestinationAlphaTest = new BindableCollection<object>(Enum.GetValues(typeof(DestinationAlphaTestMode)).Cast<object>());
            DepthTestMethods = new BindableCollection<object>(Enum.GetValues(typeof(DepthTestMethod)).Cast<object>());
            ShadingMethods = new BindableCollection<object>(Enum.GetValues(typeof(ShadingMethod)).Cast<object>());
            TextureCoordinates = new BindableCollection<object>(Enum.GetValues(typeof(TextureCoordinatesSpecification)).Cast<object>());
            TextureFilters = new BindableCollection<object>(Enum.GetValues(typeof(TextureFilter)).Cast<object>());
            ZvalueDrawMasks = new BindableCollection<object>(Enum.GetValues(typeof(ZValueDrawMask)).Cast<object>());
            ColorSpecs = new BindableCollection<object>(Enum.GetValues(typeof(ColorSpecMethod)).Cast<object>());
            AlphaSpecs = new BindableCollection<object>(Enum.GetValues(typeof(AlphaSpecMethod)).Cast<object>());
            AlphaBlendingPresets = new BindableCollection<object>(Enum.GetValues(typeof(AlphaBlendPresets)).Cast<object>());
        }
    }
}
