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

public class CompositionTechnique : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CompositionTechnique(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CompositionTechnique obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(CompositionTechnique obj) {
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

  ~CompositionTechnique() {
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
          OgrePINVOKE.delete_CompositionTechnique(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public CompositionTechnique(Compositor parent) : this(OgrePINVOKE.new_CompositionTechnique(Compositor.getCPtr(parent)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public class TextureDefinition : global::System.IDisposable {
    private global::System.Runtime.InteropServices.HandleRef swigCPtr;
    protected bool swigCMemOwn;
  
    internal TextureDefinition(global::System.IntPtr cPtr, bool cMemoryOwn) {
      swigCMemOwn = cMemoryOwn;
      swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TextureDefinition obj) {
      return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef swigRelease(TextureDefinition obj) {
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
  
    ~TextureDefinition() {
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
            OgrePINVOKE.delete_CompositionTechnique_TextureDefinition(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public string name {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_name_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        string ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_name_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public string refCompName {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_refCompName_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        string ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_refCompName_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public string refTexName {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_refTexName_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        string ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_refTexName_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public uint width {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_width_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        uint ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_width_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public uint height {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_height_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        uint ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_height_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public TextureType type {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_type_set(swigCPtr, (int)value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        TextureType ret = (TextureType)OgrePINVOKE.CompositionTechnique_TextureDefinition_type_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public float widthFactor {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_widthFactor_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        float ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_widthFactor_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public float heightFactor {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_heightFactor_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        float ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_heightFactor_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public SWIGTYPE_p_std__vectorT_Ogre__PixelFormat_t formatList {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_formatList_set(swigCPtr, SWIGTYPE_p_std__vectorT_Ogre__PixelFormat_t.getCPtr(value));
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        global::System.IntPtr cPtr = OgrePINVOKE.CompositionTechnique_TextureDefinition_formatList_get(swigCPtr);
        SWIGTYPE_p_std__vectorT_Ogre__PixelFormat_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_Ogre__PixelFormat_t(cPtr, false);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public byte fsaa {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_fsaa_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        byte ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_fsaa_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public bool hwGammaWrite {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_hwGammaWrite_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        bool ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_hwGammaWrite_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ushort depthBufferId {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_depthBufferId_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        ushort ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_depthBufferId_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public bool pooled {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_pooled_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        bool ret = OgrePINVOKE.CompositionTechnique_TextureDefinition_pooled_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public CompositionTechnique.TextureScope scope {
      set {
        OgrePINVOKE.CompositionTechnique_TextureDefinition_scope_set(swigCPtr, (int)value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        CompositionTechnique.TextureScope ret = (CompositionTechnique.TextureScope)OgrePINVOKE.CompositionTechnique_TextureDefinition_scope_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public TextureDefinition() : this(OgrePINVOKE.new_CompositionTechnique_TextureDefinition(), true) {
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
  }

  public CompositionTechnique.TextureDefinition createTextureDefinition(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositionTechnique_createTextureDefinition(swigCPtr, name);
    CompositionTechnique.TextureDefinition ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique.TextureDefinition(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void removeTextureDefinition(uint idx) {
    OgrePINVOKE.CompositionTechnique_removeTextureDefinition(swigCPtr, idx);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public CompositionTechnique.TextureDefinition getTextureDefinition(uint idx) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositionTechnique_getTextureDefinition__SWIG_0(swigCPtr, idx);
    CompositionTechnique.TextureDefinition ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique.TextureDefinition(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositionTechnique.TextureDefinition getTextureDefinition(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositionTechnique_getTextureDefinition__SWIG_1(swigCPtr, name);
    CompositionTechnique.TextureDefinition ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTechnique.TextureDefinition(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void removeAllTextureDefinitions() {
    OgrePINVOKE.CompositionTechnique_removeAllTextureDefinitions(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__vectorT_Ogre__CompositionTechnique__TextureDefinition_p_t getTextureDefinitions() {
    SWIGTYPE_p_std__vectorT_Ogre__CompositionTechnique__TextureDefinition_p_t ret = new SWIGTYPE_p_std__vectorT_Ogre__CompositionTechnique__TextureDefinition_p_t(OgrePINVOKE.CompositionTechnique_getTextureDefinitions(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositionTargetPass createTargetPass() {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositionTechnique_createTargetPass(swigCPtr);
    CompositionTargetPass ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTargetPass(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void removeTargetPass(uint idx) {
    OgrePINVOKE.CompositionTechnique_removeTargetPass(swigCPtr, idx);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeAllTargetPasses() {
    OgrePINVOKE.CompositionTechnique_removeAllTargetPasses(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__vectorT_Ogre__CompositionTargetPass_p_t getTargetPasses() {
    SWIGTYPE_p_std__vectorT_Ogre__CompositionTargetPass_p_t ret = new SWIGTYPE_p_std__vectorT_Ogre__CompositionTargetPass_p_t(OgrePINVOKE.CompositionTechnique_getTargetPasses(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CompositionTargetPass getOutputTargetPass() {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositionTechnique_getOutputTargetPass(swigCPtr);
    CompositionTargetPass ret = (cPtr == global::System.IntPtr.Zero) ? null : new CompositionTargetPass(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool isSupported(bool allowTextureDegradation) {
    bool ret = OgrePINVOKE.CompositionTechnique_isSupported(swigCPtr, allowTextureDegradation);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void setSchemeName(string schemeName) {
    OgrePINVOKE.CompositionTechnique_setSchemeName(swigCPtr, schemeName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getSchemeName() {
    string ret = OgrePINVOKE.CompositionTechnique_getSchemeName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setCompositorLogicName(string compositorLogicName) {
    OgrePINVOKE.CompositionTechnique_setCompositorLogicName(swigCPtr, compositorLogicName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getCompositorLogicName() {
    string ret = OgrePINVOKE.CompositionTechnique_getCompositorLogicName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Compositor getParent() {
    global::System.IntPtr cPtr = OgrePINVOKE.CompositionTechnique_getParent(swigCPtr);
    Compositor ret = (cPtr == global::System.IntPtr.Zero) ? null : new Compositor(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public enum TextureScope {
    TS_LOCAL,
    TS_CHAIN,
    TS_GLOBAL
  }

}

}
