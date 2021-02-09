using System;
using System.Windows.Input;
using TT_Lab.AssetData.Code;
using TT_Lab.Command;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors.Code
{
    /// <summary>
    /// Interaction logic for AnimationEditor.xaml
    /// </summary>
    public partial class AnimationEditor : BaseEditor
    {
        public AnimationEditor()
        {
           InitializeComponent();
        }

        public AnimationEditor(AssetViewModel animation) : base(animation)
        {
            InitializeComponent();
        }
    }
}
