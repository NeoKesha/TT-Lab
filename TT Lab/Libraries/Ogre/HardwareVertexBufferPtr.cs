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

public class HardwareVertexBufferPtr : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal HardwareVertexBufferPtr(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(HardwareVertexBufferPtr obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(HardwareVertexBufferPtr obj) {
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

  ~HardwareVertexBufferPtr() {
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
          OgrePINVOKE.delete_HardwareVertexBufferPtr(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public HardwareVertexBufferPtr(SWIGTYPE_p_std__nullptr_t arg0) : this(OgrePINVOKE.new_HardwareVertexBufferPtr__SWIG_0(SWIGTYPE_p_std__nullptr_t.getCPtr(arg0)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareVertexBufferPtr() : this(OgrePINVOKE.new_HardwareVertexBufferPtr__SWIG_1(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareVertexBufferPtr(HardwareVertexBufferPtr r) : this(OgrePINVOKE.new_HardwareVertexBufferPtr__SWIG_2(HardwareVertexBufferPtr.getCPtr(r)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareVertexBuffer __deref__() {
    global::System.IntPtr cPtr = OgrePINVOKE.HardwareVertexBufferPtr___deref__(swigCPtr);
    HardwareVertexBuffer ret = (cPtr == global::System.IntPtr.Zero) ? null : new HardwareVertexBuffer(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public HardwareVertexBufferPtr(HardwareVertexBuffer underlyingClass) : this(OgrePINVOKE.new_HardwareVertexBufferPtr__SWIG_3(HardwareVertexBuffer.getCPtr(underlyingClass)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareBufferManagerBase getManager() {
    global::System.IntPtr cPtr = OgrePINVOKE.HardwareVertexBufferPtr_getManager(swigCPtr);
    HardwareBufferManagerBase ret = (cPtr == global::System.IntPtr.Zero) ? null : new HardwareBufferManagerBase(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getVertexSize() {
    uint ret = OgrePINVOKE.HardwareVertexBufferPtr_getVertexSize(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getNumVertices() {
    uint ret = OgrePINVOKE.HardwareVertexBufferPtr_getNumVertices(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isInstanceData() {
    bool ret = OgrePINVOKE.HardwareVertexBufferPtr_isInstanceData(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setIsInstanceData(bool val) {
    OgrePINVOKE.HardwareVertexBufferPtr_setIsInstanceData(swigCPtr, val);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getInstanceDataStepRate() {
    uint ret = OgrePINVOKE.HardwareVertexBufferPtr_getInstanceDataStepRate(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setInstanceDataStepRate(uint val) {
    OgrePINVOKE.HardwareVertexBufferPtr_setInstanceDataStepRate(swigCPtr, val);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public global::System.IntPtr lock_(uint offset, uint length, HardwareBuffer.LockOptions options) {
    global::System.IntPtr ret = OgrePINVOKE.HardwareVertexBufferPtr_lock___SWIG_0(swigCPtr, offset, length, (int)options);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public global::System.IntPtr lock_(HardwareBuffer.LockOptions options) {
    global::System.IntPtr ret = OgrePINVOKE.HardwareVertexBufferPtr_lock___SWIG_1(swigCPtr, (int)options);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void unlock() {
    OgrePINVOKE.HardwareVertexBufferPtr_unlock(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void readData(uint offset, uint length, global::System.IntPtr pDest) {
    OgrePINVOKE.HardwareVertexBufferPtr_readData(swigCPtr, offset, length, pDest);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void writeData(uint offset, uint length, global::System.IntPtr pSource, bool discardWholeBuffer) {
    OgrePINVOKE.HardwareVertexBufferPtr_writeData__SWIG_0(swigCPtr, offset, length, pSource, discardWholeBuffer);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void writeData(uint offset, uint length, global::System.IntPtr pSource) {
    OgrePINVOKE.HardwareVertexBufferPtr_writeData__SWIG_1(swigCPtr, offset, length, pSource);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void copyData(HardwareBuffer srcBuffer, uint srcOffset, uint dstOffset, uint length, bool discardWholeBuffer) {
    OgrePINVOKE.HardwareVertexBufferPtr_copyData__SWIG_0(swigCPtr, HardwareBuffer.getCPtr(srcBuffer), srcOffset, dstOffset, length, discardWholeBuffer);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void copyData(HardwareBuffer srcBuffer, uint srcOffset, uint dstOffset, uint length) {
    OgrePINVOKE.HardwareVertexBufferPtr_copyData__SWIG_1(swigCPtr, HardwareBuffer.getCPtr(srcBuffer), srcOffset, dstOffset, length);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void copyData(HardwareBuffer srcBuffer) {
    OgrePINVOKE.HardwareVertexBufferPtr_copyData__SWIG_2(swigCPtr, HardwareBuffer.getCPtr(srcBuffer));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _updateFromShadow() {
    OgrePINVOKE.HardwareVertexBufferPtr__updateFromShadow(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getSizeInBytes() {
    uint ret = OgrePINVOKE.HardwareVertexBufferPtr_getSizeInBytes(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public byte getUsage() {
    byte ret = OgrePINVOKE.HardwareVertexBufferPtr_getUsage(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isSystemMemory() {
    bool ret = OgrePINVOKE.HardwareVertexBufferPtr_isSystemMemory(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool hasShadowBuffer() {
    bool ret = OgrePINVOKE.HardwareVertexBufferPtr_hasShadowBuffer(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isLocked() {
    bool ret = OgrePINVOKE.HardwareVertexBufferPtr_isLocked(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void suppressHardwareUpdate(bool suppress) {
    OgrePINVOKE.HardwareVertexBufferPtr_suppressHardwareUpdate(swigCPtr, suppress);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
