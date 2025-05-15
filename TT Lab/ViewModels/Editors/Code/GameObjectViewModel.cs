using System;
using Caliburn.Micro;
using org.ogre;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Attributes;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Editors.Code.Behaviour;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.ViewModels.Editors.Code;

public class GameObjectViewModel : ResourceEditorViewModel
{
    private string _name;
    private ITwinObject.ObjectType _type;
    private byte _unkTypeValue;
    private byte _cameraReactJointAmount;
    private byte _exitPointAmount;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _ogiSlots;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _animationSlots;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _behaviourSlots;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _objectSlots;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _soundSlots;
    private Enums.InstanceState _instanceStateFlags;
    private BindableCollection<PrimitiveWrapperViewModel<uint>> _instFlags;
    private BindableCollection<PrimitiveWrapperViewModel<float>> _instFloats;
    private BindableCollection<PrimitiveWrapperViewModel<uint>> _instIntegers;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _refObjects;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _refOgis;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _refAnimations;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _refBehaviourCommandSequences;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _refBehaviours;
    private BindableCollection<PrimitiveWrapperViewModel<LabURI>> _refSounds;
    private BehaviourCommandPackViewModel _commandPack;
    private int _selectedAnimationOgiPairSlot;

    public GameObjectViewModel()
    {
        Scenes.Add(IoC.Get<SceneEditorViewModel>());
        ObjectScene.SceneHeaderModel = "Object Viewer";
        InitObjectScene();
        SelectedAnimationOgiPairSlot = 0;
    }

    private void InitObjectScene()
    {
        ObjectScene.SceneCreator = window =>
        {
            var sceneManager = window.GetSceneManager();
            var pivot = sceneManager.getRootSceneNode().createChildSceneNode();
            pivot.setPosition(0, 0, 0);
            window.SetCameraTarget(pivot);
            window.SetCameraStyle(CameraStyle.CS_ORBIT);
            window.EnableImgui(true);

            if (SelectedAnimationOgiPairSlot == -1 || OgiSlots[SelectedAnimationOgiPairSlot].Value == LabURI.Empty)
            {
                return;
            }
            
            // TODO: Create separate animation player that takes in OGI and animation
            var ogiData = AssetManager.Get().GetAssetData<OGIData>(OgiSlots[SelectedAnimationOgiPairSlot].Value);
            var ogiRender = new Rendering.Objects.OGI(EditableResource, sceneManager, ogiData);
            pivot.addChild(ogiRender.GetSceneNode());
            pivot.setInheritOrientation(false);
            pivot.setInheritScale(false);
        };
    }

