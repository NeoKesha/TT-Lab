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

public class FontManager : ResourceManager {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FontManager(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgreOverlayPINVOKE.FontManager_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FontManager obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(FontManager obj) {
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
          OgreOverlayPINVOKE.delete_FontManager(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public FontManager() : this(OgreOverlayPINVOKE.new_FontManager(), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public FontPtr create(string name, string group, bool isManual, ManualResourceLoader loader, NameValueMap createParams) {
    FontPtr ret = new FontPtr(OgreOverlayPINVOKE.FontManager_create__SWIG_0(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(createParams)), true);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FontPtr create(string name, string group, bool isManual, ManualResourceLoader loader) {
    FontPtr ret = new FontPtr(OgreOverlayPINVOKE.FontManager_create__SWIG_1(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader)), true);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FontPtr create(string name, string group, bool isManual) {
    FontPtr ret = new FontPtr(OgreOverlayPINVOKE.FontManager_create__SWIG_2(swigCPtr, name, group, isManual), true);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FontPtr create(string name, string group) {
    FontPtr ret = new FontPtr(OgreOverlayPINVOKE.FontManager_create__SWIG_3(swigCPtr, name, group), true);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FontPtr getByName(string name, string groupName) {
    FontPtr ret = new FontPtr(OgreOverlayPINVOKE.FontManager_getByName__SWIG_0(swigCPtr, name, groupName), true);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FontPtr getByName(string name) {
    FontPtr ret = new FontPtr(OgreOverlayPINVOKE.FontManager_getByName__SWIG_1(swigCPtr, name), true);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static FontManager getSingleton() {
    FontManager ret = new FontManager(OgreOverlayPINVOKE.FontManager_getSingleton(), false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
