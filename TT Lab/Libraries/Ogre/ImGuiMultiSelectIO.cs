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

public class ImGuiMultiSelectIO : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImGuiMultiSelectIO(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImGuiMultiSelectIO obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ImGuiMultiSelectIO obj) {
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

  ~ImGuiMultiSelectIO() {
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
          ImGuiPINVOKE.delete_ImGuiMultiSelectIO(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SWIGTYPE_p_ImVectorT_ImGuiSelectionRequest_t Requests {
    set {
      ImGuiPINVOKE.ImGuiMultiSelectIO_Requests_set(swigCPtr, SWIGTYPE_p_ImVectorT_ImGuiSelectionRequest_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiMultiSelectIO_Requests_get(swigCPtr);
      SWIGTYPE_p_ImVectorT_ImGuiSelectionRequest_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_ImVectorT_ImGuiSelectionRequest_t(cPtr, false);
      return ret;
    } 
  }

  public long RangeSrcItem {
    set {
      ImGuiPINVOKE.ImGuiMultiSelectIO_RangeSrcItem_set(swigCPtr, value);
    } 
    get {
      long ret = ImGuiPINVOKE.ImGuiMultiSelectIO_RangeSrcItem_get(swigCPtr);
      return ret;
    } 
  }

  public long NavIdItem {
    set {
      ImGuiPINVOKE.ImGuiMultiSelectIO_NavIdItem_set(swigCPtr, value);
    } 
    get {
      long ret = ImGuiPINVOKE.ImGuiMultiSelectIO_NavIdItem_get(swigCPtr);
      return ret;
    } 
  }

  public bool NavIdSelected {
    set {
      ImGuiPINVOKE.ImGuiMultiSelectIO_NavIdSelected_set(swigCPtr, value);
    } 
    get {
      bool ret = ImGuiPINVOKE.ImGuiMultiSelectIO_NavIdSelected_get(swigCPtr);
      return ret;
    } 
  }

  public bool RangeSrcReset {
    set {
      ImGuiPINVOKE.ImGuiMultiSelectIO_RangeSrcReset_set(swigCPtr, value);
    } 
    get {
      bool ret = ImGuiPINVOKE.ImGuiMultiSelectIO_RangeSrcReset_get(swigCPtr);
      return ret;
    } 
  }

  public int ItemsCount {
    set {
      ImGuiPINVOKE.ImGuiMultiSelectIO_ItemsCount_set(swigCPtr, value);
    } 
    get {
      int ret = ImGuiPINVOKE.ImGuiMultiSelectIO_ItemsCount_get(swigCPtr);
      return ret;
    } 
  }

  public ImGuiMultiSelectIO() : this(ImGuiPINVOKE.new_ImGuiMultiSelectIO(), true) {
  }

}

}
