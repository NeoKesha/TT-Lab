using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class SetDataCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private object _prevValue;
        private object _value;
        private string _propName;
        private object _target;

        public SetDataCommand(object target, string propName, object value)
        {
            _target = target;
            _propName = propName;
            _value = value;
        }

        public Boolean CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter = null)
        {
            Type t = _target.GetType();
            var field = t.GetProperty(_propName);
            _prevValue = field.GetValue(_target);
            field.SetValue(_target, _value);
        }

        public void Unexecute()
        {
            Type t = _target.GetType();
            var field = t.GetProperty(_propName);
            field.SetValue(_target, _prevValue);
        }
    }
}
