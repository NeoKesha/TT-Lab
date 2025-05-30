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

public class ImGuiTableColumnSortSpecs : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImGuiTableColumnSortSpecs(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImGuiTableColumnSortSpecs obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ImGuiTableColumnSortSpecs obj) {
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

  ~ImGuiTableColumnSortSpecs() {
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
          ImGuiPINVOKE.delete_ImGuiTableColumnSortSpecs(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public uint ColumnUserID {
    set {
      ImGuiPINVOKE.ImGuiTableColumnSortSpecs_ColumnUserID_set(swigCPtr, value);
    } 
    get {
      uint ret = ImGuiPINVOKE.ImGuiTableColumnSortSpecs_ColumnUserID_get(swigCPtr);
      return ret;
    } 
  }

  public short ColumnIndex {
    set {
      ImGuiPINVOKE.ImGuiTableColumnSortSpecs_ColumnIndex_set(swigCPtr, value);
    } 
    get {
      short ret = ImGuiPINVOKE.ImGuiTableColumnSortSpecs_ColumnIndex_get(swigCPtr);
      return ret;
    } 
  }

  public short SortOrder {
    set {
      ImGuiPINVOKE.ImGuiTableColumnSortSpecs_SortOrder_set(swigCPtr, value);
    } 
    get {
      short ret = ImGuiPINVOKE.ImGuiTableColumnSortSpecs_SortOrder_get(swigCPtr);
      return ret;
    } 
  }

  public ImGuiSortDirection SortDirection {
    set {
      ImGuiPINVOKE.ImGuiTableColumnSortSpecs_SortDirection_set(swigCPtr, (int)value);
    } 
    get {
      ImGuiSortDirection ret = (ImGuiSortDirection)ImGuiPINVOKE.ImGuiTableColumnSortSpecs_SortDirection_get(swigCPtr);
      return ret;
    } 
  }

  public ImGuiTableColumnSortSpecs() : this(ImGuiPINVOKE.new_ImGuiTableColumnSortSpecs(), true) {
  }

}

}
