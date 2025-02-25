using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using TT_Lab.Attributes;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels.Composite;

public class PrimitiveWrapperViewModel<T> : PropertyChangedBase, IDirtyMarker where T : IComparable
{
    private T _value;
    private bool isDirty;
    
    public PrimitiveWrapperViewModel() {}

    public PrimitiveWrapperViewModel(T value)
    {
        _value = value;
    }

    [MarkDirty]
    public T Value
    {
        get => _value;
        set
        {
            if (!_value.Equals(value))
            {
                _value = value;
                NotifyOfPropertyChange();
            }
        }
    }
    
    public void ResetDirty()
    {
        IsDirty = false;
    }

    public bool IsDirty
    {
        get => isDirty;
        set
        {
            if (isDirty != value)
            {
                isDirty = value;
                NotifyOfPropertyChange();
            }
        }
    }
}