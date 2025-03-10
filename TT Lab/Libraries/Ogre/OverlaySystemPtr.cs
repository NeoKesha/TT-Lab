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

public class OverlaySystemPtr : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal OverlaySystemPtr(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OverlaySystemPtr obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(OverlaySystemPtr obj) {
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

  ~OverlaySystemPtr() {
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
          OgreOverlayPINVOKE.delete_OverlaySystemPtr(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public OverlaySystemPtr(SWIGTYPE_p_std__nullptr_t arg0) : this(OgreOverlayPINVOKE.new_OverlaySystemPtr__SWIG_0(SWIGTYPE_p_std__nullptr_t.getCPtr(arg0)), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public OverlaySystemPtr() : this(OgreOverlayPINVOKE.new_OverlaySystemPtr__SWIG_1(), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public OverlaySystemPtr(OverlaySystemPtr r) : this(OgreOverlayPINVOKE.new_OverlaySystemPtr__SWIG_2(OverlaySystemPtr.getCPtr(r)), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public OverlaySystem __deref__() {
    global::System.IntPtr cPtr = OgreOverlayPINVOKE.OverlaySystemPtr___deref__(swigCPtr);
    OverlaySystem ret = (cPtr == global::System.IntPtr.Zero) ? null : new OverlaySystem(cPtr, false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void renderQueueStarted(byte queueGroupId, string cameraName, SWIGTYPE_p_bool skipThisInvocation) {
    OgreOverlayPINVOKE.OverlaySystemPtr_renderQueueStarted(swigCPtr, queueGroupId, cameraName, SWIGTYPE_p_bool.getCPtr(skipThisInvocation));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void eventOccurred(string eventName, NameValueMap parameters) {
    OgreOverlayPINVOKE.OverlaySystemPtr_eventOccurred(swigCPtr, eventName, NameValueMap.getCPtr(parameters));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public OverlaySystem getSingleton() {
    OverlaySystem ret = new OverlaySystem(OgreOverlayPINVOKE.OverlaySystemPtr_getSingleton(swigCPtr), false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void preRenderQueues() {
    OgreOverlayPINVOKE.OverlaySystemPtr_preRenderQueues(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void postRenderQueues() {
    OgreOverlayPINVOKE.OverlaySystemPtr_postRenderQueues(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void renderQueueEnded(byte queueGroupId, string cameraName, SWIGTYPE_p_bool repeatThisInvocation) {
    OgreOverlayPINVOKE.OverlaySystemPtr_renderQueueEnded(swigCPtr, queueGroupId, cameraName, SWIGTYPE_p_bool.getCPtr(repeatThisInvocation));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
