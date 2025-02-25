using AdonisUI.Controls;
using Caliburn.Micro;
using TT_Lab.Rendering;

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

        private void AdonisWindow_SizeChanged(System.Object sender, System.Windows.SizeChangedEventArgs e)
        {
            IoC.Get<OgreWindowManager>().NotifyResizeAllWindows();
        }

        private void AdonisWindow_LocationChanged(System.Object sender, System.EventArgs e)
        {
            IoC.Get<OgreWindowManager>().NotifyResizeAllWindows();
        }
    }
}
