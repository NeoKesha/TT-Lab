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

public class FocusedShadowCameraSetupPtr : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal FocusedShadowCameraSetupPtr(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FocusedShadowCameraSetupPtr obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(FocusedShadowCameraSetupPtr obj) {
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

  ~FocusedShadowCameraSetupPtr() {
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
          OgrePINVOKE.delete_FocusedShadowCameraSetupPtr(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public FocusedShadowCameraSetupPtr(SWIGTYPE_p_std__nullptr_t arg0) : this(OgrePINVOKE.new_FocusedShadowCameraSetupPtr__SWIG_0(SWIGTYPE_p_std__nullptr_t.getCPtr(arg0)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FocusedShadowCameraSetupPtr() : this(OgrePINVOKE.new_FocusedShadowCameraSetupPtr__SWIG_1(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FocusedShadowCameraSetupPtr(FocusedShadowCameraSetupPtr r) : this(OgrePINVOKE.new_FocusedShadowCameraSetupPtr__SWIG_2(FocusedShadowCameraSetupPtr.getCPtr(r)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FocusedShadowCameraSetup __deref__() {
    global::System.IntPtr cPtr = OgrePINVOKE.FocusedShadowCameraSetupPtr___deref__(swigCPtr);
    FocusedShadowCameraSetup ret = (cPtr == global::System.IntPtr.Zero) ? null : new FocusedShadowCameraSetup(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FocusedShadowCameraSetupPtr(FocusedShadowCameraSetup underlyingClass) : this(OgrePINVOKE.new_FocusedShadowCameraSetupPtr__SWIG_3(FocusedShadowCameraSetup.getCPtr(underlyingClass)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ShadowCameraSetupPtr create(bool useAggressiveRegion) {
    ShadowCameraSetupPtr ret = new ShadowCameraSetupPtr(OgrePINVOKE.FocusedShadowCameraSetupPtr_create__SWIG_0(swigCPtr, useAggressiveRegion), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ShadowCameraSetupPtr create() {
    ShadowCameraSetupPtr ret = new ShadowCameraSetupPtr(OgrePINVOKE.FocusedShadowCameraSetupPtr_create__SWIG_1(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void getShadowCamera(SceneManager sm, Camera cam, Viewport vp, Light light, Camera texCam, uint iteration) {
    OgrePINVOKE.FocusedShadowCameraSetupPtr_getShadowCamera(swigCPtr, SceneManager.getCPtr(sm), Camera.getCPtr(cam), Viewport.getCPtr(vp), Light.getCPtr(light), Camera.getCPtr(texCam), iteration);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setUseAggressiveFocusRegion(bool aggressive) {
    OgrePINVOKE.FocusedShadowCameraSetupPtr_setUseAggressiveFocusRegion(swigCPtr, aggressive);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getUseAggressiveFocusRegion() {
    bool ret = OgrePINVOKE.FocusedShadowCameraSetupPtr_getUseAggressiveFocusRegion(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
