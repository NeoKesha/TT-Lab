using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using TT_Lab.Attributes;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels.Composite;

public class PrimitiveWrapperViewModel<T> : PropertyChangedBase, IDirtyMarker, IComparable, IEquatable<PrimitiveWrapperViewModel<T>> where T : IComparable
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

    public Int32 CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }
        
        return _value.CompareTo(((PrimitiveWrapperViewModel<T>)obj)._value);
    }

    public bool Equals(PrimitiveWrapperViewModel<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PrimitiveWrapperViewModel<T>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(_value);
    }

    public static bool operator ==(PrimitiveWrapperViewModel<T>? left, PrimitiveWrapperViewModel<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(PrimitiveWrapperViewModel<T>? left, PrimitiveWrapperViewModel<T>? right)
    {
        return !Equals(left, right);
    }
}