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

public class GlyphInfo : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GlyphInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GlyphInfo obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(GlyphInfo obj) {
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

  ~GlyphInfo() {
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
          OgreOverlayPINVOKE.delete_GlyphInfo(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public uint codePoint {
    set {
      OgreOverlayPINVOKE.GlyphInfo_codePoint_set(swigCPtr, value);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      uint ret = OgreOverlayPINVOKE.GlyphInfo_codePoint_get(swigCPtr);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FloatRect uvRect {
    set {
      OgreOverlayPINVOKE.GlyphInfo_uvRect_set(swigCPtr, FloatRect.getCPtr(value));
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = OgreOverlayPINVOKE.GlyphInfo_uvRect_get(swigCPtr);
      FloatRect ret = (cPtr == global::System.IntPtr.Zero) ? null : new FloatRect(cPtr, false);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float aspectRatio {
    set {
      OgreOverlayPINVOKE.GlyphInfo_aspectRatio_set(swigCPtr, value);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = OgreOverlayPINVOKE.GlyphInfo_aspectRatio_get(swigCPtr);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float bearing {
    set {
      OgreOverlayPINVOKE.GlyphInfo_bearing_set(swigCPtr, value);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = OgreOverlayPINVOKE.GlyphInfo_bearing_get(swigCPtr);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float advance {
    set {
      OgreOverlayPINVOKE.GlyphInfo_advance_set(swigCPtr, value);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = OgreOverlayPINVOKE.GlyphInfo_advance_get(swigCPtr);
      if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public GlyphInfo() : this(OgreOverlayPINVOKE.new_GlyphInfo(), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
