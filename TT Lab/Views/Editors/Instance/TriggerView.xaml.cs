using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TT_Lab.ViewModels.Editors.Instance;

namespace TT_Lab.Views.Editors.Instance
{
    /// <summary>
    /// Interaction logic for TriggerView.xaml
    /// </summary>
    public partial class TriggerView : UserControl
    {
        private static DataTemplate instTemplate;

        static TriggerView()
        {
            var instVM = typeof(ObjectInstanceViewModel);
            string xamlTemplate = $"<DataTemplate DataType=\"{{x:Type vm:{instVM.Name}}}\"><TextBlock Text=\"{{Binding Name}}\" /></DataTemplate>";
            var xaml = xamlTemplate;
            var context = new ParserContext
            {
                XamlTypeMapper = new XamlTypeMapper(Array.Empty<String>())
            };
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", instVM.Namespace, instVM.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");

            instTemplate = (DataTemplate)XamlReader.Parse(xaml, context);
        }

        public TriggerView()
        {
            InitializeComponent();
        }

    }
}
