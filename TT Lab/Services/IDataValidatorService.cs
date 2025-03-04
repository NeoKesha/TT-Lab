using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TT_Lab.Services;

public interface IDataValidatorService : INotifyDataErrorInfo
{
    void RaiseErrorsChanged([CallerMemberName] string propertyName = "");
    void AddError(string propertyName, string message);
    void RemoveError(string propertyName, string? message = null);
    void RegisterProperty<T>(string propertyName, Func<T, Boolean> validator);
    void RegisterOwner(INotifyDataErrorInfo source);
    Boolean ValidateProperty<T>(T newValue, [CallerMemberName] string propertyName = "");
}