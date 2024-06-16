using GlmSharp;
using SharpGL;
using SharpGL.WPF;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using TT_Lab.Controls;
using TT_Lab.Extensions;
using TT_Lab.Libraries;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Renderers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;

namespace TT_Lab.Rendering
{
    /// <summary>
    /// Renders a transparent OpenTK window as a child of another window
    /// </summary>
    public partial class GLWindow
    {
        private OpenGL GL;

        // Window's position
        private double xPos;
        private double yPos;

        private readonly ConcurrentQueue<Action> commands = new();

        private Func<GLWindow, Scene>? sceneCreator;
        private ShaderStorage shaders = new();
        private IRenderer? renderer;
        private readonly PrimitiveRenderer primitiveRenderer = new();
        private Scene? scene;

        private ShaderStorage.LibraryFragmentShaders fragmentLibraryShader;
        private ShaderStorage.LibraryVertexShaders vertexLibraryShader;

        private readonly List<IRenderable> renderedObjects = new();
        private TextureBuffer colorTextureNT;
        private FrameBuffer framebufferNT;
        private RenderBuffer depthRenderbuffer;
        private EmbededRender renderControl;

        public IRenderer? Renderer { get => renderer; }
        public OpenGL RenderContext { get => GL; }
        public EmbededRender RenderControl { get => renderControl; }

        public static GLWindow CreateAndRun(EmbededRender render)
        {
            return new(render);
        }

        private GLWindow(EmbededRender render)
        {
            renderControl = render;
            renderControl.OpenGLDraw += OnRenderFrame;
            renderControl.OpenGLInitialized += OnLoad;
            Preferences.PreferenceChanged += Preferences_PreferenceChanged;
        }

        private void Preferences_PreferenceChanged(Object? sender, Preferences.PreferenceChangedArgs e)
        {
            if (e.PreferenceName == nameof(Preferences.TranslucencyMethod))
            {
                SetupRenderer();
            }
        }

        public void Cleanup()
        {
            OnUnload();
            renderControl.OpenGLDraw -= OnRenderFrame;
            renderControl.OpenGLInitialized -= OnLoad;
        }

        public void CreateScene(Func<GLWindow, Scene> sceneCreator)
        {
            this.sceneCreator = sceneCreator;
            scene = sceneCreator(this);
            renderedObjects.Add(scene);
        }

        public void SetRendererLibraries(ShaderStorage.LibraryFragmentShaders fragmentLibraryShader, ShaderStorage.LibraryVertexShaders vertexLibraryShader = ShaderStorage.LibraryVertexShaders.VertexShading)
        {
            this.fragmentLibraryShader = fragmentLibraryShader;
            this.vertexLibraryShader = vertexLibraryShader;
        }

        public void RotateView(vec2 rotation)
        {
            scene?.RotateView(rotation);
        }

        /// <summary>
        /// Executes code on the render thread
        /// </summary>
        /// <param name="action"></param>
        public void DeferToRender(Action action)
        {
            action();
        }

        public void AddItemToScene(IRenderable item)
        {
            scene?.AddChild(item);
        }

        private void OnLoad(object sender, OpenGLRoutedEventArgs args)
        {
            GL = renderControl.GL;

            colorTextureNT = new TextureBuffer(GL);
            framebufferNT = new FrameBuffer(GL);
            depthRenderbuffer = new RenderBuffer(GL);

            shaders.BuildShaderCache(GL);

            GL.ClearColor(System.Drawing.Color.LightGray);

            SetupRenderer();

            ReallocateFramebuffer((int)renderControl.Width, (int)renderControl.Height);
            framebufferNT.Bind();
            GL.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, (uint)TextureBuffer.TextureTarget.Texture2DMultisample, colorTextureNT.Buffer, 0);
            GL.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER, depthRenderbuffer.Buffer);
        }

        private void OnUnload()
        {
            // Scene is included here
            foreach (var renderable in renderedObjects)
            {
                renderable?.Delete();
            }
            renderedObjects.Clear();

            scene = null;
            shaders.ClearShaderCache();

            Preferences.PreferenceChanged -= Preferences_PreferenceChanged;
        }

        private void OnResize(vec2 e)
        {
            //GL.Viewport(0, 0, (int)e.x, (int)e.y);
            ReallocateFramebuffer((int)e.x, (int)e.y);
        }

        private void ReallocateFramebuffer(int width, int height)
        {
            colorTextureNT.Bind();
            GL.TexStorage2DMultisample((uint)TextureBuffer.TextureTarget.Texture2DMultisample, 4, (uint)TextureBuffer.PixelInternalFormat.Rgb16f, (uint)width, (uint)height, true);
            depthRenderbuffer.Bind();
            GL.RenderbufferStorageMultisampleEXT(OpenGL.GL_RENDERBUFFER, 4, OpenGL.GL_DEPTH_COMPONENT, width, height);
            renderer?.ReallocateFramebuffer(width, height);
        }

        private void OnRenderFrame(object sender, OpenGLRoutedEventArgs args)
        {
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            GL.CullFace(OpenGL.GL_FRONT_AND_BACK);

            GL.Enable(OpenGL.GL_DEPTH_TEST);
            //GL.Enable(EnableCap.Blend);
            GL.Enable(OpenGL.GL_MULTISAMPLE);
            GL.DepthMask(1);
            GL.DepthFunc(OpenGL.GL_LEQUAL);
            framebufferNT.Bind();
            float[] clearColorNT = System.Drawing.Color.LightGray.ToArray();
            float[] clearDepth = { 1f, 1f, 1f, 1f };
            GL.ClearBuffer(OpenGL.GL_COLOR, 0, clearColorNT);
            GL.ClearBuffer(OpenGL.GL_DEPTH, 0, clearDepth);
            // Render all objects
            renderer?.Render(renderedObjects);
            // TODO: Render HUD

            // Post process effects
            GL.Disable(OpenGL.GL_BLEND);
            GL.Disable(OpenGL.GL_DEPTH_TEST);
            GL.Disable(OpenGL.GL_MULTISAMPLE);
            renderer?.PostProcess();

            // Reset modes
            GL.CullFace(OpenGL.GL_BACK);
            GL.Disable(OpenGL.GL_BLEND);
            GL.Disable(OpenGL.GL_DEPTH_TEST);
            GL.Disable(OpenGL.GL_MULTISAMPLE);
        }

        private void SetupRenderer()
        {
            var method = Preferences.GetPreference<RenderSwitches.TranslucencyMethod>(Preferences.TranslucencyMethod);
            renderer?.Delete();
            renderer = method switch
            {
                RenderSwitches.TranslucencyMethod.WBOIT => new WBOITRenderer(GL, shaders, colorTextureNT, depthRenderbuffer, (int)renderControl.Width, (int)renderControl.Height, fragmentLibraryShader, vertexLibraryShader),
                _ => new BasicRenderer(GL, shaders, fragmentLibraryShader, vertexLibraryShader)
            };
        }
    }
}
