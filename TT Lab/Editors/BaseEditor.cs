using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Controls.Events;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    public class BaseEditor : UserControl
    {
        protected object viewModel;
        internal Command.CommandManager CommandManager = new Command.CommandManager();
        protected Dictionary<string, Func<object, object, object?>> AcceptNewPropValuePredicate = new Dictionary<string, Func<object, object, object?>>();

        public BaseEditor? ParentEditor { get; set; }

        public BaseEditor()
        {
            //throw new Exception("Can't create BaseEditor with no asset view model bound!");
        }

        public BaseEditor(object asset)
        {
            // This is used as reference because DataContext may not always be our viewmodel depending on specific editor needs
            viewModel = asset;
            DataContext = asset;
        }

        protected AssetViewModel AssetViewModel
        {
            get => (AssetViewModel)viewModel;
        }

        public BaseEditor(object asset, Command.CommandManager commandManager) : this(asset)
        {
            CommandManager = commandManager;
        }

        protected AbstractAssetData GetAssetData()
        {
            return AssetViewModel.Asset.GetData();
        }

        public void UndoExecuted(Object? sender, EventArgs e)
        {
            Control_UndoPerformed(sender, e);
        }

        public void RedoExecuted(Object? sender, EventArgs e)
        {
            Control_RedoPerformed(sender, e);
        }

        public void SaveExecuted(Object? sender, EventArgs e)
        {
            Control_SavePerformed(sender, e);
        }

        public void BoundPropertyChanged(Object? sender, BoundPropertyChangedEventArgs e)
        {
            if (AcceptNewPropValuePredicate.ContainsKey(e.PropName))
            {
                var data = AcceptNewPropValuePredicate[e.PropName].Invoke(e.NewValue, e.OldValue);
                if (data == null) return;
                SetData(e.PropName, data, e.Target);
            }
            else if (e.NewValue != e.OldValue)
            {
                SetData(e.PropName, e.NewValue, e.Target);
            }
        }

        protected virtual void Control_SavePerformed(Object? sender, EventArgs e)
        {
            if (AssetViewModel.IsDirty)
            {
                AssetViewModel.Save(null);
            }
        }

        protected virtual void Control_UndoPerformed(Object? sender, EventArgs e)
        {
            CommandManager.Undo();
        }

        protected virtual void Control_RedoPerformed(Object? sender, EventArgs e)
        {
            CommandManager.Redo();
        }

        protected void SetData<T>(string propName, T data, object? target = null)
        {
            if (target == null) target = viewModel;
            CommandManager.Execute(new SetDataCommand<T>(target, propName, data));
        }
    }
}
