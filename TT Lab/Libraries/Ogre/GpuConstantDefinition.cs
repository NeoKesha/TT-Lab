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

public class GpuConstantDefinition : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GpuConstantDefinition(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GpuConstantDefinition obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(GpuConstantDefinition obj) {
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

  ~GpuConstantDefinition() {
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
          OgrePINVOKE.delete_GpuConstantDefinition(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public uint physicalIndex {
    set {
      OgrePINVOKE.GpuConstantDefinition_physicalIndex_set(swigCPtr, value);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      uint ret = OgrePINVOKE.GpuConstantDefinition_physicalIndex_get(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public uint logicalIndex {
    set {
      OgrePINVOKE.GpuConstantDefinition_logicalIndex_set(swigCPtr, value);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      uint ret = OgrePINVOKE.GpuConstantDefinition_logicalIndex_get(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public uint elementSize {
    set {
      OgrePINVOKE.GpuConstantDefinition_elementSize_set(swigCPtr, value);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      uint ret = OgrePINVOKE.GpuConstantDefinition_elementSize_get(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public uint arraySize {
    set {
      OgrePINVOKE.GpuConstantDefinition_arraySize_set(swigCPtr, value);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      uint ret = OgrePINVOKE.GpuConstantDefinition_arraySize_get(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_Ogre__GpuConstantType constType {
    set {
      OgrePINVOKE.GpuConstantDefinition_constType_set(swigCPtr, (int)value);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SWIGTYPE_Ogre__GpuConstantType ret = (SWIGTYPE_Ogre__GpuConstantType)OgrePINVOKE.GpuConstantDefinition_constType_get(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ushort variability {
    set {
      OgrePINVOKE.GpuConstantDefinition_variability_set(swigCPtr, value);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      ushort ret = OgrePINVOKE.GpuConstantDefinition_variability_get(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool isFloat() {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isFloat__SWIG_0(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static bool isFloat(SWIGTYPE_Ogre__GpuConstantType c) {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isFloat__SWIG_1((int)c);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isDouble() {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isDouble__SWIG_0(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static bool isDouble(SWIGTYPE_Ogre__GpuConstantType c) {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isDouble__SWIG_1((int)c);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isInt() {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isInt__SWIG_0(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static bool isInt(SWIGTYPE_Ogre__GpuConstantType c) {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isInt__SWIG_1((int)c);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isUnsignedInt() {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isUnsignedInt__SWIG_0(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static bool isUnsignedInt(SWIGTYPE_Ogre__GpuConstantType c) {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isUnsignedInt__SWIG_1((int)c);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isBool() {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isBool__SWIG_0(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static bool isBool(SWIGTYPE_Ogre__GpuConstantType c) {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isBool__SWIG_1((int)c);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isSampler() {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isSampler__SWIG_0(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static bool isSampler(SWIGTYPE_Ogre__GpuConstantType c) {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isSampler__SWIG_1((int)c);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isSpecialization() {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isSpecialization__SWIG_0(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static bool isSpecialization(SWIGTYPE_Ogre__GpuConstantType c) {
    bool ret = OgrePINVOKE.GpuConstantDefinition_isSpecialization__SWIG_1((int)c);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static BaseConstantType getBaseType(SWIGTYPE_Ogre__GpuConstantType ctype) {
    BaseConstantType ret = (BaseConstantType)OgrePINVOKE.GpuConstantDefinition_getBaseType((int)ctype);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static uint getElementSize(SWIGTYPE_Ogre__GpuConstantType ctype, bool padToMultiplesOf4) {
    uint ret = OgrePINVOKE.GpuConstantDefinition_getElementSize((int)ctype, padToMultiplesOf4);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public GpuConstantDefinition() : this(OgrePINVOKE.new_GpuConstantDefinition(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
