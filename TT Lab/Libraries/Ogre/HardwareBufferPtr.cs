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

public class HardwareBufferPtr : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal HardwareBufferPtr(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(HardwareBufferPtr obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(HardwareBufferPtr obj) {
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

  ~HardwareBufferPtr() {
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
          OgrePINVOKE.delete_HardwareBufferPtr(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public HardwareBufferPtr(SWIGTYPE_p_std__nullptr_t arg0) : this(OgrePINVOKE.new_HardwareBufferPtr__SWIG_0(SWIGTYPE_p_std__nullptr_t.getCPtr(arg0)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareBufferPtr() : this(OgrePINVOKE.new_HardwareBufferPtr__SWIG_1(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareBufferPtr(HardwareBufferPtr r) : this(OgrePINVOKE.new_HardwareBufferPtr__SWIG_2(HardwareBufferPtr.getCPtr(r)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public HardwareBuffer __deref__() {
    global::System.IntPtr cPtr = OgrePINVOKE.HardwareBufferPtr___deref__(swigCPtr);
    HardwareBuffer ret = (cPtr == global::System.IntPtr.Zero) ? null : new HardwareBuffer(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public HardwareBufferPtr(HardwareBuffer underlyingClass) : this(OgrePINVOKE.new_HardwareBufferPtr__SWIG_3(HardwareBuffer.getCPtr(underlyingClass)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public global::System.IntPtr lock_(uint offset, uint length, HardwareBuffer.LockOptions options) {
    global::System.IntPtr ret = OgrePINVOKE.HardwareBufferPtr_lock___SWIG_0(swigCPtr, offset, length, (int)options);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public global::System.IntPtr lock_(HardwareBuffer.LockOptions options) {
    global::System.IntPtr ret = OgrePINVOKE.HardwareBufferPtr_lock___SWIG_1(swigCPtr, (int)options);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void unlock() {
    OgrePINVOKE.HardwareBufferPtr_unlock(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void readData(uint offset, uint length, global::System.IntPtr pDest) {
    OgrePINVOKE.HardwareBufferPtr_readData(swigCPtr, offset, length, pDest);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void writeData(uint offset, uint length, global::System.IntPtr pSource, bool discardWholeBuffer) {
    OgrePINVOKE.HardwareBufferPtr_writeData__SWIG_0(swigCPtr, offset, length, pSource, discardWholeBuffer);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void writeData(uint offset, uint length, global::System.IntPtr pSource) {
    OgrePINVOKE.HardwareBufferPtr_writeData__SWIG_1(swigCPtr, offset, length, pSource);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void copyData(HardwareBuffer srcBuffer, uint srcOffset, uint dstOffset, uint length, bool discardWholeBuffer) {
    OgrePINVOKE.HardwareBufferPtr_copyData__SWIG_0(swigCPtr, HardwareBuffer.getCPtr(srcBuffer), srcOffset, dstOffset, length, discardWholeBuffer);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void copyData(HardwareBuffer srcBuffer, uint srcOffset, uint dstOffset, uint length) {
    OgrePINVOKE.HardwareBufferPtr_copyData__SWIG_1(swigCPtr, HardwareBuffer.getCPtr(srcBuffer), srcOffset, dstOffset, length);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void copyData(HardwareBuffer srcBuffer) {
    OgrePINVOKE.HardwareBufferPtr_copyData__SWIG_2(swigCPtr, HardwareBuffer.getCPtr(srcBuffer));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _updateFromShadow() {
    OgrePINVOKE.HardwareBufferPtr__updateFromShadow(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getSizeInBytes() {
    uint ret = OgrePINVOKE.HardwareBufferPtr_getSizeInBytes(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public byte getUsage() {
    byte ret = OgrePINVOKE.HardwareBufferPtr_getUsage(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isSystemMemory() {
    bool ret = OgrePINVOKE.HardwareBufferPtr_isSystemMemory(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool hasShadowBuffer() {
    bool ret = OgrePINVOKE.HardwareBufferPtr_hasShadowBuffer(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isLocked() {
    bool ret = OgrePINVOKE.HardwareBufferPtr_isLocked(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void suppressHardwareUpdate(bool suppress) {
    OgrePINVOKE.HardwareBufferPtr_suppressHardwareUpdate(swigCPtr, suppress);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
