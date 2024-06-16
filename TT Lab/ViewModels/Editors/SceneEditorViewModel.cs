using Caliburn.Micro;
using GlmSharp;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TT_Lab.Controls;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;
using TT_Lab.Views.Editors;

namespace TT_Lab.ViewModels.Editors
{
    public class SceneEditorViewModel : Conductor<object>
    {
        private GLWindow? _glControl;
        private EmbededRender? _render;
        private Scene? _scene;
        private Point mousePos;

        public event EventHandler<FileDropEventArgs>? FileDrop;

        public Scene? Scene
        {
            get => _scene;
        }

        public EmbededRender? RenderControl
        {
            get => _render;
            private set
            {
                _render = value;
            }
        }

        public Func<GLWindow, Scene>? SceneCreator
        {
            private get;
            set;
        }

        public SceneEditorViewModel()
        {
        }

        protected override Task OnDeactivateAsync(System.Boolean close, CancellationToken cancellationToken)
        {
            if (close && _glControl != null)
            {
                _glControl.Cleanup();
                _glControl = null;
                _scene = null;
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public void DragDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                FileDrop?.Invoke(this, new FileDropEventArgs { File = files[0] });
            }
            else if (e.Data.GetDataPresent(typeof(DraggedData)))
            {
                var data = e.Data.GetData(typeof(DraggedData)) as DraggedData;
                FileDrop?.Invoke(this, new FileDropEventArgs { Data = data });
            }
        }

        public void DragEntered(System.Boolean allowDrop, DragEventArgs e)
        {
            if (allowDrop)
            {
                e.Effects = DragDropEffects.All;
            }
        }

        public void MouseWheelMoved(MouseWheelEventArgs e)
        {
            Scene?.ZoomView(e.Delta);
        }

        public void MouseMoved(MouseEventArgs e)
        {
            var curMousePos = e.GetPosition(_render);
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                _glControl?.RotateView(new vec2((float)(curMousePos.X - mousePos.X), (float)(mousePos.Y - curMousePos.Y)));
            }
            mousePos = curMousePos;
        }

        public void RendererInitialized(EmbededRender embededRender)
        {
            if (_glControl == null)
            {
                _glControl = embededRender.GetRenderWindow();

                if (SceneCreator == null)
                {
                    Debug.WriteLine("WARNING: SceneCreator is not set! NO SCENE WILL BE RENDERED");
                }

                if (_glControl != null && SceneCreator != null)
                {
                    _glControl.CreateScene(SceneCreator);
                }
            }
        }
    }
}
