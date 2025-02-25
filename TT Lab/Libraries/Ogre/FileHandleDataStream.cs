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

public class FileHandleDataStream : DataStream {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FileHandleDataStream(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.FileHandleDataStream_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FileHandleDataStream obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(FileHandleDataStream obj) {
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
          OgrePINVOKE.delete_FileHandleDataStream(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public FileHandleDataStream(SWIGTYPE_p_FILE handle, ushort accessMode) : this(OgrePINVOKE.new_FileHandleDataStream__SWIG_0(SWIGTYPE_p_FILE.getCPtr(handle), accessMode), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FileHandleDataStream(SWIGTYPE_p_FILE handle) : this(OgrePINVOKE.new_FileHandleDataStream__SWIG_1(SWIGTYPE_p_FILE.getCPtr(handle)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FileHandleDataStream(string name, SWIGTYPE_p_FILE handle, ushort accessMode) : this(OgrePINVOKE.new_FileHandleDataStream__SWIG_2(name, SWIGTYPE_p_FILE.getCPtr(handle), accessMode), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public FileHandleDataStream(string name, SWIGTYPE_p_FILE handle) : this(OgrePINVOKE.new_FileHandleDataStream__SWIG_3(name, SWIGTYPE_p_FILE.getCPtr(handle)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override uint read(global::System.IntPtr buf, uint count) {
    uint ret = OgrePINVOKE.FileHandleDataStream_read(swigCPtr, buf, count);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override uint write(global::System.IntPtr buf, uint count) {
    uint ret = OgrePINVOKE.FileHandleDataStream_write(swigCPtr, buf, count);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void skip(int count) {
    OgrePINVOKE.FileHandleDataStream_skip(swigCPtr, count);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void seek(uint pos) {
    OgrePINVOKE.FileHandleDataStream_seek(swigCPtr, pos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override uint tell() {
    uint ret = OgrePINVOKE.FileHandleDataStream_tell(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool eof() {
    bool ret = OgrePINVOKE.FileHandleDataStream_eof(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void close() {
    OgrePINVOKE.FileHandleDataStream_close(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
