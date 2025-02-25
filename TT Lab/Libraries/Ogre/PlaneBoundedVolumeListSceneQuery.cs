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

public class PlaneBoundedVolumeListSceneQuery : RegionSceneQuery {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal PlaneBoundedVolumeListSceneQuery(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.PlaneBoundedVolumeListSceneQuery_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(PlaneBoundedVolumeListSceneQuery obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(PlaneBoundedVolumeListSceneQuery obj) {
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
          OgrePINVOKE.delete_PlaneBoundedVolumeListSceneQuery(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public void setVolumes(SWIGTYPE_p_std__vectorT_Ogre__PlaneBoundedVolume_t volumes) {
    OgrePINVOKE.PlaneBoundedVolumeListSceneQuery_setVolumes(swigCPtr, SWIGTYPE_p_std__vectorT_Ogre__PlaneBoundedVolume_t.getCPtr(volumes));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__vectorT_Ogre__PlaneBoundedVolume_t getVolumes() {
    SWIGTYPE_p_std__vectorT_Ogre__PlaneBoundedVolume_t ret = new SWIGTYPE_p_std__vectorT_Ogre__PlaneBoundedVolume_t(OgrePINVOKE.PlaneBoundedVolumeListSceneQuery_getVolumes(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