    public override void LoadData()
    {
        var data = AssetManager.Get().GetAssetData<GameObjectData>(EditableResource);
        _name = data.Name;
        _type = data.Type;
        _unkTypeValue = data.UnkTypeValue;
        _cameraReactJointAmount = data.CameraReactJointAmount;
        _exitPointAmount = data.ExitPointAmount;
        _instanceStateFlags = data.InstanceStateFlags;
        _commandPack = new BehaviourCommandPackViewModel(this, data.BehaviourPack.ToString());
        DirtyTracker.AddChild(_commandPack);
        
        _ogiSlots = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _animationSlots = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _behaviourSlots = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _objectSlots = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _soundSlots = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        foreach (var ogiSlot in data.OGISlots)
        {
            _ogiSlots.Add(new PrimitiveWrapperViewModel<LabURI>(ogiSlot));
        }
        foreach (var animSlot in data.AnimationSlots)
        {
            _animationSlots.Add(new PrimitiveWrapperViewModel<LabURI>(animSlot));
        }
        foreach (var behaviourSlot in data.BehaviourSlots)
        {
            _behaviourSlots.Add(new PrimitiveWrapperViewModel<LabURI>(behaviourSlot));
        }
        foreach (var objectSlot in data.ObjectSlots)
        {
            _objectSlots.Add(new PrimitiveWrapperViewModel<LabURI>(objectSlot));
        }
        foreach (var soundSlot in data.SoundSlots)
        {
            _soundSlots.Add(new PrimitiveWrapperViewModel<LabURI>(soundSlot));
        }
        DirtyTracker.AddBindableCollection(_ogiSlots);
        DirtyTracker.AddBindableCollection(_animationSlots);
        DirtyTracker.AddBindableCollection(_behaviourSlots);
        DirtyTracker.AddBindableCollection(_objectSlots);
        DirtyTracker.AddBindableCollection(_soundSlots);
        
        _instFlags = new BindableCollection<PrimitiveWrapperViewModel<uint>>();
        _instFloats = new BindableCollection<PrimitiveWrapperViewModel<float>>();
        _instIntegers = new BindableCollection<PrimitiveWrapperViewModel<uint>>();
        foreach (var instFlags in data.InstFlags)
        {
            _instFlags.Add(new PrimitiveWrapperViewModel<uint>(instFlags));
        }
        foreach (var instFlags in data.InstFloats)
        {
            _instFloats.Add(new PrimitiveWrapperViewModel<float>(instFlags));
        }
        foreach (var instFlags in data.InstIntegers)
        {
            _instIntegers.Add(new PrimitiveWrapperViewModel<uint>(instFlags));
        }
        DirtyTracker.AddBindableCollection(_instFlags);
        DirtyTracker.AddBindableCollection(_instFloats);
        DirtyTracker.AddBindableCollection(_instIntegers);
        
        _refObjects = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _refOgis = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _refAnimations = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _refBehaviourCommandSequences = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _refBehaviours = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        _refSounds = new BindableCollection<PrimitiveWrapperViewModel<LabURI>>();
        foreach (var refObject in data.RefObjects)
        {
            _refObjects.Add(new PrimitiveWrapperViewModel<LabURI>(refObject));
        }
        foreach (var refOgi in data.RefOGIs)
        {
            _refOgis.Add(new PrimitiveWrapperViewModel<LabURI>(refOgi));
        }
        foreach (var refAnimation in data.RefAnimations)
        {
            _refAnimations.Add(new PrimitiveWrapperViewModel<LabURI>(refAnimation));
        }
        foreach (var refCommandSequence in data.RefBehaviourCommandsSequences)
        {
            _refBehaviourCommandSequences.Add(new PrimitiveWrapperViewModel<LabURI>(refCommandSequence));
        }
        foreach (var refBehaviour in data.RefBehaviours)
        {
            _refBehaviours.Add(new PrimitiveWrapperViewModel<LabURI>(refBehaviour));
        }
        foreach (var refSound in data.RefSounds)
        {
            _refSounds.Add(new PrimitiveWrapperViewModel<LabURI>(refSound));
        }
        DirtyTracker.AddBindableCollection(_refObjects);
        DirtyTracker.AddBindableCollection(_refOgis);
        DirtyTracker.AddBindableCollection(_refAnimations);
        DirtyTracker.AddBindableCollection(_refBehaviourCommandSequences);
        DirtyTracker.AddBindableCollection(_refBehaviours);
        DirtyTracker.AddBindableCollection(_refSounds);
    }

