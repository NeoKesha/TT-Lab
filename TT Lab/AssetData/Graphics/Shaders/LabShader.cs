using System;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using TT_Lab.ViewModels.Graphics;
using Twinsanity.TwinsanityInterchange.Common;
using static Twinsanity.TwinsanityInterchange.Common.TwinShader;

namespace TT_Lab.AssetData.Graphics.Shaders
{
    public class LabShader
    {
        public Type ShaderType { get; set; }
        public UInt32 IntParam { get; set; }
        public Single[] FloatParam { get; set; }
        public AlphaBlending ABlending { get; set; }
        public Byte AlphaRegSettingsIndex { get; set; }
        public AlphaTest ATest { get; set; }
        public AlphaTestMethod ATestMethod { get; set; }
        public byte AlphaValueToBeComparedTo { get; set; }
        public ProcessAfterAlphaTestFailed ProcessMethodWhenAlphaTestFailed { get; set; }
        public DestinationAlphaTest DAlphaTest { get; set; }
        public DestinationAlphaTestMode DAlphaTestMode { get; set; }
        public DepthTestMethod DepthTest { get; set; }
        public ShadingMethod ShdMethod { get; set; }
        public TextureMapping TxtMapping { get; set; }
        public TextureCoordinatesSpecification MethodOfSpecifyingTextureCoordinates { get; set; }
        public Fogging Fog { get; set; }
        public Context ContextNum { get; set; }
        public bool UsePresetAlphaRegSettings { get; set; }
        public ColorSpecMethod SpecOfColA { get; set; }
        public ColorSpecMethod SpecOfColB { get; set; }
        public AlphaSpecMethod SpecOfAlphaC { get; set; }
        public ColorSpecMethod SpecOfColD { get; set; }
        public byte FixedAlphaValue { get; set; }
        public TextureFilter TextureFilterWhenTextureIsExpanded { get; set; }
        public bool AlphaCorrectionValue { get; set; }
        public ZValueDrawMask ZValueDrawingMask { get; set; }
        public UInt16 LodParamK { get; set; }
        public UInt16 LodParamL { get; set; }
        public LabURI TextureId { get; set; }
        public byte UnkVal1 { get; set; }
        public byte UnkVal2 { get; set; }
        public byte UnkVal3 { get; set; }
        public bool UnkFlag1 { get; set; }
        public bool UnkFlag2 { get; set; }
        public bool UnkFlag3 { get; set; }
        public Vector4 UnkVector1 { get; set; }
        public Vector4 UnkVector2 { get; set; }
        public Vector4 UnkVector3 { get; set; }
        public TwinBlob Blob { get; set; }

        public LabShader() { }

        public LabShader(LabShaderViewModel vm)
        {
            ShaderType = vm.Type;
            IntParam = vm.IntParam;
            FloatParam = new Single[4];
            for (var j = 0; j < 4; ++j)
            {
                FloatParam[j] = vm.FloatParam[j];
            }
            ABlending = vm.AlphaBlending ? AlphaBlending.ON : AlphaBlending.OFF;
            AlphaRegSettingsIndex = vm.AlphaRegSettingsIndex;
            ATest = vm.AlphaTest ? AlphaTest.ON : AlphaTest.OFF;
            ATestMethod = vm.AlphaTestMethod;
            AlphaValueToBeComparedTo = vm.AlphaValueToCompareTo;
            ProcessMethodWhenAlphaTestFailed = vm.ProcessAfterATestFailed;
            DAlphaTest = vm.DAlphaTest ? DestinationAlphaTest.ON : DestinationAlphaTest.OFF;
            DAlphaTestMode = vm.DAlphaTestMode;
            DepthTest = vm.DepthTest;
            ShdMethod = vm.ShdMethod;
            TxtMapping = vm.TxtMapping ? TextureMapping.ON : TextureMapping.OFF;
            MethodOfSpecifyingTextureCoordinates = vm.TexCoordSpec;
            Fog = vm.Fog ? Fogging.ON : Fogging.OFF;
            ContextNum = vm.CxtNum;
            UsePresetAlphaRegSettings = vm.UsePresetAlphaRegSettings;
            SpecOfColA = vm.SpecOfColA;
            SpecOfColB = vm.SpecOfColB;
            SpecOfAlphaC = vm.SpecOfAlphaC;
            SpecOfColD = vm.SpecOfColD;
            FixedAlphaValue = vm.FixedAlphaValue;
            TextureFilterWhenTextureIsExpanded = vm.TexFilterWhenTextureIsExpanded;
            AlphaCorrectionValue = vm.AlphaCorrectionValue;
            ZValueDrawingMask = vm.ZValueDrawMask;
            LodParamK = vm.LodParamK;
            LodParamL = vm.LodParamL;
            TextureId = vm.TexID;
            UnkVal1 = vm.UnkVal1;
            UnkVal2 = vm.UnkVal2;
            UnkVal3 = vm.UnkVal3;
            UnkFlag1 = vm.UnkFlag1;
            UnkFlag2 = vm.UnkFlag2;
            UnkFlag3 = vm.UnkFlag3;
            UnkVector1 = new Vector4
            {
                X = vm.UnkVec1.X,
                Y = vm.UnkVec1.Y,
                Z = vm.UnkVec1.Z,
                W = vm.UnkVec1.W
            };
            UnkVector2 = new Vector4
            {
                X = vm.UnkVec2.X,
                Y = vm.UnkVec2.Y,
                Z = vm.UnkVec2.Z,
                W = vm.UnkVec2.W,
            };
            UnkVector3 = new Vector4
            {
                X = vm.UnkVec3.X,
                Y = vm.UnkVec3.Y,
                Z = vm.UnkVec3.Z,
                W = vm.UnkVec3.W,
            };
        }

