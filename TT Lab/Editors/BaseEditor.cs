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
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    public class BaseEditor : UserControl
    {
        protected AssetViewModel viewModel;
        protected Command.CommandManager CommandManager = new Command.CommandManager();

        protected static RoutedCommand UndoCommand = new RoutedCommand();
        protected static RoutedCommand RedoCommand = new RoutedCommand();
        protected static RoutedCommand SaveCommand = new RoutedCommand();

        public BaseEditor()
        {
            //throw new Exception("Can't create BaseEditor with no asset view model bound!");
        }

        public BaseEditor(AssetViewModel asset)
        {
            // This is used as reference because DataContext may not always be our viewmodel depending on specific editor needs
            viewModel = asset;
            DataContext = asset;
            var undoBinding = new CommandBinding(UndoCommand, UndoExecuted);
            var redoBinding = new CommandBinding(RedoCommand, RedoExecuted);
            var saveBinding = new CommandBinding(SaveCommand, SaveExecuted, CanSave);
            CommandBindings.Add(undoBinding);
            CommandBindings.Add(redoBinding);
            CommandBindings.Add(saveBinding);
            AddKeybind(RedoCommand, Key.Y, ModifierKeys.Control);
            AddKeybind(UndoCommand, Key.Z, ModifierKeys.Control);
            AddKeybind(SaveCommand, Key.S, ModifierKeys.Control);
        }

        public BaseEditor(AssetViewModel asset, TT_Lab.Command.CommandManager commandManager) : this(asset)
        {
            CommandManager = commandManager;
        }

        protected AbstractAssetData GetAssetData()
        {
            return viewModel.Asset.GetData();
        }

        protected void AddKeybind(System.Windows.Input.ICommand command, Key key, ModifierKeys modifierKeys)
        {
            InputBindings.Add(new KeyBinding(command, key, modifierKeys));
        }

        protected void UndoExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            Control_UndoPerformed(sender, e);
        }

        protected void RedoExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            Control_RedoPerformed(sender, e);
        }

        protected void SaveExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.Save();
        }

        protected virtual void CanSave(Object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = viewModel.IsDirty;
        }

        protected virtual void Control_UndoPerformed(Object sender, EventArgs e)
        {
            CommandManager.Undo();
        }

        protected virtual void Control_RedoPerformed(Object sender, EventArgs e)
        {
            CommandManager.Redo();
        }

        protected void SetData(string propName, object data)
        {
            CommandManager.Execute(new SetDataCommand(viewModel, propName, data));
        }
    }
}
