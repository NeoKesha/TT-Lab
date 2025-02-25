using org.ogre;

namespace TT_Lab.Rendering.Objects;

public class TextDisplay
{
    private MovableObject _movableObject;
    private Camera _camera;
    private bool _enabled = false;
    private string _text = "";
    private static Overlay? _overlay = null;
    private OverlayContainer _overlayContainer;
    private OverlayElement _overlayText;
    
    public TextDisplay(MovableObject movableObject, Camera camera)
    {
        _movableObject = movableObject;
        _camera = camera;

        _overlay ??= OverlayManager.getSingleton().create($"BaseOverlay");

        _overlayContainer = OverlayManager.getSingleton().createOverlayElement("Panel", $"container1_{GetHashCode()}").castOverlayContainer();
        _overlay.add2D(_overlayContainer);

        _overlayText = OverlayManager.getSingleton().createOverlayElement("TextArea", $"textDisplayElement_{GetHashCode()}");
        _overlayText.setDimensions(1.0f, 1.0f);
        _overlayText.setMetricsMode(GuiMetricsMode.GMM_PIXELS);
        _overlayText.setPosition(0.0f, 0.0f);
        
        FontManager.getSingleton().getByName("InterDisplay").load();
        _overlayText.setParameter("font_name", "InterDisplay");
        _overlayText.setParameter("char_height", "32");
        _overlayText.setParameter("horz_align", "center");
        _overlayText.setColour(ColourValue.White);
        
        _overlayContainer.addChild(_overlayText);
        _overlayContainer.setPosition(0.5f, 0.5f);
        _overlay.show();
    }

    public void Enable(bool enabled)
    {
        _enabled = enabled;
        if (enabled)
        {
            _overlayContainer.show();
        }
        else
        {
            _overlayContainer.hide();
        }
    }

    public void SetText(string text)
    {
        _text = text;
        _overlayText.setCaption(text);
    }

    public void Update()
    {
        if (!_enabled)
        {
            return;
        }
        
        // var bbox = _movableObject.getWorldBoundingBox(true);
        // var topCenter = bbox.getCenter();
        // topCenter = _camera.getProjectionMatrix().__mul__(_camera.getViewMatrix().__mul__(topCenter));
        // var isBehindCamera = topCenter.z < 0.0;
        //
        // if (isBehindCamera)
        // {
        //     _overlayContainer.setPosition(0.5f, 0.5f);
        // }
        // else
        // {
        //     _overlayContainer.setPosition(0.5f, 0.5f);//(1.0f - ((topCenter.x * 0.5f) + 0.5f), ((topCenter.y * 0.5f) + 0.5f));
        //     _overlayContainer.setDimensions(1.0f, 1.0f);
        // }
        
    }
}