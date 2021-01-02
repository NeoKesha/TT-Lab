using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using static Twinsanity.TwinsanityInterchange.Common.TwinShader;

namespace TT_Lab.AssetData.Graphics.Shaders
{
    public class LabShader
    {
        public UInt32 ShaderType { get; set; }
        public UInt32 IntParam { get; set; }
        public Single[] FloatParam { get; set; }
        public AlphaBlending ABlending { get; set; }
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
        public Guid TextureId { get; set; }
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

        public LabShader(TwinShader twinShader)
        {
            ShaderType = twinShader.ShaderType;
            IntParam = twinShader.IntParam;
            FloatParam = twinShader.FloatParam;
            ABlending = twinShader.ABlending;
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
            TextureId = (twinShader.TextureId == 0)?Guid.Empty:GuidManager.GetGuidByTwinId(twinShader.TextureId, typeof(Texture));
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

    }
}
