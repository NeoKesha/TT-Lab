using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for ObjectInstanceEditor.xaml
    /// </summary>
    public partial class ObjectInstanceEditor : BaseEditor
    {
        private MenuItem addInstance;
        private MenuItem addPosition;
        private MenuItem addPath;

        public ObjectInstanceEditor()
        {
            InitializeComponent();
        }

        public ObjectInstanceEditor(ObjectInstanceViewModel vm, CommandManager commandManager) : base(vm, commandManager)
        {
            InitializeComponent();
            InitValidators();
            Loaded += ObjectInstanceEditor_Loaded;
        }

        private void ObjectInstanceEditor_Loaded(Object sender, RoutedEventArgs e)
        {
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            var chunkEditor = (ChunkEditor)ParentEditor!;
            chunkEditor?.SceneRenderer.Scene.SetCameraPosition(new GlmNet.vec3(-vm.Position.X, vm.Position.Y, vm.Position.Z));
            vm.PropertyChanged += ObjectInstanceViewModel_PropertyChanged;
            vm.Instances.CollectionChanged += Instances_CollectionChanged;
            vm.Positions.CollectionChanged += Positions_CollectionChanged;
            vm.Paths.CollectionChanged += Paths_CollectionChanged;
            var allObj = new ObservableCollection<object>(AssetManager.Get().GetAssets().Values.Where(a => a.Type == typeof(GameObject)).Select(a => a.GetViewModel()).Cast<object>());
            var allScripts = new ObservableCollection<object>(AssetManager.Get().GetAssets().Values.Where(a => a.Type == typeof(BehaviourStarter)).Select(a => a.GetViewModel()).Cast<object>());
            DataContext = new
            {
                ViewModel = vm,
                Layers = Util.Layers,
                AllObjects = allObj,
                AllScripts = allScripts,
            };

            UpdateAddInstanceMenu();
            LinkedInstancesContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteLinkedInstanceCommand, CommandManager)
            });
            UpdateAddPathMenu();
            LinkedPathsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteLinkedPathCommand, CommandManager)
            });
            UpdateAddPositionMenu();
            LinkedPositionsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteLinkedPositionCommand, CommandManager)
            });

            IntPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Add",
                Command = new RelayCommand(vm.AddIntParamCommand, CommandManager)
            });
            IntPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteIntParamCommand, CommandManager)
            });
            FlagPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Add",
                Command = new RelayCommand(vm.AddFlagParamCommand, CommandManager)
            });
            FlagPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteFlagParamCommand, CommandManager)
            });
            FloatPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Add",
                Command = new RelayCommand(vm.AddFloatParamCommand, CommandManager)
            });
            FloatPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteFloatParamCommand, CommandManager)
            });

            Paths_CollectionChanged(null, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            Instances_CollectionChanged(null, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
            Positions_CollectionChanged(null, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
        }

        private void Paths_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            var pathVMs = new List<PathViewModel>();
            var tree = chunkEditor!.ChunkTree;
            var positions = tree.Find(avm => avm.Asset.Name == "Paths");
            foreach (var item in vm.Paths)
            {
                pathVMs.Add((PathViewModel)positions!.Children.First(inst => inst.Asset.LayoutID == (int)vm.LayoutID && inst.Asset.ID == item));
            }
            LinkedPathsList.ItemsSource = pathVMs;
            UpdateAddPathMenu();
        }

        private void Positions_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            var posVMs = new List<PositionViewModel>();
            var tree = chunkEditor!.ChunkTree;
            var positions = tree.Find(avm => avm.Asset.Name == "Positions");
            foreach (var item in vm.Positions)
            {
                posVMs.Add((PositionViewModel)positions!.Children.First(inst => inst.Asset.LayoutID == (int)vm.LayoutID && inst.Asset.ID == item));
            }
            LinkedPositionsList.ItemsSource = posVMs;
            UpdateAddPositionMenu();
        }

        private void Instances_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            var instVMs = new List<ObjectInstanceViewModel>();
            var tree = chunkEditor!.ChunkTree;
            var instances = tree.Find(avm => avm.Asset.Name == "Instances");
            foreach (var item in vm.Instances)
            {
                instVMs.Add((ObjectInstanceViewModel)instances!.Children.First(inst => inst.Asset.LayoutID == (int)vm.LayoutID && inst.Asset.ID == item));
            }
            LinkedInstancesList.ItemsSource = instVMs;
            UpdateAddInstanceMenu();
        }

        private void ObjectInstanceViewModel_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LayoutID")
            {
                UpdateAddInstanceMenu();
                UpdateAddPathMenu();
                UpdateAddPositionMenu();
            }
        }

        private void InitValidators()
        {
            foreach (var rule in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(rule.Key, rule.Value);
            }
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            AcceptNewPropValuePredicate[nameof(vm.StateFlags)] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return (UInt32)0;
                }
                if (!UInt32.TryParse(nStr.ToUpperInvariant(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out UInt32 result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate[nameof(vm.SelectedFlag)] = AcceptNewPropValuePredicate[nameof(vm.StateFlags)];
            AcceptNewPropValuePredicate[nameof(vm.SelectedFloat)] = AcceptNewPropValuePredicate["X"];
            AcceptNewPropValuePredicate[nameof(vm.SelectedInt)] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return (UInt32)0;
                }
                if (!UInt32.TryParse(nStr, out UInt32 result)) return null;
                return result;
            };
        }

        private void IntPropsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (IntPropsList.SelectedIndex == -1) return;

            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteIntParamCommand.Index = IntPropsList.SelectedIndex;
            vm.IntIndex = IntPropsList.SelectedIndex;
        }

        private void FlagPropsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (FlagPropsList.SelectedIndex == -1) return;

            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteFlagParamCommand.Index = FlagPropsList.SelectedIndex;
            vm.FlagIndex = FlagPropsList.SelectedIndex;
        }

        private void FloatPropsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (FloatPropsList.SelectedIndex == -1) return;

            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteFloatParamCommand.Index = FloatPropsList.SelectedIndex;
            vm.FloatIndex = FloatPropsList.SelectedIndex;
        }

        private void UpdateAddInstanceMenu()
        {
            if (addInstance != null)
            {
                LinkedInstancesContextMenu.Items.Remove(addInstance);
            }
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var tree = chunkEditor!.ChunkTree;
            var instances = tree.Find(avm => avm.Alias == "Instances");
            var vm = (ObjectInstanceViewModel)AssetViewModel;
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
                            var index = vm.Instances.Count;
                            var rc = new RelayCommand(new GenerateCommand(() =>
                            {
                                vm.Instances.Add((UInt16)inst.Asset.ID);
                            },
                            () =>
                            {
                                vm.Instances.RemoveAt(index);
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
                LinkedInstancesContextMenu.Items.Insert(0, addInstance);
            }
        }
        private void UpdateAddPathMenu()
        {
            if (addPath != null)
            {
                LinkedPathsContextMenu.Items.Remove(addPath);
            }
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var tree = chunkEditor!.ChunkTree;
            var paths = tree.Find(avm => avm.Alias == "Paths");
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            var fitPaths = paths!.Children.Where((i) =>
            {
                return i.Asset.LayoutID == (int)vm.LayoutID && !vm.Paths.Contains((UInt16)i.Asset.ID);
            }).ToList();
            if (fitPaths.Any())
            {
                var pathsMenuItem = new MenuItem[fitPaths.Count];
                for (var i = 0; i < fitPaths.Count; ++i)
                {
                    var path = (PathViewModel)fitPaths[i]!;
                    pathsMenuItem[i] = new MenuItem
                    {
                        Header = path.Alias,
                        Command = new GenerateCommand(() =>
                        {
                            var index = vm.Paths.Count;
                            var rc = new RelayCommand(new GenerateCommand(() =>
                            {
                                vm.Paths.Add((UInt16)path.Asset.ID);
                            },
                            () =>
                            {
                                vm.Paths.RemoveAt(index);
                            }), CommandManager);
                            rc.Execute();
                        })
                    };
                }
                addPath = new MenuItem
                {
                    Header = "Add",
                    ItemsSource = pathsMenuItem,
                };
                LinkedPathsContextMenu.Items.Insert(0, addPath);
            }
        }
        private void UpdateAddPositionMenu()
        {
            if (addPosition != null)
            {
                LinkedPositionsContextMenu.Items.Remove(addPosition);
            }
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var tree = chunkEditor!.ChunkTree;
            var positions = tree.Find(avm => avm.Alias == "Positions");
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            var fitPositions = positions!.Children.Where((i) =>
            {
                return i.Asset.LayoutID == (int)vm.LayoutID && !vm.Positions.Contains((UInt16)i.Asset.ID);
            }).ToList();
            if (fitPositions.Any())
            {
                var posMenuItem = new MenuItem[fitPositions.Count];
                for (var i = 0; i < fitPositions.Count; ++i)
                {
                    var pos = (PositionViewModel)fitPositions[i]!;
                    posMenuItem[i] = new MenuItem
                    {
                        Header = pos.Alias,
                        Command = new GenerateCommand(() =>
                        {
                            var index = vm.Positions.Count;
                            var rc = new RelayCommand(new GenerateCommand(() =>
                            {
                                vm.Positions.Add((UInt16)pos.Asset.ID);
                            },
                            () =>
                            {
                                vm.Positions.RemoveAt(index);
                            }), CommandManager);
                            rc.Execute();
                        })
                    };
                }
                addPosition = new MenuItem
                {
                    Header = "Add",
                    ItemsSource = posMenuItem,
                };
                LinkedPositionsContextMenu.Items.Insert(0, addPosition);
            }
        }

        private void LinkedInstancesList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteLinkedInstanceCommand.Index = LinkedInstancesList.SelectedIndex;
        }

        private void LinkedPositionsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteLinkedPositionCommand.Index = LinkedPositionsList.SelectedIndex;
        }

        private void LinkedPathsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteLinkedPathCommand.Index = LinkedPathsList.SelectedIndex;
        }
    }
}
