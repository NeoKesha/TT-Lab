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

public class ResourcePtr : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ResourcePtr(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ResourcePtr obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ResourcePtr obj) {
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

  ~ResourcePtr() {
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
          OgrePINVOKE.delete_ResourcePtr(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ResourcePtr(SWIGTYPE_p_std__nullptr_t arg0) : this(OgrePINVOKE.new_ResourcePtr__SWIG_0(SWIGTYPE_p_std__nullptr_t.getCPtr(arg0)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ResourcePtr() : this(OgrePINVOKE.new_ResourcePtr__SWIG_1(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ResourcePtr(ResourcePtr r) : this(OgrePINVOKE.new_ResourcePtr__SWIG_2(ResourcePtr.getCPtr(r)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Resource __deref__() {
    global::System.IntPtr cPtr = OgrePINVOKE.ResourcePtr___deref__(swigCPtr);
    Resource ret = (cPtr == global::System.IntPtr.Zero) ? null : new Resource(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr(Resource underlyingClass) : this(OgrePINVOKE.new_ResourcePtr__SWIG_3(Resource.getCPtr(underlyingClass)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void prepare(bool backgroundThread) {
    OgrePINVOKE.ResourcePtr_prepare__SWIG_0(swigCPtr, backgroundThread);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void prepare() {
    OgrePINVOKE.ResourcePtr_prepare__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void load(bool backgroundThread) {
    OgrePINVOKE.ResourcePtr_load__SWIG_0(swigCPtr, backgroundThread);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void load() {
    OgrePINVOKE.ResourcePtr_load__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void reload(Resource.LoadingFlags flags) {
    OgrePINVOKE.ResourcePtr_reload__SWIG_0(swigCPtr, (int)flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void reload() {
    OgrePINVOKE.ResourcePtr_reload__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool isReloadable() {
    bool ret = OgrePINVOKE.ResourcePtr_isReloadable(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isManuallyLoaded() {
    bool ret = OgrePINVOKE.ResourcePtr_isManuallyLoaded(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void unload() {
    OgrePINVOKE.ResourcePtr_unload(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getSize() {
    uint ret = OgrePINVOKE.ResourcePtr_getSize(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void touch() {
    OgrePINVOKE.ResourcePtr_touch(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getName() {
    string ret = OgrePINVOKE.ResourcePtr_getName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getHandle() {
    uint ret = OgrePINVOKE.ResourcePtr_getHandle(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isPrepared() {
    bool ret = OgrePINVOKE.ResourcePtr_isPrepared(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isLoaded() {
    bool ret = OgrePINVOKE.ResourcePtr_isLoaded(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isLoading() {
    bool ret = OgrePINVOKE.ResourcePtr_isLoading(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Resource.LoadingState getLoadingState() {
    Resource.LoadingState ret = (Resource.LoadingState)OgrePINVOKE.ResourcePtr_getLoadingState(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isBackgroundLoaded() {
    bool ret = OgrePINVOKE.ResourcePtr_isBackgroundLoaded(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setBackgroundLoaded(bool bl) {
    OgrePINVOKE.ResourcePtr_setBackgroundLoaded(swigCPtr, bl);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void escalateLoading() {
    OgrePINVOKE.ResourcePtr_escalateLoading(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addListener(Resource.Listener lis) {
    OgrePINVOKE.ResourcePtr_addListener(swigCPtr, Resource.Listener.getCPtr(lis));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeListener(Resource.Listener lis) {
    OgrePINVOKE.ResourcePtr_removeListener(swigCPtr, Resource.Listener.getCPtr(lis));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getGroup() {
    string ret = OgrePINVOKE.ResourcePtr_getGroup(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void changeGroupOwnership(string newGroup) {
    OgrePINVOKE.ResourcePtr_changeGroupOwnership(swigCPtr, newGroup);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ResourceManager getCreator() {
    global::System.IntPtr cPtr = OgrePINVOKE.ResourcePtr_getCreator(swigCPtr);
    ResourceManager ret = (cPtr == global::System.IntPtr.Zero) ? null : new ResourceManager(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getOrigin() {
    string ret = OgrePINVOKE.ResourcePtr_getOrigin(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _notifyOrigin(string origin) {
    OgrePINVOKE.ResourcePtr__notifyOrigin(swigCPtr, origin);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getStateCount() {
    uint ret = OgrePINVOKE.ResourcePtr_getStateCount(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _dirtyState() {
    OgrePINVOKE.ResourcePtr__dirtyState(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _fireLoadingComplete(bool unused) {
    OgrePINVOKE.ResourcePtr__fireLoadingComplete__SWIG_0(swigCPtr, unused);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _fireLoadingComplete() {
    OgrePINVOKE.ResourcePtr__fireLoadingComplete__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _firePreparingComplete(bool unused) {
    OgrePINVOKE.ResourcePtr__firePreparingComplete__SWIG_0(swigCPtr, unused);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _firePreparingComplete() {
    OgrePINVOKE.ResourcePtr__firePreparingComplete__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _fireUnloadingComplete() {
    OgrePINVOKE.ResourcePtr__fireUnloadingComplete(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ParamDictionary getParamDictionary() {
    global::System.IntPtr cPtr = OgrePINVOKE.ResourcePtr_getParamDictionary__SWIG_0(swigCPtr);
    ParamDictionary ret = (cPtr == global::System.IntPtr.Zero) ? null : new ParamDictionary(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public StringList getParameters() {
    StringList ret = new StringList(OgrePINVOKE.ResourcePtr_getParameters(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool setParameter(string name, string value) {
    bool ret = OgrePINVOKE.ResourcePtr_setParameter(swigCPtr, name, value);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setParameterList(NameValueMap paramList) {
    OgrePINVOKE.ResourcePtr_setParameterList(swigCPtr, NameValueMap.getCPtr(paramList));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getParameter(string name) {
    string ret = OgrePINVOKE.ResourcePtr_getParameter(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void copyParametersTo(StringInterface dest) {
    OgrePINVOKE.ResourcePtr_copyParametersTo(swigCPtr, StringInterface.getCPtr(dest));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void cleanupDictionary() {
    OgrePINVOKE.ResourcePtr_cleanupDictionary(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
