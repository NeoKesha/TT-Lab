//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace org.ogre {

public class Font : Resource {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal Font(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgreOverlayPINVOKE.Font_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Font obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Font obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          OgreOverlayPINVOKE.delete_Font(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public Font(ResourceManager creator, string name, uint handle, string group, bool isManual, ManualResourceLoader loader) : this(OgreOverlayPINVOKE.new_Font__SWIG_0(ResourceManager.getCPtr(creator), name, handle, group, isManual, ManualResourceLoader.getCPtr(loader)), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public Font(ResourceManager creator, string name, uint handle, string group, bool isManual) : this(OgreOverlayPINVOKE.new_Font__SWIG_1(ResourceManager.getCPtr(creator), name, handle, group, isManual), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public Font(ResourceManager creator, string name, uint handle, string group) : this(OgreOverlayPINVOKE.new_Font__SWIG_2(ResourceManager.getCPtr(creator), name, handle, group), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setType(FontType ftype) {
    OgreOverlayPINVOKE.Font_setType(swigCPtr, (int)ftype);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public FontType getType() {
    FontType ret = (FontType)OgreOverlayPINVOKE.Font_getType(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setSource(string source) {
    OgreOverlayPINVOKE.Font_setSource(swigCPtr, source);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getSource() {
    string ret = OgreOverlayPINVOKE.Font_getSource(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setTrueTypeSize(float ttfSize) {
    OgreOverlayPINVOKE.Font_setTrueTypeSize(swigCPtr, ttfSize);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setTrueTypeResolution(uint ttfResolution) {
    OgreOverlayPINVOKE.Font_setTrueTypeResolution(swigCPtr, ttfResolution);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public float getTrueTypeSize() {
    float ret = OgreOverlayPINVOKE.Font_getTrueTypeSize(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getTrueTypeResolution() {
    uint ret = OgreOverlayPINVOKE.Font_getTrueTypeResolution(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int getTrueTypeMaxBearingY() {
    int ret = OgreOverlayPINVOKE.Font_getTrueTypeMaxBearingY(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FloatRect getGlyphTexCoords(uint id) {
    FloatRect ret = new FloatRect(OgreOverlayPINVOKE.Font_getGlyphTexCoords(swigCPtr, id), false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setGlyphInfoFromTexCoords(uint id, FloatRect rect, float textureAspect) {
    OgreOverlayPINVOKE.Font_setGlyphInfoFromTexCoords__SWIG_0(swigCPtr, id, FloatRect.getCPtr(rect), textureAspect);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setGlyphInfoFromTexCoords(uint id, FloatRect rect) {
    OgreOverlayPINVOKE.Font_setGlyphInfoFromTexCoords__SWIG_1(swigCPtr, id, FloatRect.getCPtr(rect));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setGlyphInfo(GlyphInfo info) {
    OgreOverlayPINVOKE.Font_setGlyphInfo(swigCPtr, GlyphInfo.getCPtr(info));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public float getGlyphAspectRatio(uint id) {
    float ret = OgreOverlayPINVOKE.Font_getGlyphAspectRatio(swigCPtr, id);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setGlyphAspectRatio(uint id, float ratio) {
    OgreOverlayPINVOKE.Font_setGlyphAspectRatio(swigCPtr, id, ratio);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public GlyphInfo getGlyphInfo(uint id) {
    GlyphInfo ret = new GlyphInfo(OgreOverlayPINVOKE.Font_getGlyphInfo(swigCPtr, id), false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void addCodePointRange(SWIGTYPE_p_std__pairT_unsigned_int_unsigned_int_t range) {
    OgreOverlayPINVOKE.Font_addCodePointRange(swigCPtr, SWIGTYPE_p_std__pairT_unsigned_int_unsigned_int_t.getCPtr(range));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void clearCodePointRanges() {
    OgreOverlayPINVOKE.Font_clearCodePointRanges(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__vectorT_std__pairT_unsigned_int_unsigned_int_t_t getCodePointRangeList() {
    SWIGTYPE_p_std__vectorT_std__pairT_unsigned_int_unsigned_int_t_t ret = new SWIGTYPE_p_std__vectorT_std__pairT_unsigned_int_unsigned_int_t_t(OgreOverlayPINVOKE.Font_getCodePointRangeList(swigCPtr), false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public MaterialPtr getMaterial() {
    MaterialPtr ret = new MaterialPtr(OgreOverlayPINVOKE.Font_getMaterial(swigCPtr), false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void putText(BillboardSet bbs, string text, float height, ColourValue colour) {
    OgreOverlayPINVOKE.Font_putText__SWIG_0(swigCPtr, BillboardSet.getCPtr(bbs), text, height, ColourValue.getCPtr(colour));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void putText(BillboardSet bbs, string text, float height) {
    OgreOverlayPINVOKE.Font_putText__SWIG_1(swigCPtr, BillboardSet.getCPtr(bbs), text, height);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setAntialiasColour(bool enabled) {
    OgreOverlayPINVOKE.Font_setAntialiasColour(swigCPtr, enabled);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getAntialiasColour() {
    bool ret = OgreOverlayPINVOKE.Font_getAntialiasColour(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void loadResource(Resource resource) {
    OgreOverlayPINVOKE.Font_loadResource(swigCPtr, Resource.getCPtr(resource));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void _setMaterial(MaterialPtr mat) {
    OgreOverlayPINVOKE.Font__setMaterial(swigCPtr, MaterialPtr.getCPtr(mat));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
