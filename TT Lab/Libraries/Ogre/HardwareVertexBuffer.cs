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

public class HardwareVertexBuffer : HardwareBuffer {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal HardwareVertexBuffer(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.HardwareVertexBuffer_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(HardwareVertexBuffer obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(HardwareVertexBuffer obj) {
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
          OgrePINVOKE.delete_HardwareVertexBuffer(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public HardwareVertexBuffer(HardwareBufferManagerBase mgr, uint vertexSize, uint numVertices, byte usage, bool useShadowBuffer) : this(OgrePINVOKE.new_HardwareVertexBuffer__SWIG_0(HardwareBufferManagerBase.getCPtr(mgr), vertexSize, numVertices, usage, useShadowBuffer), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareVertexBuffer(HardwareBufferManagerBase mgr, uint vertexSize, uint numVertices, HardwareBuffer delegate_) : this(OgrePINVOKE.new_HardwareVertexBuffer__SWIG_1(HardwareBufferManagerBase.getCPtr(mgr), vertexSize, numVertices, HardwareBuffer.getCPtr(delegate_)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareBufferManagerBase getManager() {
    global::System.IntPtr cPtr = OgrePINVOKE.HardwareVertexBuffer_getManager(swigCPtr);
    HardwareBufferManagerBase ret = (cPtr == global::System.IntPtr.Zero) ? null : new HardwareBufferManagerBase(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getVertexSize() {
    uint ret = OgrePINVOKE.HardwareVertexBuffer_getVertexSize(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getNumVertices() {
    uint ret = OgrePINVOKE.HardwareVertexBuffer_getNumVertices(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isInstanceData() {
    bool ret = OgrePINVOKE.HardwareVertexBuffer_isInstanceData(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setIsInstanceData(bool val) {
    OgrePINVOKE.HardwareVertexBuffer_setIsInstanceData(swigCPtr, val);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getInstanceDataStepRate() {
    uint ret = OgrePINVOKE.HardwareVertexBuffer_getInstanceDataStepRate(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setInstanceDataStepRate(uint val) {
    OgrePINVOKE.HardwareVertexBuffer_setInstanceDataStepRate(swigCPtr, val);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
