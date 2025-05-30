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

public class SubRenderStateFactory : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SubRenderStateFactory(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SubRenderStateFactory obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(SubRenderStateFactory obj) {
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

  ~SubRenderStateFactory() {
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
          RTShaderPINVOKE.delete_SubRenderStateFactory(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public virtual string getType() {
    string ret = RTShaderPINVOKE.SubRenderStateFactory_getType(swigCPtr);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SubRenderState createInstance() {
    global::System.IntPtr cPtr = RTShaderPINVOKE.SubRenderStateFactory_createInstance__SWIG_0(swigCPtr);
    SubRenderState ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubRenderState(cPtr, false);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SubRenderState createInstance(ScriptCompiler compiler, PropertyAbstractNode prop, Pass pass, SGScriptTranslator translator) {
    global::System.IntPtr cPtr = RTShaderPINVOKE.SubRenderStateFactory_createInstance__SWIG_1(swigCPtr, ScriptCompiler.getCPtr(compiler), PropertyAbstractNode.getCPtr(prop), Pass.getCPtr(pass), SGScriptTranslator.getCPtr(translator));
    SubRenderState ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubRenderState(cPtr, false);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SubRenderState createInstance(ScriptCompiler compiler, PropertyAbstractNode prop, TextureUnitState texState, SGScriptTranslator translator) {
    global::System.IntPtr cPtr = RTShaderPINVOKE.SubRenderStateFactory_createInstance__SWIG_2(swigCPtr, ScriptCompiler.getCPtr(compiler), PropertyAbstractNode.getCPtr(prop), TextureUnitState.getCPtr(texState), SGScriptTranslator.getCPtr(translator));
    SubRenderState ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubRenderState(cPtr, false);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual SubRenderState createOrRetrieveInstance(SGScriptTranslator translator) {
    global::System.IntPtr cPtr = RTShaderPINVOKE.SubRenderStateFactory_createOrRetrieveInstance(swigCPtr, SGScriptTranslator.getCPtr(translator));
    SubRenderState ret = (cPtr == global::System.IntPtr.Zero) ? null : new SubRenderState(cPtr, false);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void destroyInstance(SubRenderState subRenderState) {
    RTShaderPINVOKE.SubRenderStateFactory_destroyInstance(swigCPtr, SubRenderState.getCPtr(subRenderState));
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void destroyAllInstances() {
    RTShaderPINVOKE.SubRenderStateFactory_destroyAllInstances(swigCPtr);
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void writeInstance(MaterialSerializer ser, SubRenderState subRenderState, Pass srcPass, Pass dstPass) {
    RTShaderPINVOKE.SubRenderStateFactory_writeInstance__SWIG_0(swigCPtr, MaterialSerializer.getCPtr(ser), SubRenderState.getCPtr(subRenderState), Pass.getCPtr(srcPass), Pass.getCPtr(dstPass));
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void writeInstance(MaterialSerializer ser, SubRenderState subRenderState, TextureUnitState srcTextureUnit, TextureUnitState dstTextureUnit) {
    RTShaderPINVOKE.SubRenderStateFactory_writeInstance__SWIG_1(swigCPtr, MaterialSerializer.getCPtr(ser), SubRenderState.getCPtr(subRenderState), TextureUnitState.getCPtr(srcTextureUnit), TextureUnitState.getCPtr(dstTextureUnit));
    if (RTShaderPINVOKE.SWIGPendingException.Pending) throw RTShaderPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
