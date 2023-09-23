using System;
using TT_Lab.Util;

namespace TT_Lab.Command
{
    public class SetDataCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private T _prevValue;
        private T _value;
        private string _propName;
        private object _target;

        public SetDataCommand(object target, string propName, T value)
        {
            _target = target;
            _propName = propName;
            _value = value;
        }

        public Boolean CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter = null)
        {
            Type t = _target.GetType();
            var field = t.GetProperty(_propName);
            _prevValue = CloneUtils.DeepClone((T)field.GetValue(_target));
            field.SetValue(_target, _value);
        }

        public void Unexecute()
        {
            Type t = _target.GetType();
            var field = t.GetProperty(_propName);
            if (field.PropertyType.IsEnum)
            {
                field.SetValue(_target, MiscUtils.ConvertEnum(field.PropertyType, _prevValue));
            }
            else
            {
                field.SetValue(_target, Convert.ChangeType(_prevValue, field.PropertyType));
            }
        }
    }
}