    protected override void Save()
    {
        var data = AssetManager.Get().GetAssetData<GameObjectData>(EditableResource);
        data.Name = _name;
        data.UnkTypeValue = _unkTypeValue;
        data.CameraReactJointAmount = _cameraReactJointAmount;
        data.ExitPointAmount = _exitPointAmount;
        data.InstanceStateFlags = _instanceStateFlags;
        _commandPack.Save(data.BehaviourPack);
        
        data.OGISlots.Clear();
        data.AnimationSlots.Clear();
        data.BehaviourSlots.Clear();
        data.ObjectSlots.Clear();
        data.SoundSlots.Clear();
        foreach (var ogiSlot in _ogiSlots)
        {
            data.OGISlots.Add(ogiSlot.Value);
        }
        foreach (var animSlot in _animationSlots)
        {
            data.AnimationSlots.Add(animSlot.Value);
        }
        foreach (var behaviourSlot in _behaviourSlots)
        {
            data.BehaviourSlots.Add(behaviourSlot.Value);
        }
        foreach (var objectSlot in _objectSlots)
        {
            data.ObjectSlots.Add(objectSlot.Value);
        }
        foreach (var soundSlot in _soundSlots)
        {
            data.SoundSlots.Add(soundSlot.Value);
        }
        
        data.InstFlags.Clear();
        data.InstFloats.Clear();
        data.InstIntegers.Clear();
        foreach (var instFlag in _instFlags)
        {
            data.InstFlags.Add(instFlag.Value);
        }
        foreach (var instFloat in _instFloats)
        {
            data.InstFloats.Add(instFloat.Value);
        }
        foreach (var instInteger in _instIntegers)
        {
            data.InstIntegers.Add(instInteger.Value);
        }
        
        data.RefObjects.Clear();
        data.RefOGIs.Clear();
        data.RefAnimations.Clear();
        data.RefBehaviours.Clear();
        data.RefSounds.Clear();
        foreach (var refObject in _refObjects)
        {
            data.RefObjects.Add(refObject.Value);
        }
        foreach (var refObject in _refOgis)
        {
            data.RefOGIs.Add(refObject.Value);
        }
        foreach (var refObject in _refAnimations)
        {
            data.RefAnimations.Add(refObject.Value);
        }
        foreach (var refObject in _refBehaviours)
        {
            data.RefBehaviours.Add(refObject.Value);
        }
        foreach (var refObject in _refSounds)
        {
            data.RefSounds.Add(refObject.Value);
        }
    }

    [MarkDirty]
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                NotifyOfPropertyChange();
            }
        }
    }

    [MarkDirty]
    public ITwinObject.ObjectType Type
    {
        get => _type;
        set
        {
            if (_type != value)
            {
                _type = value;
                NotifyOfPropertyChange();
            }
        }
    }

    [MarkDirty]
    public byte UnkTypeValue
    {
        get => _unkTypeValue;
        set
        {
            if (_unkTypeValue != value)
            {
                _unkTypeValue = value;
                NotifyOfPropertyChange();
            }
        }
    }

    [MarkDirty]
    public byte CameraReactJointAmount
    {
        get => _cameraReactJointAmount;
        set
        {
            if (_cameraReactJointAmount != value)
            {
                _cameraReactJointAmount = value;
                NotifyOfPropertyChange();
            }
        }
    }

    [MarkDirty]
    public byte ExitPointAmount
    {
        get => _exitPointAmount;
        set
        {
            if (_exitPointAmount != value)
            {
                _exitPointAmount = value;
                NotifyOfPropertyChange();
            }
        }
    }

    [MarkDirty]
    public Enums.InstanceState InstanceStateFlags
    {
        get => _instanceStateFlags;
        set
        {
            if (_instanceStateFlags != value)
            {
                _instanceStateFlags = value;
                NotifyOfPropertyChange();
            }
        }
    }

    public SceneEditorViewModel ObjectScene => Scenes[0];

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> OgiSlots => _ogiSlots;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> AnimationSlots => _animationSlots;

    public int SelectedAnimationOgiPairSlot
    {
        get => _selectedAnimationOgiPairSlot;
        set
        {
            if (_selectedAnimationOgiPairSlot != value)
            {
                _selectedAnimationOgiPairSlot = value;
                ObjectScene.ResetScene();
                NotifyOfPropertyChange();
            }
        }
    }

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> BehaviourSlots => _behaviourSlots;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> ObjectSlots => _objectSlots;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> SoundSlots => _soundSlots;

    public BindableCollection<PrimitiveWrapperViewModel<UInt32>> InstFlags => _instFlags;

    public BindableCollection<PrimitiveWrapperViewModel<Single>> InstFloats => _instFloats;

    public BindableCollection<PrimitiveWrapperViewModel<UInt32>> InstIntegers => _instIntegers;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> RefObjects => _refObjects;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> RefOgis => _refOgis;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> RefAnimations => _refAnimations;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> RefBehaviourCommandSequences => _refBehaviourCommandSequences;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> RefBehaviours => _refBehaviours;

    public BindableCollection<PrimitiveWrapperViewModel<LabURI>> RefSounds => _refSounds;

    public BehaviourCommandPackViewModel CommandPack => _commandPack;
}