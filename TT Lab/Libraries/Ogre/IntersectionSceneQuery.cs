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

public class IntersectionSceneQuery : SceneQuery {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IntersectionSceneQuery(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.IntersectionSceneQuery_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IntersectionSceneQuery obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IntersectionSceneQuery obj) {
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
          OgrePINVOKE.delete_IntersectionSceneQuery(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public virtual IntersectionSceneQueryResult execute() {
    IntersectionSceneQueryResult ret = new IntersectionSceneQueryResult(OgrePINVOKE.IntersectionSceneQuery_execute__SWIG_0(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void execute(IntersectionSceneQueryListener listener) {
    OgrePINVOKE.IntersectionSceneQuery_execute__SWIG_1(swigCPtr, IntersectionSceneQueryListener.getCPtr(listener));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual IntersectionSceneQueryResult getLastResults() {
    IntersectionSceneQueryResult ret = new IntersectionSceneQueryResult(OgrePINVOKE.IntersectionSceneQuery_getLastResults(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void clearResults() {
    OgrePINVOKE.IntersectionSceneQuery_clearResults(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool queryResult(MovableObject first, MovableObject second) {
    bool ret = OgrePINVOKE.IntersectionSceneQuery_queryResult__SWIG_0(swigCPtr, MovableObject.getCPtr(first), MovableObject.getCPtr(second));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool queryResult(MovableObject movable, SWIGTYPE_p_Ogre__WorldFragment fragment) {
    bool ret = OgrePINVOKE.IntersectionSceneQuery_queryResult__SWIG_1(swigCPtr, MovableObject.getCPtr(movable), SWIGTYPE_p_Ogre__WorldFragment.getCPtr(fragment));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
