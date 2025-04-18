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

public class ImFontAtlasCustomRect : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImFontAtlasCustomRect(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImFontAtlasCustomRect obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ImFontAtlasCustomRect obj) {
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

  ~ImFontAtlasCustomRect() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ImGuiPINVOKE.delete_ImFontAtlasCustomRect(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ushort Width {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_Width_set(swigCPtr, value);
    } 
    get {
      ushort ret = ImGuiPINVOKE.ImFontAtlasCustomRect_Width_get(swigCPtr);
      return ret;
    } 
  }

  public ushort Height {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_Height_set(swigCPtr, value);
    } 
    get {
      ushort ret = ImGuiPINVOKE.ImFontAtlasCustomRect_Height_get(swigCPtr);
      return ret;
    } 
  }

  public ushort X {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_X_set(swigCPtr, value);
    } 
    get {
      ushort ret = ImGuiPINVOKE.ImFontAtlasCustomRect_X_get(swigCPtr);
      return ret;
    } 
  }

  public ushort Y {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_Y_set(swigCPtr, value);
    } 
    get {
      ushort ret = ImGuiPINVOKE.ImFontAtlasCustomRect_Y_get(swigCPtr);
      return ret;
    } 
  }

  public uint GlyphID {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_GlyphID_set(swigCPtr, value);
    } 
    get {
      uint ret = ImGuiPINVOKE.ImFontAtlasCustomRect_GlyphID_get(swigCPtr);
      return ret;
    } 
  }

  public float GlyphAdvanceX {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_GlyphAdvanceX_set(swigCPtr, value);
    } 
    get {
      float ret = ImGuiPINVOKE.ImFontAtlasCustomRect_GlyphAdvanceX_get(swigCPtr);
      return ret;
    } 
  }

  public ImVec2 GlyphOffset {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_GlyphOffset_set(swigCPtr, ImVec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImFontAtlasCustomRect_GlyphOffset_get(swigCPtr);
      ImVec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new ImVec2(cPtr, false);
      return ret;
    } 
  }

  public ImFont Font {
    set {
      ImGuiPINVOKE.ImFontAtlasCustomRect_Font_set(swigCPtr, ImFont.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImFontAtlasCustomRect_Font_get(swigCPtr);
      ImFont ret = (cPtr == global::System.IntPtr.Zero) ? null : new ImFont(cPtr, false);
      return ret;
    } 
  }

  public ImFontAtlasCustomRect() : this(ImGuiPINVOKE.new_ImFontAtlasCustomRect(), true) {
  }

  public bool IsPacked() {
    bool ret = ImGuiPINVOKE.ImFontAtlasCustomRect_IsPacked(swigCPtr);
    return ret;
  }

}

}
