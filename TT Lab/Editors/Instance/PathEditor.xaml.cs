﻿using System;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for PathEditor.xaml
    /// </summary>
    public partial class PathEditor : BaseEditor
    {
        public PathEditor()
        {
            InitializeComponent();
        }

        public PathEditor(PathViewModel pvm, CommandManager comManager) : base(pvm, comManager)
        {
            InitializeComponent();
            InitValidators();
            Loaded += PathEditor_Loaded;
        }

        private void PathEditor_Loaded(Object sender, RoutedEventArgs e)
        {
            PointsList.SelectedIndex = 0;
            ArgumentsList.SelectedIndex = 0;
            PointsList.Focus();
        }

        private void InitValidators()
        {
            foreach (var pair in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(pair.Key, pair.Value);
            }
        }

        private void PointsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (PointsList.SelectedItem == null) return;

            var vm = GetViewModel<PathViewModel>();
            vm.DeletePointCommand.Index = PointsList.SelectedIndex;
            CoordEditor.VectorComponentsAmount = 4;
            CoordEditor.PropertyTarget = PointsList.SelectedItem;
        }

        private void ArgumentsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (ArgumentsList.SelectedItem == null) return;

            var vm = GetViewModel<PathViewModel>();
            vm.DeleteArgumentCommand.Index = ArgumentsList.SelectedIndex;
            CoordEditor.VectorComponentsAmount = 2;
            CoordEditor.PropertyTarget = ArgumentsList.SelectedItem;
        }

        private void PointsList_GotFocus(Object sender, RoutedEventArgs e)
        {
            PointsList_SelectionChanged(sender, null);
        }

        private void ArgumentsList_GotFocus(Object sender, RoutedEventArgs e)
        {
            ArgumentsList_SelectionChanged(sender, null);
        }
    }
}