        public LabShader(String package, String subpackage, String? variant, TwinShader twinShader)
        {
            ShaderType = (Type)twinShader.ShaderType;
            IntParam = twinShader.IntParam;
            FloatParam = twinShader.FloatParam;
            ABlending = twinShader.ABlending;
            AlphaRegSettingsIndex = twinShader.AlphaRegSettingsIndex;
            ATest = twinShader.ATest;
            ATestMethod = twinShader.ATestMethod;
            AlphaValueToBeComparedTo = twinShader.AlphaValueToBeComparedTo;
            ProcessMethodWhenAlphaTestFailed = twinShader.ProcessMethodWhenAlphaTestFailed;
            DAlphaTest = twinShader.DAlphaTest;
            DAlphaTestMode = twinShader.DAlphaTestMode;
            DepthTest = twinShader.DepthTest;
            ShdMethod = twinShader.ShdMethod;
            TxtMapping = twinShader.TxtMapping;
            MethodOfSpecifyingTextureCoordinates = twinShader.MethodOfSpecifyingTextureCoordinates;
            Fog = twinShader.Fog;
            ContextNum = twinShader.ContextNum;
            UsePresetAlphaRegSettings = twinShader.UsePresetAlphaRegSettings;
            SpecOfColA = twinShader.SpecOfColA;
            SpecOfColB = twinShader.SpecOfColB;
            SpecOfAlphaC = twinShader.SpecOfAlphaC;
            SpecOfColD = twinShader.SpecOfColD;
            FixedAlphaValue = twinShader.FixedAlphaValue;
            TextureFilterWhenTextureIsExpanded = twinShader.TextureFilterWhenTextureIsExpanded;
            AlphaCorrectionValue = twinShader.AlphaCorrectionValue;
            ZValueDrawingMask = twinShader.ZValueDrawingMask;
            LodParamK = twinShader.LodParamK;
            LodParamL = twinShader.LodParamL;
            TextureId = (twinShader.TextureId == 0) ? LabURI.Empty : AssetManager.Get().GetUri(package, subpackage, typeof(Texture).Name, variant, twinShader.TextureId);
            UnkVal1 = twinShader.UnkVal1;
            UnkVal2 = twinShader.UnkVal2;
            UnkVal3 = twinShader.UnkVal3;
            UnkFlag1 = twinShader.UnkFlag1;
            UnkFlag2 = twinShader.UnkFlag2;
            UnkFlag3 = twinShader.UnkFlag3;
            UnkVector1 = CloneUtils.Clone(twinShader.UnkVector1);
            UnkVector2 = CloneUtils.Clone(twinShader.UnkVector2);
            UnkVector3 = CloneUtils.Clone(twinShader.UnkVector3);
            Blob = CloneUtils.DeepClone(twinShader.Blob);
        }

        public enum Type
        {
            SHADER_1 = 1,
            SHADER_2 = 2,
            SHADER_4 = 4,
            SHADER_10 = 10,
            SHADER_11 = 11,
            SHADER_12 = 12,
            SHADER_13 = 13,
            SHADER_15 = 15,
            SHADER_16 = 16,
            SHADER_17 = 17,
            SHADER_18 = 18,
            SHADER_19 = 19,
            SHADER_20 = 20,
            SHADER_21 = 21,
            SHADER_22 = 22,
            SHADER_23 = 23,
            SHADER_25 = 25,
            SHADER_26 = 26,
            SHADER_27 = 27,
            SHADER_30 = 30,
            SHADER_31 = 31,
            SHADER_32 = 32,
        }
    }
}
