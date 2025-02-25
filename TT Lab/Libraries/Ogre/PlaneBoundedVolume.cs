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

public class PlaneBoundedVolume : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PlaneBoundedVolume(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(PlaneBoundedVolume obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(PlaneBoundedVolume obj) {
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

  ~PlaneBoundedVolume() {
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
          OgrePINVOKE.delete_PlaneBoundedVolume(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SWIGTYPE_p_std__vectorT_Ogre__Plane_t planes {
    set {
      OgrePINVOKE.PlaneBoundedVolume_planes_set(swigCPtr, SWIGTYPE_p_std__vectorT_Ogre__Plane_t.getCPtr(value));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = OgrePINVOKE.PlaneBoundedVolume_planes_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_Ogre__Plane_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_Ogre__Plane_t(cPtr, false);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public Plane.Side outside {
    set {
      OgrePINVOKE.PlaneBoundedVolume_outside_set(swigCPtr, (int)value);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      Plane.Side ret = (Plane.Side)OgrePINVOKE.PlaneBoundedVolume_outside_get(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public PlaneBoundedVolume() : this(OgrePINVOKE.new_PlaneBoundedVolume__SWIG_0(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public PlaneBoundedVolume(Plane.Side theOutside) : this(OgrePINVOKE.new_PlaneBoundedVolume__SWIG_1((int)theOutside), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool intersects(AxisAlignedBox box) {
    bool ret = OgrePINVOKE.PlaneBoundedVolume_intersects__SWIG_0(swigCPtr, AxisAlignedBox.getCPtr(box));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool intersects(Sphere sphere) {
    bool ret = OgrePINVOKE.PlaneBoundedVolume_intersects__SWIG_1(swigCPtr, Sphere.getCPtr(sphere));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public RayTestResult intersects(Ray ray) {
    RayTestResult ret = new RayTestResult(OgrePINVOKE.PlaneBoundedVolume_intersects__SWIG_2(swigCPtr, Ray.getCPtr(ray)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
