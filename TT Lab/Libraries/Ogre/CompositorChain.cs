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

public class CompositorChain : RenderTargetListener {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal CompositorChain(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.CompositorChain_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CompositorChain obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(CompositorChain obj) {
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
          OgrePINVOKE.delete_CompositorChain(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public CompositorChain(Viewport vp) : this(OgrePINVOKE.new_CompositorChain(Viewport.getCPtr(vp)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public CompositorInstance addCompositor(CompositorPtr filter, uint addPosition, string scheme) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_addCompositor__SWIG_0(swigCPtr, CompositorPtr.getCPtr(filter), addPosition, scheme);
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositorInstance addCompositor(CompositorPtr filter, uint addPosition) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_addCompositor__SWIG_1(swigCPtr, CompositorPtr.getCPtr(filter), addPosition);
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositorInstance addCompositor(CompositorPtr filter) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_addCompositor__SWIG_2(swigCPtr, CompositorPtr.getCPtr(filter));
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void removeCompositor(uint position) {
    OgrePINVOKE.CompositorChain_removeCompositor__SWIG_0(swigCPtr, position);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeCompositor() {
    OgrePINVOKE.CompositorChain_removeCompositor__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeAllCompositors() {
    OgrePINVOKE.CompositorChain_removeAllCompositors(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public CompositorInstance getCompositor(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_getCompositor(swigCPtr, name);
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getCompositorPosition(string name) {
    uint ret = OgrePINVOKE.CompositorChain_getCompositorPosition(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositorInstance _getOriginalSceneCompositor() {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain__getOriginalSceneCompositor(swigCPtr);
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositorInstanceList getCompositorInstances() {
    CompositorInstanceList ret = new CompositorInstanceList(OgrePINVOKE.CompositorChain_getCompositorInstances(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setCompositorEnabled(uint position, bool state) {
    OgrePINVOKE.CompositorChain_setCompositorEnabled(swigCPtr, position, state);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void preRenderTargetUpdate(RenderTargetEvent evt) {
    OgrePINVOKE.CompositorChain_preRenderTargetUpdate(swigCPtr, RenderTargetEvent.getCPtr(evt));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void postRenderTargetUpdate(RenderTargetEvent evt) {
    OgrePINVOKE.CompositorChain_postRenderTargetUpdate(swigCPtr, RenderTargetEvent.getCPtr(evt));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void preViewportUpdate(RenderTargetViewportEvent evt) {
    OgrePINVOKE.CompositorChain_preViewportUpdate(swigCPtr, RenderTargetViewportEvent.getCPtr(evt));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void postViewportUpdate(RenderTargetViewportEvent evt) {
    OgrePINVOKE.CompositorChain_postViewportUpdate(swigCPtr, RenderTargetViewportEvent.getCPtr(evt));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void viewportCameraChanged(Viewport viewport) {
    OgrePINVOKE.CompositorChain_viewportCameraChanged(swigCPtr, Viewport.getCPtr(viewport));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void viewportDimensionsChanged(Viewport viewport) {
    OgrePINVOKE.CompositorChain_viewportDimensionsChanged(swigCPtr, Viewport.getCPtr(viewport));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void viewportDestroyed(Viewport viewport) {
    OgrePINVOKE.CompositorChain_viewportDestroyed(swigCPtr, Viewport.getCPtr(viewport));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _markDirty() {
    OgrePINVOKE.CompositorChain__markDirty(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Viewport getViewport() {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_getViewport(swigCPtr);
    Viewport ret = (cPtr == global::System.IntPtr.Zero) ? null : new Viewport(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _notifyViewport(Viewport vp) {
    OgrePINVOKE.CompositorChain__notifyViewport(swigCPtr, Viewport.getCPtr(vp));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _removeInstance(CompositorInstance i) {
    OgrePINVOKE.CompositorChain__removeInstance(swigCPtr, CompositorInstance.getCPtr(i));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _queuedOperation(CompositorInstance.RenderSystemOperation op) {
    OgrePINVOKE.CompositorChain__queuedOperation(swigCPtr, CompositorInstance.RenderSystemOperation.getCPtr(op));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _compile() {
    OgrePINVOKE.CompositorChain__compile(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public CompositorInstance getPreviousInstance(CompositorInstance curr, bool activeOnly) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_getPreviousInstance__SWIG_0(swigCPtr, CompositorInstance.getCPtr(curr), activeOnly);
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositorInstance getPreviousInstance(CompositorInstance curr) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_getPreviousInstance__SWIG_1(swigCPtr, CompositorInstance.getCPtr(curr));
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositorInstance getNextInstance(CompositorInstance curr, bool activeOnly) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_getNextInstance__SWIG_0(swigCPtr, CompositorInstance.getCPtr(curr), activeOnly);
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositorInstance getNextInstance(CompositorInstance curr) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositorChain_getNextInstance__SWIG_1(swigCPtr, CompositorInstance.getCPtr(curr));
    CompositorInstance ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositorInstance(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static readonly int BEST = OgrePINVOKE.CompositorChain_BEST_get();
  public static readonly int LAST = OgrePINVOKE.CompositorChain_LAST_get();
  public static readonly int NPOS = OgrePINVOKE.CompositorChain_NPOS_get();

}

}
