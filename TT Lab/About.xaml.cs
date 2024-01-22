using AdonisUI.Controls;
using System;
using System.Diagnostics;

namespace TT_Lab
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : AdonisWindow
    {
        public About()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(Object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
