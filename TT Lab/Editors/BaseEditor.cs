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
        }
    }
}
