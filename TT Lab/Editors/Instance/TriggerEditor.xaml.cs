using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for TriggerEditor.xaml
    /// </summary>
    public partial class TriggerEditor : BaseEditor
    {
        private static DataTemplate instTemplate;
        private MenuItem addInstance;

        static TriggerEditor()
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

        public TriggerEditor()
        {
            InitializeComponent();
        }

        public TriggerEditor(TriggerViewModel viewModel, Command.CommandManager commandManager) : base(viewModel, commandManager)
        {
            InitializeComponent();
            DataContext = new
            {
                ViewModel = viewModel,
                Layers = Util.Layers
            };
            InitValidators();
            Loaded += TriggerEditor_Loaded;
        }

        private void TriggerEditor_Loaded(Object sender, System.Windows.RoutedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = (TriggerViewModel)AssetViewModel;
            vm.Instances.CollectionChanged += Instances_CollectionChanged;
            vm.PropertyChanged += TriggerViewModel_Changed;
            chunkEditor?.SceneRenderer.Scene.SetCameraPosition(new GlmNet.vec3(-vm.Position.X, vm.Position.Y, vm.Position.Z));

            UpdateAddInstancesMenu();
            InstancesListContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteInstanceFromListCommand, CommandManager)
            });

            Instances_CollectionChanged(null, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            InstancesList.ItemTemplate = instTemplate;
        }

        private void Instances_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = (TriggerViewModel)AssetViewModel;
            var instVMs = new List<ObjectInstanceViewModel>();
            var tree = chunkEditor!.ChunkTree;
            var instances = tree.Find(avm => avm.Asset.Name == "Instances");
            foreach (var item in vm.Instances)
            {
                instVMs.Add((ObjectInstanceViewModel)instances!.Children.First(inst => inst.Asset.LayoutID == (int)vm.LayoutID && inst.Asset.ID == item));
            }
            InstancesList.ItemsSource = instVMs;
            UpdateAddInstancesMenu();
        }

        private void TriggerViewModel_Changed(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LayoutID")
            {
                UpdateAddInstancesMenu();
            }
        }

        private void UpdateAddInstancesMenu()
        {
            if (addInstance != null)
            {
                InstancesListContextMenu.Items.Remove(addInstance);
            }
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var tree = chunkEditor!.ChunkTree;
            var instances = tree.Find(avm => avm.Alias == "Instances");
            var vm = (TriggerViewModel)AssetViewModel;
            var fitInstances = instances!.Children.Where((i) =>
            {
                return i.Asset.LayoutID == (int)vm.LayoutID && !vm.Instances.Contains((UInt16)i.Asset.ID);
            }).ToList();
            if (fitInstances.Any())
            {
                var instMenuItems = new MenuItem[fitInstances.Count];
                for (var i = 0; i < fitInstances.Count; ++i)
                {
                    var inst = (ObjectInstanceViewModel)fitInstances[i]!;
                    instMenuItems[i] = new MenuItem
                    {
                        Header = inst.Name,
                        Command = new GenerateCommand(() =>
                        {
                            var rc = new RelayCommand(new GenerateCommand(() =>
                            {
                                vm.Instances.Add((UInt16)inst.Asset.ID);
                            },
                            () =>
                            {
                                vm.Instances.Remove((UInt16)inst.Asset.ID);
                            }), CommandManager);
                            rc.Execute();
                        })
                    };
                }
                addInstance = new MenuItem
                {
                    Header = "Add",
                    ItemsSource = instMenuItems,
                };
                InstancesListContextMenu.Items.Insert(0, addInstance);
            }
        }

        private void InitValidators()
        {
            foreach (var pair in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(pair.Key, pair.Value);
            }
            var vm = (TriggerViewModel)AssetViewModel;
            AcceptNewPropValuePredicate[nameof(vm.UnkFloat)] = AcceptNewPropValuePredicate["X"];
            AcceptNewPropValuePredicate[nameof(vm.Header)] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return 0U;
                }
                if (!UInt32.TryParse(nStr.ToUpperInvariant(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out UInt32 result)) return null;
                return result;
            };
        }

        private void InstancesList_SelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var viewModel = (TriggerViewModel)AssetViewModel;
            viewModel.DeleteInstanceFromListCommand.Index = InstancesList.SelectedIndex;
        }
    }
}
