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

public class ImGuiStorage : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ImGuiStorage(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ImGuiStorage obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ImGuiStorage obj) {
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

  ~ImGuiStorage() {
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
          ImGuiPINVOKE.delete_ImGuiStorage(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SWIGTYPE_p_ImVectorT_ImGuiStoragePair_t Data {
    set {
      ImGuiPINVOKE.ImGuiStorage_Data_set(swigCPtr, SWIGTYPE_p_ImVectorT_ImGuiStoragePair_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_Data_get(swigCPtr);
      SWIGTYPE_p_ImVectorT_ImGuiStoragePair_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_ImVectorT_ImGuiStoragePair_t(cPtr, false);
      return ret;
    } 
  }

  public void Clear() {
    ImGuiPINVOKE.ImGuiStorage_Clear(swigCPtr);
  }

  public int GetInt(uint key, int default_val) {
    int ret = ImGuiPINVOKE.ImGuiStorage_GetInt__SWIG_0(swigCPtr, key, default_val);
    return ret;
  }

  public int GetInt(uint key) {
    int ret = ImGuiPINVOKE.ImGuiStorage_GetInt__SWIG_1(swigCPtr, key);
    return ret;
  }

  public void SetInt(uint key, int val) {
    ImGuiPINVOKE.ImGuiStorage_SetInt(swigCPtr, key, val);
  }

  public bool GetBool(uint key, bool default_val) {
    bool ret = ImGuiPINVOKE.ImGuiStorage_GetBool__SWIG_0(swigCPtr, key, default_val);
    return ret;
  }

  public bool GetBool(uint key) {
    bool ret = ImGuiPINVOKE.ImGuiStorage_GetBool__SWIG_1(swigCPtr, key);
    return ret;
  }

  public void SetBool(uint key, bool val) {
    ImGuiPINVOKE.ImGuiStorage_SetBool(swigCPtr, key, val);
  }

  public float GetFloat(uint key, float default_val) {
    float ret = ImGuiPINVOKE.ImGuiStorage_GetFloat__SWIG_0(swigCPtr, key, default_val);
    return ret;
  }

  public float GetFloat(uint key) {
    float ret = ImGuiPINVOKE.ImGuiStorage_GetFloat__SWIG_1(swigCPtr, key);
    return ret;
  }

  public void SetFloat(uint key, float val) {
    ImGuiPINVOKE.ImGuiStorage_SetFloat(swigCPtr, key, val);
  }

  public SWIGTYPE_p_void GetVoidPtr(uint key) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetVoidPtr(swigCPtr, key);
    SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
    return ret;
  }

  public void SetVoidPtr(uint key, SWIGTYPE_p_void val) {
    ImGuiPINVOKE.ImGuiStorage_SetVoidPtr(swigCPtr, key, SWIGTYPE_p_void.getCPtr(val));
  }

  public SWIGTYPE_p_int GetIntRef(uint key, int default_val) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetIntRef__SWIG_0(swigCPtr, key, default_val);
    SWIGTYPE_p_int ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_int(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_int GetIntRef(uint key) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetIntRef__SWIG_1(swigCPtr, key);
    SWIGTYPE_p_int ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_int(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_bool GetBoolRef(uint key, bool default_val) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetBoolRef__SWIG_0(swigCPtr, key, default_val);
    SWIGTYPE_p_bool ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_bool(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_bool GetBoolRef(uint key) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetBoolRef__SWIG_1(swigCPtr, key);
    SWIGTYPE_p_bool ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_bool(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_float GetFloatRef(uint key, float default_val) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetFloatRef__SWIG_0(swigCPtr, key, default_val);
    SWIGTYPE_p_float ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_float(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_float GetFloatRef(uint key) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetFloatRef__SWIG_1(swigCPtr, key);
    SWIGTYPE_p_float ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_float(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_p_void GetVoidPtrRef(uint key, SWIGTYPE_p_void default_val) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetVoidPtrRef__SWIG_0(swigCPtr, key, SWIGTYPE_p_void.getCPtr(default_val));
    SWIGTYPE_p_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_p_void(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_p_void GetVoidPtrRef(uint key) {
    global::System.IntPtr cPtr = ImGuiPINVOKE.ImGuiStorage_GetVoidPtrRef__SWIG_1(swigCPtr, key);
    SWIGTYPE_p_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_p_void(cPtr, false);
    return ret;
  }

  public void BuildSortByKey() {
    ImGuiPINVOKE.ImGuiStorage_BuildSortByKey(swigCPtr);
  }

  public void SetAllInt(int val) {
    ImGuiPINVOKE.ImGuiStorage_SetAllInt(swigCPtr, val);
  }

  public ImGuiStorage() : this(ImGuiPINVOKE.new_ImGuiStorage(), true) {
  }

}

}
