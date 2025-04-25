using System;
using Caliburn.Micro;
using TT_Lab.Attributes;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Editors.Code.OGI;

public class ExitPointViewModel : Screen, IHaveParentEditor<OGIViewModel>, ISaveableViewModel<TwinExitPoint>
{
    private DirtyTracker _dirtyTracker;
    private UInt32 _parentJointIndex;
    private UInt32 _id;
    private Matrix4ViewModel _matrix = new();
    
    public OGIViewModel ParentEditor { get; set; }

    public ExitPointViewModel(OGIViewModel parentEditor)
    {
        ParentEditor = parentEditor;
        _dirtyTracker = new DirtyTracker(this);
        _dirtyTracker.AddChild(_matrix);
    }

    public ExitPointViewModel(OGIViewModel parentEditor, TwinExitPoint exitPoint) : this(parentEditor)
    {
        _parentJointIndex = exitPoint.ParentJointIndex;
        _id =  exitPoint.ID;
        _matrix = new Matrix4ViewModel(exitPoint.Matrix);
    }

    public void Save(TwinExitPoint o)
    {
        o.ParentJointIndex = _parentJointIndex;
        o.ID = _id;
        o.Matrix = new Matrix4
        {
            Column1 = new Vector4(_matrix.V1.X, _matrix.V1.Y, _matrix.V1.Z, _matrix.V1.W),
            Column2 = new Vector4(_matrix.V2.X, _matrix.V2.Y, _matrix.V2.Z, _matrix.V2.W),
            Column3 = new Vector4(_matrix.V3.X, _matrix.V3.Y, _matrix.V3.Z, _matrix.V3.W),
            Column4 = new Vector4(_matrix.V4.X, _matrix.V4.Y, _matrix.V4.Z, _matrix.V4.W)
        };
    }

    public override String ToString()
    {
        return $"Exit Point {_id}";
    }

    public void ResetDirty()
    {
        _dirtyTracker.ResetDirty();
    }

    [MarkDirty]
    public UInt32 ParentJointIndex
    {
        get => _parentJointIndex;
        set
        {
            if (_parentJointIndex != value)
            {
                _parentJointIndex = value;
                NotifyOfPropertyChange();
            }
        }
    }

    [MarkDirty]
    public UInt32 ID
    {
        get => _id;
        set
        {
            if (_id != value)
            {
                _id = value;
                NotifyOfPropertyChange();
            }
        }
    }

    public Matrix4ViewModel Matrix => _matrix;

    public bool IsDirty => _dirtyTracker.IsDirty;
}