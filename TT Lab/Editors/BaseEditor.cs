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

        public BaseEditor()
        {
            //throw new Exception("Can't create BaseEditor with no asset view model bound!");
        }

        public BaseEditor(AssetViewModel asset)
        {
            // This is used as reference because DataContext may not always be our viewmodel depending on specific editor needs
            viewModel = asset;
            DataContext = asset;
        }

        public BaseEditor(AssetViewModel asset, TT_Lab.Command.CommandManager commandManager) : this(asset)
        {
            CommandManager = commandManager;
        }

        protected AbstractAssetData GetAssetData()
        {
            return viewModel.Asset.GetData();
        }

        public void UndoExecuted(Object sender, EventArgs e)
        {
            Control_UndoPerformed(sender, e);
        }

        public void RedoExecuted(Object sender, EventArgs e)
        {
            Control_RedoPerformed(sender, e);
        }

        public void SaveExecuted(Object sender, EventArgs e)
        {
            Control_SavePerformed(sender, e);
        }

        protected virtual void Control_SavePerformed(Object sender, EventArgs e)
        {
            if (viewModel.IsDirty)
            {
                viewModel.Save();
            }
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
