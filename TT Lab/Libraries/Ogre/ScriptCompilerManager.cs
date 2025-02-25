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

public class ScriptCompilerManager : ScriptLoader {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal ScriptCompilerManager(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.ScriptCompilerManager_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ScriptCompilerManager obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ScriptCompilerManager obj) {
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
          OgrePINVOKE.delete_ScriptCompilerManager(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ScriptCompilerManager() : this(OgrePINVOKE.new_ScriptCompilerManager(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setListener(ScriptCompilerListener listener) {
    OgrePINVOKE.ScriptCompilerManager_setListener(swigCPtr, ScriptCompilerListener.getCPtr(listener));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ScriptCompilerListener getListener() {
    global::System.IntPtr cPtr = OgrePINVOKE.ScriptCompilerManager_getListener(swigCPtr);
    ScriptCompilerListener ret = (cPtr == global::System.IntPtr.Zero) ? null : new ScriptCompilerListener(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void addTranslatorManager(SWIGTYPE_p_Ogre__ScriptTranslatorManager man) {
    OgrePINVOKE.ScriptCompilerManager_addTranslatorManager(swigCPtr, SWIGTYPE_p_Ogre__ScriptTranslatorManager.getCPtr(man));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeTranslatorManager(SWIGTYPE_p_Ogre__ScriptTranslatorManager man) {
    OgrePINVOKE.ScriptCompilerManager_removeTranslatorManager(swigCPtr, SWIGTYPE_p_Ogre__ScriptTranslatorManager.getCPtr(man));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void clearTranslatorManagers() {
    OgrePINVOKE.ScriptCompilerManager_clearTranslatorManagers(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_Ogre__ScriptTranslator getTranslator(SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AbstractNode_t node) {
    global::System.IntPtr cPtr = OgrePINVOKE.ScriptCompilerManager_getTranslator(swigCPtr, SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AbstractNode_t.getCPtr(node));
    SWIGTYPE_p_Ogre__ScriptTranslator ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Ogre__ScriptTranslator(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint registerCustomWordId(string word) {
    uint ret = OgrePINVOKE.ScriptCompilerManager_registerCustomWordId(swigCPtr, word);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void addScriptPattern(string pattern) {
    OgrePINVOKE.ScriptCompilerManager_addScriptPattern(swigCPtr, pattern);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override StringList getScriptPatterns() {
    StringList ret = new StringList(OgrePINVOKE.ScriptCompilerManager_getScriptPatterns(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void parseScript(DataStreamPtr stream, string groupName) {
    OgrePINVOKE.ScriptCompilerManager_parseScript(swigCPtr, DataStreamPtr.getCPtr(stream), groupName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override float getLoadingOrder() {
    float ret = OgrePINVOKE.ScriptCompilerManager_getLoadingOrder(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ScriptCompilerManager getSingleton() {
    ScriptCompilerManager ret = new ScriptCompilerManager(OgrePINVOKE.ScriptCompilerManager_getSingleton(), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
