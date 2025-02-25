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

public class ScriptCompilerListener : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ScriptCompilerListener(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ScriptCompilerListener obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ScriptCompilerListener obj) {
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

  ~ScriptCompilerListener() {
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
          OgrePINVOKE.delete_ScriptCompilerListener(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ScriptCompilerListener() : this(OgrePINVOKE.new_ScriptCompilerListener(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual SWIGTYPE_p_Ogre__SharedPtrT_std__listT_Ogre__SharedPtrT_Ogre__ConcreteNode_t_t_t importFile(ScriptCompiler compiler, string name) {
    SWIGTYPE_p_Ogre__SharedPtrT_std__listT_Ogre__SharedPtrT_Ogre__ConcreteNode_t_t_t ret = new SWIGTYPE_p_Ogre__SharedPtrT_std__listT_Ogre__SharedPtrT_Ogre__ConcreteNode_t_t_t(OgrePINVOKE.ScriptCompilerListener_importFile(swigCPtr, ScriptCompiler.getCPtr(compiler), name), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void preConversion(ScriptCompiler compiler, SWIGTYPE_p_Ogre__SharedPtrT_std__listT_Ogre__SharedPtrT_Ogre__ConcreteNode_t_t_t nodes) {
    OgrePINVOKE.ScriptCompilerListener_preConversion(swigCPtr, ScriptCompiler.getCPtr(compiler), SWIGTYPE_p_Ogre__SharedPtrT_std__listT_Ogre__SharedPtrT_Ogre__ConcreteNode_t_t_t.getCPtr(nodes));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual bool postConversion(ScriptCompiler compiler, SWIGTYPE_p_Ogre__SharedPtrT_std__listT_Ogre__SharedPtrT_Ogre__AbstractNode_t_t_t arg1) {
    bool ret = OgrePINVOKE.ScriptCompilerListener_postConversion(swigCPtr, ScriptCompiler.getCPtr(compiler), SWIGTYPE_p_Ogre__SharedPtrT_std__listT_Ogre__SharedPtrT_Ogre__AbstractNode_t_t_t.getCPtr(arg1));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void handleError(ScriptCompiler compiler, uint code, string file, int line, string msg) {
    OgrePINVOKE.ScriptCompilerListener_handleError(swigCPtr, ScriptCompiler.getCPtr(compiler), code, file, line, msg);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual bool handleEvent(ScriptCompiler compiler, ScriptCompilerEvent evt, global::System.IntPtr retval) {
    bool ret = OgrePINVOKE.ScriptCompilerListener_handleEvent(swigCPtr, ScriptCompiler.getCPtr(compiler), ScriptCompilerEvent.getCPtr(evt), retval);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
