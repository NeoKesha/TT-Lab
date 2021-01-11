using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics.Shaders;
using static Twinsanity.TwinsanityInterchange.Common.TwinShader;

namespace TT_Lab.ViewModels.Graphics
{
    public class LabShaderViewModel : ObservableObject
    {
        private String _name = "Shader";
        private UInt32 _type;
        private UInt32 _intParam;
        private Single[] _floatParam;
        private AlphaBlending _alphaBlending;
        private AlphaTest _alphaTest;
        private AlphaTestMethod _alphaTestMethod;
        private Byte _alphaValueToCompareTo;
        private ProcessAfterAlphaTestFailed _processAfterATestFailed;
        private DestinationAlphaTest _dAlphaTest;
        private DestinationAlphaTestMode _dAlphaTestMode;
        private DepthTestMethod _depthTest;
        private ShadingMethod _shdMethod;
        private TextureMapping _txtMapping;
        private TextureCoordinatesSpecification _texCoordSpec;
        private Fogging _fog;
        private Context _cxtNum;
        private Boolean _usePresetAlphaRegSettings;
        private ColorSpecMethod _specOfColA;
        private ColorSpecMethod _specOfColB;
        private AlphaSpecMethod _specOfAlphaC;
        private ColorSpecMethod _specOfColD;
        private Byte _fixedAlphaValue;
        private TextureFilter _texFilterWhenTextureIsExpanded;
        private Boolean _alphaCorrectionValue;
        private ZValueDrawMask _zValueDrawMask;
        private UInt16 _lodParamK;
        private UInt16 _lodParamL;
        private Guid _texID;
        private Byte _unkVal1;
        private Byte _unkVal2;
        private Byte _unkVal3;
        private Boolean _unkFlag1;
        private Boolean _unkFlag2;
        private Boolean _unkFlag3;
        private Vector4ViewModel _unkVec1;
        private Vector4ViewModel _unkVec2;
        private Vector4ViewModel _unkVec3;

