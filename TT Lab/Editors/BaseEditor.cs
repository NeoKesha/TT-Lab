using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    public class BaseEditor : UserControl
    {
        protected AssetViewModel AssetViewModel;
        protected CommandManager CommandManager = new CommandManager();

        public BaseEditor()
        {
            //throw new Exception("Can't create BaseEditor with no asset view model bound!");
        }

        public BaseEditor(AssetViewModel asset)
        {
            AssetViewModel = asset;
            DataContext = asset.Asset.GetData();
            AddKeybind(new RedoCommand(CommandManager), System.Windows.Input.Key.Y, System.Windows.Input.ModifierKeys.Control);
            AddKeybind(new UndoCommand(CommandManager), System.Windows.Input.Key.Z, System.Windows.Input.ModifierKeys.Control);
        }

        protected void AddKeybind(System.Windows.Input.ICommand command, System.Windows.Input.Key key, System.Windows.Input.ModifierKeys modifierKeys)
        {
            InputBindings.Add(new System.Windows.Input.KeyBinding(command, key, modifierKeys));
        }
    }
}
