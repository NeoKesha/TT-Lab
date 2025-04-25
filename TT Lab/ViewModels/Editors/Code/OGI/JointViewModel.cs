using System;
using Caliburn.Micro;
using TT_Lab.Attributes;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Editors.Code.OGI;

public class JointViewModel : Screen, IHaveParentEditor<OGIViewModel>, ISaveableViewModel<TwinJoint>
{
    private Vector4ViewModel _localTranslation = new();
    private Vector4ViewModel _localRotation = new();
    private Vector4ViewModel _worldTranslation = new();
    private Vector4ViewModel _worldRotation = new();
    private Vector4ViewModel _additionalAnimationRotation = new();
    private Int32 _reactId = -1;
    private DirtyTracker _dirtyTracker;
    private Int32 _index = 0;
    private Int32 _parentIndex = -1;
    private Int32 _childrenAmt1 = 0;
    private Int32 _childrenAmt2 = 0;

    public OGIViewModel ParentEditor { get; set; }

    public JointViewModel(OGIViewModel parentEditor)
    {
        ParentEditor = parentEditor;
        _dirtyTracker = new DirtyTracker(this);
        _dirtyTracker.AddChild(_localTranslation);
        _dirtyTracker.AddChild(_localRotation);
        _dirtyTracker.AddChild(_worldTranslation);
        _dirtyTracker.AddChild(_worldRotation);
        _dirtyTracker.AddChild(_additionalAnimationRotation);
    }

    public JointViewModel(OGIViewModel parentEditor, TwinJoint joint) : this(parentEditor)
    {
        _localTranslation = new Vector4ViewModel(joint.LocalTranslation);
        _localRotation = new Vector4ViewModel(joint.LocalRotation);
        _worldTranslation = new Vector4ViewModel(joint.WorldTranslation);
        _worldRotation = new Vector4ViewModel(joint.UnusedRotation);
        _additionalAnimationRotation = new Vector4ViewModel(joint.AdditionalAnimationRotation);
        _reactId = joint.ReactId;
        _index = joint.Index;
        _parentIndex = joint.ParentIndex;
        _childrenAmt1 = joint.ChildrenAmt1;
        _childrenAmt2 = joint.ChildrenAmt2;
    }

    public void Save(TwinJoint o)
    {
        o.ChildrenAmt1 =  _childrenAmt1;
        o.ChildrenAmt2 =  _childrenAmt2;
        o.Index = _index;
        o.ParentIndex = _parentIndex;
        o.LocalTranslation = new Vector4(_localTranslation.X, _localTranslation.Y, _localTranslation.Z, _localTranslation.W);
        o.LocalRotation = new Vector4(_localRotation.X, _localRotation.Y, _localRotation.Z, _localRotation.W);
        o.WorldTranslation = new Vector4(_worldTranslation.X, _worldTranslation.Y, _worldTranslation.Z, _worldTranslation.W);
        o.UnusedRotation = new Vector4(_worldRotation.X, _worldRotation.Y, _worldRotation.Z, _worldRotation.W);
    }

    public void ResetDirty()
    {
        _dirtyTracker.ResetDirty();
    }

    public bool IsDirty => _dirtyTracker.IsDirty;

    public Vector4ViewModel LocalTranslation => _localTranslation;

    public Vector4ViewModel LocalRotation => _localRotation;

    public Vector4ViewModel WorldTranslation => _worldTranslation;

    public Vector4ViewModel WorldRotation => _worldRotation;

    public Vector4ViewModel AdditionalAnimationRotation => _additionalAnimationRotation;

    public override String ToString()
    {
        return $"Joint {_index}";
    }

    [MarkDirty]
    public Int32 ReactId
    {
        get => _reactId;
        set
        {
            if (value == _reactId) return;
            _reactId = value;
            NotifyOfPropertyChange();
        }
    }
}