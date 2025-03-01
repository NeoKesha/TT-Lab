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

public class ImGuiListClipper : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImGuiListClipper(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImGuiListClipper obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ImGuiListClipper obj) {
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

  ~ImGuiListClipper() {
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
          ImGuiPINVOKE.delete_ImGuiListClipper(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SWIGTYPE_p_ImGuiContext Ctx {
    set {
      ImGuiPINVOKE.ImGuiListClipper_Ctx_set(swigCPtr, SWIGTYPE_p_ImGuiContext.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiListClipper_Ctx_get(swigCPtr);
      SWIGTYPE_p_ImGuiContext ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_ImGuiContext(cPtr, false);
      return ret;
    } 
  }

  public int DisplayStart {
    set {
      ImGuiPINVOKE.ImGuiListClipper_DisplayStart_set(swigCPtr, value);
    } 
    get {
      int ret = ImGuiPINVOKE.ImGuiListClipper_DisplayStart_get(swigCPtr);
      return ret;
    } 
  }

  public int DisplayEnd {
    set {
      ImGuiPINVOKE.ImGuiListClipper_DisplayEnd_set(swigCPtr, value);
    } 
    get {
      int ret = ImGuiPINVOKE.ImGuiListClipper_DisplayEnd_get(swigCPtr);
      return ret;
    } 
  }

  public int ItemsCount {
    set {
      ImGuiPINVOKE.ImGuiListClipper_ItemsCount_set(swigCPtr, value);
    } 
    get {
      int ret = ImGuiPINVOKE.ImGuiListClipper_ItemsCount_get(swigCPtr);
      return ret;
    } 
  }

  public float ItemsHeight {
    set {
      ImGuiPINVOKE.ImGuiListClipper_ItemsHeight_set(swigCPtr, value);
    } 
    get {
      float ret = ImGuiPINVOKE.ImGuiListClipper_ItemsHeight_get(swigCPtr);
      return ret;
    } 
  }

  public float StartPosY {
    set {
      ImGuiPINVOKE.ImGuiListClipper_StartPosY_set(swigCPtr, value);
    } 
    get {
      float ret = ImGuiPINVOKE.ImGuiListClipper_StartPosY_get(swigCPtr);
      return ret;
    } 
  }

  public double StartSeekOffsetY {
    set {
      ImGuiPINVOKE.ImGuiListClipper_StartSeekOffsetY_set(swigCPtr, value);
    } 
    get {
      double ret = ImGuiPINVOKE.ImGuiListClipper_StartSeekOffsetY_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_void TempData {
    set {
      ImGuiPINVOKE.ImGuiListClipper_TempData_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiListClipper_TempData_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      return ret;
    } 
  }

  public ImGuiListClipper() : this(ImGuiPINVOKE.new_ImGuiListClipper(), true) {
  }

  public void Begin(int items_count, float items_height) {
    ImGuiPINVOKE.ImGuiListClipper_Begin__SWIG_0(swigCPtr, items_count, items_height);
  }

  public void Begin(int items_count) {
    ImGuiPINVOKE.ImGuiListClipper_Begin__SWIG_1(swigCPtr, items_count);
  }

  public void End() {
    ImGuiPINVOKE.ImGuiListClipper_End(swigCPtr);
  }

  public bool Step() {
    bool ret = ImGuiPINVOKE.ImGuiListClipper_Step(swigCPtr);
    return ret;
  }

  public void IncludeItemByIndex(int item_index) {
    ImGuiPINVOKE.ImGuiListClipper_IncludeItemByIndex(swigCPtr, item_index);
  }

  public void IncludeItemsByIndex(int item_begin, int item_end) {
    ImGuiPINVOKE.ImGuiListClipper_IncludeItemsByIndex(swigCPtr, item_begin, item_end);
  }

  public void SeekCursorForItem(int item_index) {
    ImGuiPINVOKE.ImGuiListClipper_SeekCursorForItem(swigCPtr, item_index);
  }

}

}
