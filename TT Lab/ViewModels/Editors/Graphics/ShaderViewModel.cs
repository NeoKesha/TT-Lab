using Caliburn.Micro;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Assets;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Objects;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;
using static Twinsanity.TwinsanityInterchange.Common.TwinShader;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class ShaderViewModel : Conductor<IScreen>.Collection.AllActive, IHaveParentEditor<MaterialViewModel>
    {
        private SceneEditorViewModel _textureViewer;

        private String _name = "Shader";
        private TwinShader.Type _type;
        private UInt32 _intParam;
        private Single[] _floatParam;
        private AlphaBlending _alphaBlending;
        private AlphaBlendPresets _alphaRegSettingsIndex;
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
        private Boolean _useCustomAlphaRegSettings;
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
        private LabURI _texID;
        private Byte _unkVal1;
        private Byte _unkVal2;
        private Byte _unkVal3;
        private Boolean _unkFlag1;
        private Boolean _unkFlag2;
        private Boolean _unkFlag3;
        private Vector4ViewModel _unkVec1;
        private Vector4ViewModel _unkVec2;
        private Vector4ViewModel _unkVec3;

        public ShaderViewModel(MaterialViewModel materialEditor)
        {
            ParentEditor = materialEditor;

            Type = TwinShader.Type.StandardUnlit;
            _floatParam = new Single[4];
            _unkVec1 = new Vector4ViewModel();
            _unkVec2 = new Vector4ViewModel();
            _unkVec3 = new Vector4ViewModel();
            _texID = LabURI.Empty;
            _textureViewer = IoC.Get<SceneEditorViewModel>();

            InitTextureViewer();
        }

        public ShaderViewModel(LabShader shader, MaterialViewModel materialEditor) : this(materialEditor)
        {
            Type = shader.ShaderType;
            _intParam = shader.IntParam;
            for (var i = 0; i < _floatParam.Length; ++i)
            {
                _floatParam[i] = shader.FloatParam[i];
            }
            _alphaBlending = shader.ABlending;
            _alphaRegSettingsIndex = shader.AlphaRegSettingsIndex;
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
            _useCustomAlphaRegSettings = shader.UseCustomAlphaRegSettings;
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
        }

        public void ShaderViewModelFileDrop(Controls.FileDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.File))
            {
                var data = e.Data.Data as ResourceTreeElementViewModel;
                if (data.Asset.Type.Name == "Texture")
                {
                    TexID = data.Asset.URI;
                    InitTextureViewer();
                }
            }
        }

        private void InitTextureViewer()
        {
            TextureViewer.SceneCreator = (GLWindow glControl) =>
            {
                glControl.SetRendererLibraries(ShaderStorage.LibraryFragmentShaders.TexturePass);

                var texId = TexID;
                var hasMapping = TxtMapping;
                Bitmap bitmap;
                if (texId == LabURI.Empty || !hasMapping)
                {
                    bitmap = MiscUtils.GetBoatGuy();
                }
                else
                {
                    var texData = AssetManager.Get().GetAsset(texId).GetData<TextureData>();
                    bitmap = texData.Bitmap;
                }
                var scene = new Scene(glControl.RenderContext, glControl, (float)glControl.RenderControl.Width, (float)glControl.RenderControl.Height);
                scene.SetCameraSpeed(0);
                scene.DisableCameraManipulation();

                var texPlane = new Plane(glControl.RenderContext, glControl, scene, bitmap);
                scene.AddChild(texPlane);

                return scene;
            };
        }

        public MaterialViewModel ParentEditor { get; set; }

        public SceneEditorViewModel TextureViewer
        {
            get => _textureViewer;
        }

        public String Name
        {
            get => _name;
            private set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Boolean HasIntParam
        {
            get => _type == TwinShader.Type.UnlitClothDeformation || _type == TwinShader.Type.UnlitClothDeformation2;
        }

        public Boolean HasFloatParam1
        {
            get
            {
                return _type switch
                {
                    TwinShader.Type.LitReflectionSurface or TwinShader.Type.SHADER_17 or TwinShader.Type.UnlitClothDeformation or TwinShader.Type.UnlitClothDeformation2 => true,
                    _ => false,
                };
            }
        }

        public Boolean HasFloatParam2
        {
            get
            {
                return _type switch
                {
                    TwinShader.Type.UnlitClothDeformation or TwinShader.Type.UnlitClothDeformation2 => true,
                    _ => false,
                };
            }
        }

        public Boolean HasFloatParam3
        {
            get
            {
                return _type switch
                {
                    TwinShader.Type.UnlitClothDeformation2 => true,
                    _ => false,
                };
            }
        }

        public Boolean HasFloatParam4
        {
            get
            {
                return _type switch
                {
                    TwinShader.Type.UnlitClothDeformation2 => true,
                    _ => false,
                };
            }
        }

        public Boolean CanColorSpec
        {
            get => UseCustomAlphaRegSettings && AlphaBlending;
        }

        public Boolean UsePresetBlending
        {
            get => !UseCustomAlphaRegSettings && AlphaBlending;
        }

        public TwinShader.Type Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    _name = $"{_type}";
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(HasIntParam));
                    NotifyOfPropertyChange(nameof(HasFloatParam1));
                    NotifyOfPropertyChange(nameof(HasFloatParam2));
                    NotifyOfPropertyChange(nameof(HasFloatParam3));
                    NotifyOfPropertyChange(nameof(HasFloatParam4));
                    NotifyOfPropertyChange(nameof(Name));
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
                    NotifyOfPropertyChange();
                }
            }
        }

        public Single[] FloatParam { get => _floatParam; private set => _floatParam = value; }

        public Boolean AlphaBlending
        {
            get => _alphaBlending == TwinShader.AlphaBlending.ON;
            set
            {
                if (AlphaBlending != value)
                {
                    _alphaBlending = value ? TwinShader.AlphaBlending.ON : TwinShader.AlphaBlending.OFF;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(CanColorSpec));
                    NotifyOfPropertyChange(nameof(UsePresetBlending));
                }
            }
        }

        public AlphaBlendPresets AlphaRegSettingsIndex
        {
            get => _alphaRegSettingsIndex;
            set
            {
                if (_alphaRegSettingsIndex != value)
                {
                    _alphaRegSettingsIndex = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Boolean AlphaTest
        {
            get => _alphaTest == TwinShader.AlphaTest.ON;
            set
            {
                if (AlphaTest != value)
                {
                    _alphaTest = value ? TwinShader.AlphaTest.ON : TwinShader.AlphaTest.OFF;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }
        public Boolean DAlphaTest
        {
            get => _dAlphaTest == DestinationAlphaTest.ON;
            set
            {
                if (DAlphaTest != value)
                {
                    _dAlphaTest = value ? DestinationAlphaTest.ON : DestinationAlphaTest.OFF;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }
        public Boolean TxtMapping
        {
            get => _txtMapping == TextureMapping.ON;
            set
            {
                if (TxtMapping != value)
                {
                    _txtMapping = value ? TextureMapping.ON : TextureMapping.OFF;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }
        public Boolean Fog
        {
            get => _fog == Fogging.ON;
            set
            {
                if (Fog != value)
                {
                    _fog = value ? Fogging.ON : Fogging.OFF;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }
        public Boolean UseCustomAlphaRegSettings
        {
            get => _useCustomAlphaRegSettings;
            set
            {
                if (_useCustomAlphaRegSettings != value)
                {
                    _useCustomAlphaRegSettings = value;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(CanColorSpec));
                    NotifyOfPropertyChange(nameof(UsePresetBlending));
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }
        public LabURI TexID
        {
            get => _texID;
            set
            {
                if (_texID != value)
                {
                    _texID = value;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
