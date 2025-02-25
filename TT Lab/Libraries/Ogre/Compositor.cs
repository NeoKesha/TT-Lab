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

public class Compositor : Resource {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal Compositor(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.Compositor_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Compositor obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Compositor obj) {
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
          OgrePINVOKE.delete_Compositor(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public Compositor(ResourceManager creator, string name, uint handle, string group, bool isManual, ManualResourceLoader loader) : this(OgrePINVOKE.new_Compositor__SWIG_0(ResourceManager.getCPtr(creator), name, handle, group, isManual, ManualResourceLoader.getCPtr(loader)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Compositor(ResourceManager creator, string name, uint handle, string group, bool isManual) : this(OgrePINVOKE.new_Compositor__SWIG_1(ResourceManager.getCPtr(creator), name, handle, group, isManual), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Compositor(ResourceManager creator, string name, uint handle, string group) : this(OgrePINVOKE.new_Compositor__SWIG_2(ResourceManager.getCPtr(creator), name, handle, group), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public CompositionTechnique createTechnique() {
    global::System.IntPtr cPtr = OgrePINVOKE.Compositor_createTechnique(swigCPtr);
    CompositionTechnique ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void removeTechnique(uint idx) {
    OgrePINVOKE.Compositor_removeTechnique(swigCPtr, idx);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public CompositionTechnique getTechnique(uint idx) {
    global::System.IntPtr cPtr = OgrePINVOKE.Compositor_getTechnique(swigCPtr, idx);
    CompositionTechnique ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getNumTechniques() {
    uint ret = OgrePINVOKE.Compositor_getNumTechniques(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void removeAllTechniques() {
    OgrePINVOKE.Compositor_removeAllTechniques(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public CompositionTechnique getSupportedTechnique(uint idx) {
    global::System.IntPtr cPtr = OgrePINVOKE.Compositor_getSupportedTechnique__SWIG_0(swigCPtr, idx);
    CompositionTechnique ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getNumSupportedTechniques() {
    uint ret = OgrePINVOKE.Compositor_getNumSupportedTechniques(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositionTechnique getSupportedTechnique(string schemeName) {
    global::System.IntPtr cPtr = OgrePINVOKE.Compositor_getSupportedTechnique__SWIG_1(swigCPtr, schemeName);
    CompositionTechnique ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositionTechnique getSupportedTechnique() {
    global::System.IntPtr cPtr = OgrePINVOKE.Compositor_getSupportedTechnique__SWIG_2(swigCPtr);
    CompositionTechnique ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getTextureInstanceName(string name, uint mrtIndex) {
    string ret = OgrePINVOKE.Compositor_getTextureInstanceName(swigCPtr, name, mrtIndex);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public TexturePtr getTextureInstance(string name, uint mrtIndex) {
    TexturePtr ret = new TexturePtr(OgrePINVOKE.Compositor_getTextureInstance(swigCPtr, name, mrtIndex), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public RenderTarget getRenderTarget(string name, int slice) {
    global::System.IntPtr cPtr = OgrePINVOKE.Compositor_getRenderTarget__SWIG_0(swigCPtr, name, slice);
    RenderTarget ret = (cPtr == global::System.IntPtr.Zero) ? null : new RenderTarget(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public RenderTarget getRenderTarget(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.Compositor_getRenderTarget__SWIG_1(swigCPtr, name);
    RenderTarget ret = (cPtr == global::System.IntPtr.Zero) ? null : new RenderTarget(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
