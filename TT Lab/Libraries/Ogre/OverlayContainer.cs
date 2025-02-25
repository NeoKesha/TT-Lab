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

public class OverlayContainer : OverlayElement {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal OverlayContainer(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgreOverlayPINVOKE.OverlayContainer_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OverlayContainer obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(OverlayContainer obj) {
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
          OgreOverlayPINVOKE.delete_OverlayContainer(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public virtual void addChild(OverlayElement elem) {
    OgreOverlayPINVOKE.OverlayContainer_addChild(swigCPtr, OverlayElement.getCPtr(elem));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void addChildImpl(OverlayElement elem) {
    OgreOverlayPINVOKE.OverlayContainer_addChildImpl__SWIG_0(swigCPtr, OverlayElement.getCPtr(elem));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void addChildImpl(OverlayContainer cont) {
    OgreOverlayPINVOKE.OverlayContainer_addChildImpl__SWIG_1(swigCPtr, OverlayContainer.getCPtr(cont));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void removeChild(string name) {
    OgreOverlayPINVOKE.OverlayContainer_removeChild(swigCPtr, name);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual OverlayElement getChild(string name) {
    global::System.IntPtr cPtr = OgreOverlayPINVOKE.OverlayContainer_getChild(swigCPtr, name);
    OverlayElement ret = (cPtr == global::System.IntPtr.Zero) ? null : new OverlayElement(cPtr, false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void initialise() {
    OgreOverlayPINVOKE.OverlayContainer_initialise(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void _addChild(OverlayElement elem) {
    OgreOverlayPINVOKE.OverlayContainer__addChild(swigCPtr, OverlayElement.getCPtr(elem));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void _removeChild(OverlayElement elem) {
    OgreOverlayPINVOKE.OverlayContainer__removeChild__SWIG_0(swigCPtr, OverlayElement.getCPtr(elem));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void _removeChild(string name) {
    OgreOverlayPINVOKE.OverlayContainer__removeChild__SWIG_1(swigCPtr, name);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__mapT_std__string_Ogre__OverlayElement_p_std__lessT_std__string_t_t getChildren() {
    SWIGTYPE_p_std__mapT_std__string_Ogre__OverlayElement_p_std__lessT_std__string_t_t ret = new SWIGTYPE_p_std__mapT_std__string_Ogre__OverlayElement_p_std__lessT_std__string_t_t(OgreOverlayPINVOKE.OverlayContainer_getChildren(swigCPtr), false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void _positionsOutOfDate() {
    OgreOverlayPINVOKE.OverlayContainer__positionsOutOfDate(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void _update() {
    OgreOverlayPINVOKE.OverlayContainer__update(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public override ushort _notifyZOrder(ushort newZOrder) {
    ushort ret = OgreOverlayPINVOKE.OverlayContainer__notifyZOrder(swigCPtr, newZOrder);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void _notifyViewport() {
    OgreOverlayPINVOKE.OverlayContainer__notifyViewport(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void _notifyWorldTransforms(Matrix4 xform) {
    OgreOverlayPINVOKE.OverlayContainer__notifyWorldTransforms(swigCPtr, Matrix4.getCPtr(xform));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void _notifyParent(OverlayContainer parent, Overlay overlay) {
    OgreOverlayPINVOKE.OverlayContainer__notifyParent(swigCPtr, OverlayContainer.getCPtr(parent), Overlay.getCPtr(overlay));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public void _updateRenderQueue(RenderQueue queue) {
    OgreOverlayPINVOKE.OverlayContainer__updateRenderQueue(swigCPtr, RenderQueue.getCPtr(queue));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public override bool isContainer() {
    bool ret = OgreOverlayPINVOKE.OverlayContainer_isContainer(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool isChildrenProcessEvents() {
    bool ret = OgreOverlayPINVOKE.OverlayContainer_isChildrenProcessEvents(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void setChildrenProcessEvents(bool val) {
    OgreOverlayPINVOKE.OverlayContainer_setChildrenProcessEvents(swigCPtr, val);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public override OverlayElement findElementAt(float x, float y) {
    global::System.IntPtr cPtr = OgreOverlayPINVOKE.OverlayContainer_findElementAt(swigCPtr, x, y);
    OverlayElement ret = (cPtr == global::System.IntPtr.Zero) ? null : new OverlayElement(cPtr, false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void copyFromTemplate(OverlayElement templateOverlay) {
    OgreOverlayPINVOKE.OverlayContainer_copyFromTemplate(swigCPtr, OverlayElement.getCPtr(templateOverlay));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public override OverlayElement clone(string instanceName) {
    global::System.IntPtr cPtr = OgreOverlayPINVOKE.OverlayContainer_clone(swigCPtr, instanceName);
    OverlayElement ret = (cPtr == global::System.IntPtr.Zero) ? null : new OverlayElement(cPtr, false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
