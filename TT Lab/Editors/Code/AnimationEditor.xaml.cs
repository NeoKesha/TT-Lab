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
