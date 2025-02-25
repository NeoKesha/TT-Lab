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

public class DefaultShadowCameraSetup : ShadowCameraSetup {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal DefaultShadowCameraSetup(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.DefaultShadowCameraSetup_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DefaultShadowCameraSetup obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(DefaultShadowCameraSetup obj) {
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
          OgrePINVOKE.delete_DefaultShadowCameraSetup(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public DefaultShadowCameraSetup() : this(OgrePINVOKE.new_DefaultShadowCameraSetup(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static ShadowCameraSetupPtr create() {
    ShadowCameraSetupPtr ret = new ShadowCameraSetupPtr(OgrePINVOKE.DefaultShadowCameraSetup_create(), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void getShadowCamera(SceneManager sm, Camera cam, Viewport vp, Light light, Camera texCam, uint iteration) {
    OgrePINVOKE.DefaultShadowCameraSetup_getShadowCamera(swigCPtr, SceneManager.getCPtr(sm), Camera.getCPtr(cam), Viewport.getCPtr(vp), Light.getCPtr(light), Camera.getCPtr(texCam), iteration);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
