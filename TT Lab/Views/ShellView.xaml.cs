using AdonisUI.Controls;

namespace TT_Lab.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : AdonisWindow
    {
        public ShellView()
        {
            InitializeComponent();
            Log.SetLogBox(LogText);
            Preferences.Load();
        }
    }
}
