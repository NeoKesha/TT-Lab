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

public class ResourceManager : ScriptLoader {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal ResourceManager(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.ResourceManager_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ResourceManager obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ResourceManager obj) {
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
          OgrePINVOKE.delete_ResourceManager(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ResourcePtr createResource(string name, string group, bool isManual, ManualResourceLoader loader, NameValueMap createParams) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_createResource__SWIG_0(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(createParams)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr createResource(string name, string group, bool isManual, ManualResourceLoader loader) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_createResource__SWIG_1(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr createResource(string name, string group, bool isManual) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_createResource__SWIG_2(swigCPtr, name, group, isManual), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr createResource(string name, string group) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_createResource__SWIG_3(swigCPtr, name, group), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t createOrRetrieve(string name, string group, bool isManual, ManualResourceLoader loader, NameValueMap createParams) {
    SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t ret = new SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t(OgrePINVOKE.ResourceManager_createOrRetrieve__SWIG_0(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(createParams)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t createOrRetrieve(string name, string group, bool isManual, ManualResourceLoader loader) {
    SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t ret = new SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t(OgrePINVOKE.ResourceManager_createOrRetrieve__SWIG_1(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t createOrRetrieve(string name, string group, bool isManual) {
    SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t ret = new SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t(OgrePINVOKE.ResourceManager_createOrRetrieve__SWIG_2(swigCPtr, name, group, isManual), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t createOrRetrieve(string name, string group) {
    SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t ret = new SWIGTYPE_p_std__pairT_Ogre__SharedPtrT_Ogre__Resource_t_bool_t(OgrePINVOKE.ResourceManager_createOrRetrieve__SWIG_3(swigCPtr, name, group), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setMemoryBudget(uint bytes) {
    OgrePINVOKE.ResourceManager_setMemoryBudget(swigCPtr, bytes);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getMemoryBudget() {
    uint ret = OgrePINVOKE.ResourceManager_getMemoryBudget(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getMemoryUsage() {
    uint ret = OgrePINVOKE.ResourceManager_getMemoryUsage(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void unload(string name, string group) {
    OgrePINVOKE.ResourceManager_unload__SWIG_0(swigCPtr, name, group);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unload(string name) {
    OgrePINVOKE.ResourceManager_unload__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unload(uint handle) {
    OgrePINVOKE.ResourceManager_unload__SWIG_2(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadAll(bool reloadableOnly) {
    OgrePINVOKE.ResourceManager_unloadAll__SWIG_0(swigCPtr, reloadableOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadAll() {
    OgrePINVOKE.ResourceManager_unloadAll__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void reloadAll(bool reloadableOnly) {
    OgrePINVOKE.ResourceManager_reloadAll__SWIG_0(swigCPtr, reloadableOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void reloadAll() {
    OgrePINVOKE.ResourceManager_reloadAll__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadUnreferencedResources(bool reloadableOnly) {
    OgrePINVOKE.ResourceManager_unloadUnreferencedResources__SWIG_0(swigCPtr, reloadableOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadUnreferencedResources() {
    OgrePINVOKE.ResourceManager_unloadUnreferencedResources__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void reloadUnreferencedResources(bool reloadableOnly) {
    OgrePINVOKE.ResourceManager_reloadUnreferencedResources__SWIG_0(swigCPtr, reloadableOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void reloadUnreferencedResources() {
    OgrePINVOKE.ResourceManager_reloadUnreferencedResources__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void unloadAll(Resource.LoadingFlags flags) {
    OgrePINVOKE.ResourceManager_unloadAll__SWIG_2(swigCPtr, (int)flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void reloadAll(Resource.LoadingFlags flags) {
    OgrePINVOKE.ResourceManager_reloadAll__SWIG_2(swigCPtr, (int)flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void remove(ResourcePtr r) {
    OgrePINVOKE.ResourceManager_remove__SWIG_0(swigCPtr, ResourcePtr.getCPtr(r));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void remove(string name, string group) {
    OgrePINVOKE.ResourceManager_remove__SWIG_1(swigCPtr, name, group);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void remove(string name) {
    OgrePINVOKE.ResourceManager_remove__SWIG_2(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void remove(uint handle) {
    OgrePINVOKE.ResourceManager_remove__SWIG_3(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void removeAll() {
    OgrePINVOKE.ResourceManager_removeAll(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void removeUnreferencedResources(bool reloadableOnly) {
    OgrePINVOKE.ResourceManager_removeUnreferencedResources__SWIG_0(swigCPtr, reloadableOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void removeUnreferencedResources() {
    OgrePINVOKE.ResourceManager_removeUnreferencedResources__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual ResourcePtr getResourceByName(string name, string groupName) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_getResourceByName__SWIG_0(swigCPtr, name, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ResourcePtr getResourceByName(string name) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_getResourceByName__SWIG_1(swigCPtr, name), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ResourcePtr getByHandle(uint handle) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_getByHandle(swigCPtr, handle), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool resourceExists(string name, string group) {
    bool ret = OgrePINVOKE.ResourceManager_resourceExists__SWIG_0(swigCPtr, name, group);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool resourceExists(string name) {
    bool ret = OgrePINVOKE.ResourceManager_resourceExists__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool resourceExists(uint handle) {
    bool ret = OgrePINVOKE.ResourceManager_resourceExists__SWIG_2(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void _notifyResourceTouched(Resource res) {
    OgrePINVOKE.ResourceManager__notifyResourceTouched(swigCPtr, Resource.getCPtr(res));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void _notifyResourceLoaded(Resource res) {
    OgrePINVOKE.ResourceManager__notifyResourceLoaded(swigCPtr, Resource.getCPtr(res));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void _notifyResourceUnloaded(Resource res) {
    OgrePINVOKE.ResourceManager__notifyResourceUnloaded(swigCPtr, Resource.getCPtr(res));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ResourcePtr prepare(string name, string group, bool isManual, ManualResourceLoader loader, NameValueMap loadParams, bool backgroundThread) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_prepare__SWIG_0(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(loadParams), backgroundThread), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr prepare(string name, string group, bool isManual, ManualResourceLoader loader, NameValueMap loadParams) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_prepare__SWIG_1(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(loadParams)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr prepare(string name, string group, bool isManual, ManualResourceLoader loader) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_prepare__SWIG_2(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr prepare(string name, string group, bool isManual) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_prepare__SWIG_3(swigCPtr, name, group, isManual), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr prepare(string name, string group) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_prepare__SWIG_4(swigCPtr, name, group), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr load(string name, string group, bool isManual, ManualResourceLoader loader, NameValueMap loadParams, bool backgroundThread) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_load__SWIG_0(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(loadParams), backgroundThread), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr load(string name, string group, bool isManual, ManualResourceLoader loader, NameValueMap loadParams) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_load__SWIG_1(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(loadParams)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr load(string name, string group, bool isManual, ManualResourceLoader loader) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_load__SWIG_2(swigCPtr, name, group, isManual, ManualResourceLoader.getCPtr(loader)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr load(string name, string group, bool isManual) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_load__SWIG_3(swigCPtr, name, group, isManual), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourcePtr load(string name, string group) {
    ResourcePtr ret = new ResourcePtr(OgrePINVOKE.ResourceManager_load__SWIG_4(swigCPtr, name, group), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override StringList getScriptPatterns() {
    StringList ret = new StringList(OgrePINVOKE.ResourceManager_getScriptPatterns(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void parseScript(DataStreamPtr stream, string groupName) {
    OgrePINVOKE.ResourceManager_parseScript(swigCPtr, DataStreamPtr.getCPtr(stream), groupName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override float getLoadingOrder() {
    float ret = OgrePINVOKE.ResourceManager_getLoadingOrder(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getResourceType() {
    string ret = OgrePINVOKE.ResourceManager_getResourceType(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setVerbose(bool v) {
    OgrePINVOKE.ResourceManager_setVerbose(swigCPtr, v);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getVerbose() {
    bool ret = OgrePINVOKE.ResourceManager_getVerbose(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public class ResourcePool : global::System.IDisposable {
    private global::System.Runtime.InteropServices.HandleRef swigCPtr;
    protected bool swigCMemOwn;
  
    internal ResourcePool(global::System.IntPtr cPtr, bool cMemoryOwn) {
      swigCMemOwn = cMemoryOwn;
      swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ResourcePool obj) {
      return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ResourcePool obj) {
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
  
    ~ResourcePool() {
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
            OgrePINVOKE.delete_ResourceManager_ResourcePool(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public ResourcePool(string name) : this(OgrePINVOKE.new_ResourceManager_ResourcePool(name), true) {
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public string getName() {
      string ret = OgrePINVOKE.ResourceManager_ResourcePool_getName(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
  
    public void clear() {
      OgrePINVOKE.ResourceManager_ResourcePool_clear(swigCPtr);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
  }

  public ResourceManager.ResourcePool getResourcePool(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.ResourceManager_getResourcePool(swigCPtr, name);
    ResourceManager.ResourcePool ret = (cPtr == global::System.IntPtr.Zero) ? null : new ResourceManager.ResourcePool(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void destroyResourcePool(ResourceManager.ResourcePool pool) {
    OgrePINVOKE.ResourceManager_destroyResourcePool__SWIG_0(swigCPtr, ResourceManager.ResourcePool.getCPtr(pool));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyResourcePool(string name) {
    OgrePINVOKE.ResourceManager_destroyResourcePool__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyAllResourcePools() {
    OgrePINVOKE.ResourceManager_destroyAllResourcePools(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
