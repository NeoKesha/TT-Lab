using Caliburn.Micro;
using org.ogre;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Attributes;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class TextureViewModel : ResourceEditorViewModel
    {
        private Bitmap? _texture;
        private ITwinTexture.TextureFunction _texFun;
        private ITwinTexture.TexturePixelFormat _pixelFormat;
        private Boolean _generateMipmaps;
        private SceneEditorViewModel _textureViewer;

        static private ObservableCollection<object> _textureFunctions;
        static private ObservableCollection<object> _pixelFormats;

        static TextureViewModel()
        {
            _textureFunctions = new ObservableCollection<object>(Enum.GetValues(typeof(ITwinTexture.TextureFunction)).Cast<object>());
            _pixelFormats = new ObservableCollection<object>(Enum.GetValues(typeof(ITwinTexture.TexturePixelFormat)).Cast<object>());
        }

        public TextureViewModel()
        {
            _textureViewer = IoC.Get<SceneEditorViewModel>();
            _textureViewer.SceneHeaderModel = "Texture viewer";
            Scenes.Add(_textureViewer);

            InitTextureViewer();
        }

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset<Assets.Graphics.Texture>(EditableResource);
            var data = (TextureData)asset.GetData();
            data.Bitmap = Texture;
            asset.PixelFormat = PixelStorageFormat;
            asset.TextureFunction = TextureFunction;
            asset.GenerateMipmaps = GenerateMipmaps;
            
            base.Save();
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset<Assets.Graphics.Texture>(EditableResource);
            _pixelFormat = asset.PixelFormat;
            _texFun = asset.TextureFunction;
            _generateMipmaps = asset.GenerateMipmaps;
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            _texture?.Dispose();
            _texture = null;

            return base.OnDeactivateAsync(close, cancellationToken);
        }

        private void InitTextureViewer()
        {
            TextureViewer.SceneCreator = glControl =>
            {
                var sceneManager = glControl.GetSceneManager();
                var pivot = sceneManager.getRootSceneNode().createChildSceneNode();
                pivot.setPosition(0, 0, 0);
                glControl.SetCameraTarget(pivot);
                glControl.SetCameraStyle(CameraStyle.CS_ORBIT);

                var plane = sceneManager.getRootSceneNode().createChildSceneNode();
                var entity = sceneManager.createEntity(BufferGeneration.GetPlaneBuffer());
                Rendering.MaterialManager.CreateOrGetMaterial("DiffuseTexture", out var material);
                Rendering.MaterialManager.SetupMaterialPlainTexture(material, EditableResource);
                entity.setMaterial(material);
                entity.getSubEntity(0).setCustomParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                plane.attachObject(entity);
                plane.scale(0.05f, 0.05f, 1f);
            };
        }

        public void ReplaceButton()
        {
            var file = MiscUtils.GetFileFromDialogue("Image file|*.jpg;*.png;*.bmp");
            TextureViewerFileDrop(new Controls.FileDropEventArgs { File = file });
        }

        public void TextureViewerDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var file = (string[])e.Data.GetData(DataFormats.FileDrop);
                TextureViewerFileDrop(new Controls.FileDropEventArgs { File = file[0] });
            }
            else if (e.Data.GetDataPresent(typeof(Controls.DraggedData)))
            {
                var data = (Controls.DraggedData)e.Data.GetData(typeof(Controls.DraggedData));
                TextureViewerFileDrop(new Controls.FileDropEventArgs { Data = data });
            }
            else
            {
                Log.WriteLine("Format not compatible!");
                e.Effects = DragDropEffects.None;
            }
        }

        private void TextureViewerFileDrop(Controls.FileDropEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.File))
            {
                Bitmap image = new(e.File);
                if (image.Width > 256 || image.Height > 256 || !MathExtension.IsPowerOfTwo(image.Width) || !MathExtension.IsPowerOfTwo(image.Height)
                    || image.Width < 8 || image.Height < 8)
                {
                    Log.WriteLine(@"Image is not compatible.
                * Width and height can't exceed 256 pixels
                * Width and height have to be a power of 2
                * Width and height can't be less than 8 pixels");
                    image.Dispose();
                    return;
                }
                Texture = image;
            }
            else if (e.Data != null)
            {
                try
                {
                    var texAsset = AssetManager.Get().GetAsset((LabURI)e.Data.Data);
                    Texture = texAsset.GetData<TextureData>().Bitmap;
                    Log.WriteLine($"Replacing with texture: {texAsset.Alias}");
                }
                catch (Exception)
                {
                    Log.WriteLine($"Unsupported texture");
                }
            }
            InitTextureViewer();
        }

        public static ObservableCollection<object> TexFuns
        {
            get => _textureFunctions;
        }

        public static ObservableCollection<object> PixelFormats
        {
            get => _pixelFormats;
        }

        public SceneEditorViewModel TextureViewer
        {
            get => _textureViewer;
        }

        [MarkDirty]
        public Bitmap Texture
        {
            get
            {
                var asset = AssetManager.Get().GetAsset(EditableResource);
                _texture ??= (Bitmap)asset.GetData<TextureData>().Bitmap.Clone();
                return _texture;
            }
            set =>
                //_texture?.Dispose();
                _texture = (Bitmap)value.Clone();
        }

        [MarkDirty]
        public ITwinTexture.TextureFunction TextureFunction
        {
            get
            {
                return _texFun;
            }
            set
            {
                if (value != _texFun)
                {
                    _texFun = value;
                    
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public ITwinTexture.TexturePixelFormat PixelStorageFormat
        {
            get
            {
                return _pixelFormat;
            }
            set
            {
                if (value != _pixelFormat)
                {
                    _pixelFormat = value;
                    
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Boolean GenerateMipmaps
        {
            get
            {
                return _generateMipmaps;
            }

            set
            {
                if (value != _generateMipmaps)
                {
                    _generateMipmaps = value;
                    
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
