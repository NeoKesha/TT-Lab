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

public class TouchFingerEvent : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal TouchFingerEvent(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TouchFingerEvent obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(TouchFingerEvent obj) {
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

  ~TouchFingerEvent() {
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
          BitesPINVOKE.delete_TouchFingerEvent(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public int type {
    set {
      BitesPINVOKE.TouchFingerEvent_type_set(swigCPtr, value);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = BitesPINVOKE.TouchFingerEvent_type_get(swigCPtr);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int fingerId {
    set {
      BitesPINVOKE.TouchFingerEvent_fingerId_set(swigCPtr, value);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      int ret = BitesPINVOKE.TouchFingerEvent_fingerId_get(swigCPtr);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float x {
    set {
      BitesPINVOKE.TouchFingerEvent_x_set(swigCPtr, value);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = BitesPINVOKE.TouchFingerEvent_x_get(swigCPtr);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float y {
    set {
      BitesPINVOKE.TouchFingerEvent_y_set(swigCPtr, value);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = BitesPINVOKE.TouchFingerEvent_y_get(swigCPtr);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float dx {
    set {
      BitesPINVOKE.TouchFingerEvent_dx_set(swigCPtr, value);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = BitesPINVOKE.TouchFingerEvent_dx_get(swigCPtr);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public float dy {
    set {
      BitesPINVOKE.TouchFingerEvent_dy_set(swigCPtr, value);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      float ret = BitesPINVOKE.TouchFingerEvent_dy_get(swigCPtr);
      if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public TouchFingerEvent() : this(BitesPINVOKE.new_TouchFingerEvent(), true) {
    if (BitesPINVOKE.SWIGPendingException.Pending) throw BitesPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
