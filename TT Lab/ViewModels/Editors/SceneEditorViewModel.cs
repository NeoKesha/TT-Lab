using Caliburn.Micro;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
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
        private readonly IEventAggregator _eventAggregator;
        private GLWpfControl? _glControl;
        private Scene? _scene;
        private Point mousePos;

        public event EventHandler<FileDropEventArgs>? FileDrop;

        public Scene? Scene
        {
            get => _scene;
            set
            {
                if (_scene != value)
                {
                    _scene = value;
                    NotifyOfPropertyChange();
                }
                if (_scene != null && _glControl != null)
                {
                    _scene.RenderFramebuffer = _glControl.Framebuffer;
                    NotifyOfPropertyChange();
                }
            }
        }

        public GLWpfControl? GlControl => _glControl;

        public SceneEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        protected override Task OnDeactivateAsync(System.Boolean close, CancellationToken cancellationToken)
        {
            if (close)
            {
                Scene?.Delete();
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            base.OnActivateAsync(cancellationToken);

            Debug.Assert(_glControl != null, "Renderer was not initialized somehow");
            _eventAggregator.PublishOnUIThreadAsync(new RendererInitializedMessage(this, _glControl), cancellationToken);

            return Task.FromResult(true);
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
            var curMousePos = e.GetPosition(_glControl);
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Scene?.RotateView(new Vector2((float)(curMousePos.X - mousePos.X), (float)(mousePos.Y - curMousePos.Y)));
            }
            mousePos = curMousePos;
        }

        public void RendererInitialized(GLWpfControl glControl)
        {
            Scene?.Delete();
            _glControl = glControl;
        }

        public void RendererRender(RenderEventArgs delta)
        {
            if (_glControl == null)
            {
                return;
            }

            if (Scene != null)
            {
                Scene.RenderFramebuffer = _glControl.Framebuffer;
            }
            GL.ClearColor(Color4.LightGray);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            Scene?.PreRender();
            Scene?.Move();
            Scene?.Render();
            Scene?.PostRender();

            GL.Finish();
        }

        public void RenderSizeChanged()
        {
            if (_glControl == null)
            {
                return;
            }

            Scene?.SetResolution((float)_glControl.ActualWidth, (float)_glControl.ActualHeight);
            _eventAggregator.PublishOnUIThreadAsync(new RendererInitializedMessage(this, _glControl));
        }
    }
}