        public LabShaderViewModel(LabShader shader)
        {
            _type = shader.ShaderType;
            _intParam = shader.IntParam;
            _floatParam = new Single[shader.FloatParam.Length];
            for (var i = 0; i < _floatParam.Length; ++i)
            {
                _floatParam[i] = shader.FloatParam[i];
            }
            _alphaBlending = shader.ABlending;
            _alphaTest = shader.ATest;
            _alphaTestMethod = shader.ATestMethod;
            _alphaValueToCompareTo = shader.AlphaValueToBeComparedTo;
            _processAfterATestFailed = shader.ProcessMethodWhenAlphaTestFailed;
            _dAlphaTest = shader.DAlphaTest;
            _dAlphaTestMode = shader.DAlphaTestMode;
            _depthTest = shader.DepthTest;
            _shdMethod = shader.ShdMethod;
            _txtMapping = shader.TxtMapping;
            _texCoordSpec = shader.MethodOfSpecifyingTextureCoordinates;
            _fog = shader.Fog;
            _cxtNum = shader.ContextNum;
            _usePresetAlphaRegSettings = shader.UsePresetAlphaRegSettings;
            _specOfColA = shader.SpecOfColA;
            _specOfColB = shader.SpecOfColB;
            _specOfAlphaC = shader.SpecOfAlphaC;
            _specOfColD = shader.SpecOfColD;
            _fixedAlphaValue = shader.FixedAlphaValue;
            _texFilterWhenTextureIsExpanded = shader.TextureFilterWhenTextureIsExpanded;
            _alphaCorrectionValue = shader.AlphaCorrectionValue;
            _zValueDrawMask = shader.ZValueDrawingMask;
            _lodParamK = shader.LodParamK;
            _lodParamL = shader.LodParamL;
            _texID = shader.TextureId;
            _unkVal1 = shader.UnkVal1;
            _unkVal2 = shader.UnkVal2;
            _unkVal3 = shader.UnkVal3;
            _unkFlag1 = shader.UnkFlag1;
            _unkFlag2 = shader.UnkFlag2;
            _unkFlag3 = shader.UnkFlag3;
            _unkVec1 = new Vector4ViewModel(shader.UnkVector1);
            _unkVec2 = new Vector4ViewModel(shader.UnkVector2);
            _unkVec3 = new Vector4ViewModel(shader.UnkVector3);
            Name = $"Shader {_type}";
        }
        public String Name
        {
            get => _name;
            private set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyChange();
                }
            }
        }
        public UInt32 Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    NotifyChange();
                }
            }
        }
        public UInt32 IntParam
        {
            get => _intParam;
            set
            {
                if (value != _intParam)
                {
                    _intParam = value;
                    NotifyChange();
                }
            }
        }
        public Single[] FloatParam { get => _floatParam; private set => _floatParam = value; }
        public AlphaBlending AlphaBlending
        {
            get => _alphaBlending;
            set
            {
                if (_alphaBlending != value)
                {
                    _alphaBlending = value;
                    NotifyChange();
                }
            }
        }
        public AlphaTest AlphaTest
        {
            get => _alphaTest;
            set
            {
                if (_alphaTest != value)
                {
                    _alphaTest = value;
                    NotifyChange();
                }
            }
        }
        public AlphaTestMethod AlphaTestMethod
        {
            get => _alphaTestMethod;
            set
            {
                if (_alphaTestMethod != value)
                {
                    _alphaTestMethod = value;
                    NotifyChange();
                }
            }
        }
        public Byte AlphaValueToCompareTo
        {
            get => _alphaValueToCompareTo;
            set
            {
                if (_alphaValueToCompareTo != value)
                {
                    _alphaValueToCompareTo = value;
                    NotifyChange();
                }
            }
        }
        public ProcessAfterAlphaTestFailed ProcessAfterATestFailed
        {
            get => _processAfterATestFailed;
            set
            {
                if (_processAfterATestFailed != value)
                {
                    _processAfterATestFailed = value;
                    NotifyChange();
                }
            }
        }
        public DestinationAlphaTest DAlphaTest
        {
            get => _dAlphaTest;
            set
            {
                if (_dAlphaTest != value)
                {
                    _dAlphaTest = value;
                    NotifyChange();
                }
            }
        }
        public DestinationAlphaTestMode DAlphaTestMode
        {
            get => _dAlphaTestMode;
            set
            {
                if (_dAlphaTestMode != value)
                {
                    _dAlphaTestMode = value;
                    NotifyChange();
                }
            }
        }
        public DepthTestMethod DepthTest
        {
            get => _depthTest;
            set
            {
                if (_depthTest != value)
                {
                    _depthTest = value;
                    NotifyChange();
                }
            }
        }
        public ShadingMethod ShdMethod
        {
            get => _shdMethod;
            set
            {
                if (_shdMethod != value)
                {
                    _shdMethod = value;
                    NotifyChange();
                }
            }
        }
        public TextureMapping TxtMapping
        {
            get => _txtMapping;
            set
            {
                if (_txtMapping != value)
                {
                    _txtMapping = value;
                    NotifyChange();
                }
            }
        }
        public TextureCoordinatesSpecification TexCoordSpec
        {
            get => _texCoordSpec;
            set
            {
                if (_texCoordSpec != value)
                {
                    _texCoordSpec = value;
                    NotifyChange();
                }
            }
        }
        public Fogging Fog
        {
            get => _fog;
            set
            {
                if (_fog != value)
                {
                    _fog = value;
                    NotifyChange();
                }
            }
        }
        public Context CxtNum
        {
            get => _cxtNum;
            set
            {
                if (_cxtNum != value)
                {
                    _cxtNum = value;
                    NotifyChange();
                }
            }
        }
        public Boolean UsePresetAlphaRegSettings
        {
            get => _usePresetAlphaRegSettings;
            set
            {
                if (_usePresetAlphaRegSettings != value)
                {
                    _usePresetAlphaRegSettings = value;
                    NotifyChange();
                }
            }
        }
        public ColorSpecMethod SpecOfColA
        {
            get => _specOfColA;
            set
            {
                if (_specOfColA != value)
                {
                    _specOfColA = value;
                    NotifyChange();
                }
            }
        }
        public ColorSpecMethod SpecOfColB
        {
            get => _specOfColB;
            set
            {
                if (_specOfColB != value)
                {
                    _specOfColB = value;
                    NotifyChange();
                }
            }
        }
        public AlphaSpecMethod SpecOfAlphaC
        {
            get => _specOfAlphaC;
            set
            {
                if (_specOfAlphaC != value)
                {
                    _specOfAlphaC = value;
                    NotifyChange();
                }
            }
        }
        public ColorSpecMethod SpecOfColD
        {
            get => _specOfColD;
            set
            {
                if (_specOfColD != value)
                {
                    _specOfColD = value;
                    NotifyChange();
                }
            }
        }
        public Byte FixedAlphaValue
        {
            get => _fixedAlphaValue;
            set
            {
                if (_fixedAlphaValue != value)
                {
                    _fixedAlphaValue = value;
                    NotifyChange();
                }
            }
        }
        public TextureFilter TexFilterWhenTextureIsExpanded
        {
            get => _texFilterWhenTextureIsExpanded;
            set
            {
                if (_texFilterWhenTextureIsExpanded != value)
                {
                    _texFilterWhenTextureIsExpanded = value;
                    NotifyChange();
                }
            }
        }
        public Boolean AlphaCorrectionValue
        {
            get => _alphaCorrectionValue;
            set
            {
                if (_alphaCorrectionValue != value)
                {
                    _alphaCorrectionValue = value;
                    NotifyChange();
                }
            }
        }
        public ZValueDrawMask ZValueDrawMask
        {
            get => _zValueDrawMask;
            set
            {
                if (_zValueDrawMask != value)
                {
                    _zValueDrawMask = value;
                    NotifyChange();
                }
            }
        }
        public UInt16 LodParamK
        {
            get => _lodParamK;
            set
            {
                if (_lodParamK != value)
                {
                    _lodParamK = value;
                    NotifyChange();
                }
            }
        }
        public UInt16 LodParamL
        {
            get => _lodParamL;
            set
            {
                if (_lodParamL != value)
                {
                    _lodParamL = value;
                    NotifyChange();
                }
            }
        }
        public Guid TexID
        {
            get => _texID;
            set
            {
                if (_texID != value)
                {
                    _texID = value;
                    NotifyChange();
                }
            }
        }
        public Byte UnkVal1
        {
            get => _unkVal1;
            set
            {
                if (_unkVal1 != value)
                {
                    _unkVal1 = value;
                    NotifyChange();
                }
            }
        }
        public Byte UnkVal2
        {
            get => _unkVal2;
            set
            {
                if (_unkVal2 != value)
                {
                    _unkVal2 = value;
                    NotifyChange();
                }
            }
        }
        public Byte UnkVal3
        {
            get => _unkVal3;
            set
            {
                if (_unkVal3 != value)
                {
                    _unkVal3 = value;
                    NotifyChange();
                }
            }
        }
        public Boolean UnkFlag1
        {
            get => _unkFlag1;
            set
            {
                if (_unkFlag1 != value)
                {
                    _unkFlag1 = value;
                    NotifyChange();
                }
            }
        }
        public Boolean UnkFlag2
        {
            get => _unkFlag2;
            set
            {
                if (_unkFlag2 != value)
                {
                    _unkFlag2 = value;
                    NotifyChange();
                }
            }
        }
        public Boolean UnkFlag3
        {
            get => _unkFlag3;
            set
            {
                if (_unkFlag3 != value)
                {
                    _unkFlag3 = value;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel UnkVec1
        {
            get => _unkVec1;
            set
            {
                if (_unkVec1 != value)
                {
                    _unkVec1 = value;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel UnkVec2
        {
            get => _unkVec2;
            set
            {
                if (_unkVec2 != value)
                {
                    _unkVec2 = value;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel UnkVec3
        {
            get => _unkVec3;
            set
            {
                if (_unkVec3 != value)
                {
                    _unkVec3 = value;
                    NotifyChange();
                }
            }
        }
    }
}
