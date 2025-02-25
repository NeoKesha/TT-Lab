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

public class ImDrawCmd : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImDrawCmd(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImDrawCmd obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ImDrawCmd obj) {
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

  ~ImDrawCmd() {
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
          ImGuiPINVOKE.delete_ImDrawCmd(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ImVec4 ClipRect {
    set {
      ImGuiPINVOKE.ImDrawCmd_ClipRect_set(swigCPtr, ImVec4.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImDrawCmd_ClipRect_get(swigCPtr);
      ImVec4 ret = (cPtr == global::System.IntPtr.Zero) ? null : new ImVec4(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_void TextureId {
    set {
      ImGuiPINVOKE.ImDrawCmd_TextureId_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImDrawCmd_TextureId_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      return ret;
    } 
  }

  public uint VtxOffset {
    set {
      ImGuiPINVOKE.ImDrawCmd_VtxOffset_set(swigCPtr, value);
    } 
    get {
      uint ret = ImGuiPINVOKE.ImDrawCmd_VtxOffset_get(swigCPtr);
      return ret;
    } 
  }

  public uint IdxOffset {
    set {
      ImGuiPINVOKE.ImDrawCmd_IdxOffset_set(swigCPtr, value);
    } 
    get {
      uint ret = ImGuiPINVOKE.ImDrawCmd_IdxOffset_get(swigCPtr);
      return ret;
    } 
  }

  public uint ElemCount {
    set {
      ImGuiPINVOKE.ImDrawCmd_ElemCount_set(swigCPtr, value);
    } 
    get {
      uint ret = ImGuiPINVOKE.ImDrawCmd_ElemCount_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_f_p_q_const__ImDrawList_p_q_const__ImDrawCmd__void UserCallback {
    set {
      ImGuiPINVOKE.ImDrawCmd_UserCallback_set(swigCPtr, SWIGTYPE_p_f_p_q_const__ImDrawList_p_q_const__ImDrawCmd__void.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImDrawCmd_UserCallback_get(swigCPtr);
      SWIGTYPE_p_f_p_q_const__ImDrawList_p_q_const__ImDrawCmd__void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_f_p_q_const__ImDrawList_p_q_const__ImDrawCmd__void(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_void UserCallbackData {
    set {
      ImGuiPINVOKE.ImDrawCmd_UserCallbackData_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImDrawCmd_UserCallbackData_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      return ret;
    } 
  }

  public ImDrawCmd() : this(ImGuiPINVOKE.new_ImDrawCmd(), true) {
  }

  public SWIGTYPE_p_void GetTexID() {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImDrawCmd_GetTexID(swigCPtr);
    SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
    return ret;
  }

}

}
