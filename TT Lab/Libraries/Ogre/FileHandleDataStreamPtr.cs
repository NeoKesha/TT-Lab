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

public class FileHandleDataStreamPtr : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal FileHandleDataStreamPtr(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FileHandleDataStreamPtr obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(FileHandleDataStreamPtr obj) {
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

  ~FileHandleDataStreamPtr() {
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
          OgrePINVOKE.delete_FileHandleDataStreamPtr(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public FileHandleDataStreamPtr(SWIGTYPE_p_std__nullptr_t arg0) : this(OgrePINVOKE.new_FileHandleDataStreamPtr__SWIG_0(SWIGTYPE_p_std__nullptr_t.getCPtr(arg0)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FileHandleDataStreamPtr() : this(OgrePINVOKE.new_FileHandleDataStreamPtr__SWIG_1(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FileHandleDataStreamPtr(FileHandleDataStreamPtr r) : this(OgrePINVOKE.new_FileHandleDataStreamPtr__SWIG_2(FileHandleDataStreamPtr.getCPtr(r)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FileHandleDataStream __deref__() {
    global::System.IntPtr cPtr = OgrePINVOKE.FileHandleDataStreamPtr___deref__(swigCPtr);
    FileHandleDataStream ret = (cPtr == global::System.IntPtr.Zero) ? null : new FileHandleDataStream(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FileHandleDataStreamPtr(FileHandleDataStream underlyingClass) : this(OgrePINVOKE.new_FileHandleDataStreamPtr__SWIG_3(FileHandleDataStream.getCPtr(underlyingClass)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint read(global::System.IntPtr buf, uint count) {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_read(swigCPtr, buf, count);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint write(global::System.IntPtr buf, uint count) {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_write(swigCPtr, buf, count);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void skip(int count) {
    OgrePINVOKE.FileHandleDataStreamPtr_skip(swigCPtr, count);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void seek(uint pos) {
    OgrePINVOKE.FileHandleDataStreamPtr_seek(swigCPtr, pos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint tell() {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_tell(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool eof() {
    bool ret = OgrePINVOKE.FileHandleDataStreamPtr_eof(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void close() {
    OgrePINVOKE.FileHandleDataStreamPtr_close(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getName() {
    string ret = OgrePINVOKE.FileHandleDataStreamPtr_getName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ushort getAccessMode() {
    ushort ret = OgrePINVOKE.FileHandleDataStreamPtr_getAccessMode(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isReadable() {
    bool ret = OgrePINVOKE.FileHandleDataStreamPtr_isReadable(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isWriteable() {
    bool ret = OgrePINVOKE.FileHandleDataStreamPtr_isWriteable(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint readLine(string buf, uint maxCount, string delim) {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_readLine__SWIG_0(swigCPtr, buf, maxCount, delim);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint readLine(string buf, uint maxCount) {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_readLine__SWIG_1(swigCPtr, buf, maxCount);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getLine(bool trimAfter) {
    string ret = OgrePINVOKE.FileHandleDataStreamPtr_getLine__SWIG_0(swigCPtr, trimAfter);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getLine() {
    string ret = OgrePINVOKE.FileHandleDataStreamPtr_getLine__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getAsString() {
    string ret = OgrePINVOKE.FileHandleDataStreamPtr_getAsString(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint skipLine(string delim) {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_skipLine__SWIG_0(swigCPtr, delim);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint skipLine() {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_skipLine__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint size() {
    uint ret = OgrePINVOKE.FileHandleDataStreamPtr_size(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
