using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.Controls.Events;
using TT_Lab.Editors;

namespace TT_Lab.Controls
{
    public class BoundUserControl : UserControl
    {
        protected event EventHandler<BoundPropertyChangedEventArgs> BoundPropertyChanged;
        protected event EventHandler UndoPerformed;
        protected event EventHandler RedoPerformed;

        [Description("Editor that owns this user control"), Category("Common Properties")]
        public BaseEditor Editor
        {
            get { return (BaseEditor)GetValue(EditorProperty); }
            set { SetValue(EditorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BaseEditor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditorProperty =
            DependencyProperty.Register("Editor", typeof(BaseEditor), typeof(BoundUserControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnEditorChanged)));

        [Description("Property name of the viewmodel"), Category("Common Properties")]
        public string BoundProperty
        {
            get { return (string)GetValue(BoundPropertyProperty); }
            set { SetValue(BoundPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoundProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundPropertyProperty =
            DependencyProperty.Register("BoundProperty", typeof(string), typeof(BoundUserControl), new PropertyMetadata(string.Empty));


        [Description("Optional object that has the property such a sub viewmodel of a viewmodel"), Category("Common Properties")]
        public object PropertyTarget
        {
            get { return GetValue(PropertyTargetProperty); }
            set { SetValue(PropertyTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PropertyTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyTargetProperty =
            DependencyProperty.Register("PropertyTarget", typeof(object), typeof(BoundUserControl), new PropertyMetadata(null));

        protected void InvokeUndo()
        {
            UndoPerformed?.Invoke(this, new EventArgs());
        }

        protected void InvokeRedo()
        {
            RedoPerformed?.Invoke(this, new EventArgs());
        }

        protected void InvokePropChange(object newVal, object oldVal)
        {
            BoundPropertyChanged?.Invoke(this, new BoundPropertyChangedEventArgs { NewValue = newVal, OldValue = oldVal, PropName = BoundProperty, Target = PropertyTarget });
        }

        protected static void OnEditorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoundUserControl control = d as BoundUserControl;
            control.UndoPerformed += control.Editor.UndoExecuted;
            control.RedoPerformed += control.Editor.RedoExecuted;
            control.BoundPropertyChanged += control.Editor.BoundPropertyChanged;
        }

    }
}
