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

public class LayeredBlending : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal LayeredBlending(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LayeredBlending obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(LayeredBlending obj) {
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

  ~LayeredBlending() {
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
          RTShaderPINVOKE.delete_LayeredBlending(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public LayeredBlending() : this(RTShaderPINVOKE.new_LayeredBlending(), true) {
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getType() {
    string ret = RTShaderPINVOKE.LayeredBlending_getType(swigCPtr);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool setBlendMode(ushort index, string mode) {
    bool ret = RTShaderPINVOKE.LayeredBlending_setBlendMode(swigCPtr, index, mode);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public BlendMode getBlendMode(ushort index) {
    BlendMode ret = (BlendMode)RTShaderPINVOKE.LayeredBlending_getBlendMode(swigCPtr, index);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool setSourceModifier(ushort index, string modType, int customNum) {
    bool ret = RTShaderPINVOKE.LayeredBlending_setSourceModifier(swigCPtr, index, modType, customNum);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool getSourceModifier(ushort index, SWIGTYPE_p_Ogre__RTShader__SourceModifier modType, SWIGTYPE_p_int customNum) {
    bool ret = RTShaderPINVOKE.LayeredBlending_getSourceModifier(swigCPtr, index, SWIGTYPE_p_Ogre__RTShader__SourceModifier.getCPtr(modType), SWIGTYPE_p_int.getCPtr(customNum));
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void copyFrom(SubRenderState rhs) {
    RTShaderPINVOKE.LayeredBlending_copyFrom(swigCPtr, SubRenderState.getCPtr(rhs));
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
  }

  public static string Type {
    set {
      RTShaderPINVOKE.LayeredBlending_Type_set(value);
      if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = RTShaderPINVOKE.LayeredBlending_Type_get();
      if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

}

}
