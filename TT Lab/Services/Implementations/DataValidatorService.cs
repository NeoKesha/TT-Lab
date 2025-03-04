using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TT_Lab.Services.Implementations;

public class DataValidatorService : IDataValidatorService
{
    private readonly Dictionary<string, List<string>> _propertyErrors = new();
    private readonly Dictionary<string, Func<object, Boolean>> _validators = new();

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName) || !_propertyErrors.TryGetValue(propertyName, out var errorList))
        {
            return Enumerable.Empty<string>();
        }

        return errorList;
    }

    public bool HasErrors => _propertyErrors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    
    public void RaiseErrorsChanged([CallerMemberName] string propertyName = "")
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public void AddError(string propertyName, string message)
    {
        if (!_propertyErrors.ContainsKey(propertyName))
        {
            _propertyErrors[propertyName] = new List<String>();
        }

        if (!_propertyErrors[propertyName].Contains(message))
        {
            _propertyErrors[propertyName].Add(message);
            RaiseErrorsChanged(propertyName);
        }
    }

    public void RemoveError(string propertyName, string? message = null)
    {
        if (!_propertyErrors.TryGetValue(propertyName, out List<String>? errorsList))
        {
            return;
        }

        if (string.IsNullOrEmpty(message))
        {
            errorsList.Clear();
        }
        else
        {
            if (!errorsList.Contains(message))
            {
                return;
            }
            errorsList.Remove(message);
        }

        if (errorsList.Count == 0)
        {
            _propertyErrors.Remove(propertyName);
        }
        RaiseErrorsChanged(propertyName);
    }

    public void RegisterProperty<T>(string propertyName, Func<T, Boolean> validator)
    {
        _validators.Add(propertyName, (t) =>
        {
            Debug.Assert(t is T, $"Incompatible types {t.GetType().Name} and {typeof(T).Name}");
            return validator((T)t);
        });
    }

    public void RegisterOwner(INotifyDataErrorInfo source)
    {
        throw new NotImplementedException();
    }

    public Boolean ValidateProperty<T>(T newValue, [CallerMemberName] string propertyName = "")
    {
        Debug.Assert(_validators.ContainsKey(propertyName), $"A validator was not registered for {propertyName}!");
        return _validators[propertyName].Invoke(newValue!);
    }
}