using Microsoft.Xaml.Behaviors;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace TT_Lab.Triggers
{
    public class InputBindingTrigger : TriggerBase<FrameworkElement>, ICommand
    {


        public InputBinding InputBinding
        {
            get { return (InputBinding)GetValue(InputBindingProperty); }
            set { SetValue(InputBindingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InputBinding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputBindingProperty =
            DependencyProperty.Register("InputBinding", typeof(InputBinding), typeof(InputBindingTrigger), new UIPropertyMetadata(null));


        public event EventHandler? CanExecuteChanged;

        public Boolean CanExecute(Object? parameter)
        {
            return true;
        }

        public void Execute(Object? parameter)
        {
            InvokeActions(parameter);
        }

        protected override void OnAttached()
        {
            if (InputBinding != null)
            {
                InputBinding.Command = this;
                AssociatedObject.Loaded += OnAssociatedObjectLoaded;
            }
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            if (InputBinding != null)
            {
                InputBinding.Command = null;
                AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
            }
            base.OnDetaching();
        }

        private UIElement GetTopLevelElement(FrameworkElement element)
        {
            if (element.Parent == null)
            {
                return element;
            }

            var parent = element.Parent as FrameworkElement;
            Debug.Assert(parent != null);

            return GetTopLevelElement(parent);
        }

        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            var window = GetTopLevelElement(AssociatedObject);
            window.InputBindings.Add(InputBinding);
        }
    }
}
