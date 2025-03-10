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

public class ResourceGroupManager : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ResourceGroupManager(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ResourceGroupManager obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ResourceGroupManager obj) {
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

  ~ResourceGroupManager() {
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
          OgrePINVOKE.delete_ResourceGroupManager(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public static string DEFAULT_RESOURCE_GROUP_NAME {
    get {
      string ret = OgrePINVOKE.ResourceGroupManager_DEFAULT_RESOURCE_GROUP_NAME_get();
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static string INTERNAL_RESOURCE_GROUP_NAME {
    get {
      string ret = OgrePINVOKE.ResourceGroupManager_INTERNAL_RESOURCE_GROUP_NAME_get();
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static string AUTODETECT_RESOURCE_GROUP_NAME {
    get {
      string ret = OgrePINVOKE.ResourceGroupManager_AUTODETECT_RESOURCE_GROUP_NAME_get();
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static int RESOURCE_SYSTEM_NUM_REFERENCE_COUNTS {
    get {
      int ret = OgrePINVOKE.ResourceGroupManager_RESOURCE_SYSTEM_NUM_REFERENCE_COUNTS_get();
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public class ResourceDeclaration : global::System.IDisposable {
    private global::System.Runtime.InteropServices.HandleRef swigCPtr;
    protected bool swigCMemOwn;
  
    internal ResourceDeclaration(global::System.IntPtr cPtr, bool cMemoryOwn) {
      swigCMemOwn = cMemoryOwn;
      swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ResourceDeclaration obj) {
      return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ResourceDeclaration obj) {
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
  
    ~ResourceDeclaration() {
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
            OgrePINVOKE.delete_ResourceGroupManager_ResourceDeclaration(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public string resourceName {
      set {
        OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_resourceName_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        string ret = OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_resourceName_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public string resourceType {
      set {
        OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_resourceType_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        string ret = OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_resourceType_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ManualResourceLoader loader {
      set {
        OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_loader_set(swigCPtr, ManualResourceLoader.getCPtr(value));
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        global::System.IntPtr cPtr = OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_loader_get(swigCPtr);
        ManualResourceLoader ret = (cPtr == global::System.IntPtr.Zero) ? null : new ManualResourceLoader(cPtr, false);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public NameValueMap parameters {
      set {
        OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_parameters_set(swigCPtr, NameValueMap.getCPtr(value));
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        global::System.IntPtr cPtr = OgrePINVOKE.ResourceGroupManager_ResourceDeclaration_parameters_get(swigCPtr);
        NameValueMap ret = (cPtr == global::System.IntPtr.Zero) ? null : new NameValueMap(cPtr, false);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ResourceDeclaration() : this(OgrePINVOKE.new_ResourceGroupManager_ResourceDeclaration(), true) {
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
  }

  public class ResourceLocation : global::System.IDisposable {
    private global::System.Runtime.InteropServices.HandleRef swigCPtr;
    protected bool swigCMemOwn;
  
    internal ResourceLocation(global::System.IntPtr cPtr, bool cMemoryOwn) {
      swigCMemOwn = cMemoryOwn;
      swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ResourceLocation obj) {
      return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ResourceLocation obj) {
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
  
    ~ResourceLocation() {
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
            OgrePINVOKE.delete_ResourceGroupManager_ResourceLocation(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public Archive archive {
      set {
        OgrePINVOKE.ResourceGroupManager_ResourceLocation_archive_set(swigCPtr, Archive.getCPtr(value));
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        global::System.IntPtr cPtr = OgrePINVOKE.ResourceGroupManager_ResourceLocation_archive_get(swigCPtr);
        Archive ret = (cPtr == global::System.IntPtr.Zero) ? null : new Archive(cPtr, false);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public bool recursive {
      set {
        OgrePINVOKE.ResourceGroupManager_ResourceLocation_recursive_set(swigCPtr, value);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        bool ret = OgrePINVOKE.ResourceGroupManager_ResourceLocation_recursive_get(swigCPtr);
        if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ResourceLocation() : this(OgrePINVOKE.new_ResourceGroupManager_ResourceLocation(), true) {
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
  }

  public ResourceGroupManager() : this(OgrePINVOKE.new_ResourceGroupManager(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void createResourceGroup(string name, bool inGlobalPool) {
    OgrePINVOKE.ResourceGroupManager_createResourceGroup__SWIG_0(swigCPtr, name, inGlobalPool);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void createResourceGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_createResourceGroup__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void initialiseResourceGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_initialiseResourceGroup(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void initialiseAllResourceGroups() {
    OgrePINVOKE.ResourceGroupManager_initialiseAllResourceGroups(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void prepareResourceGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_prepareResourceGroup(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void loadResourceGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_loadResourceGroup(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadResourceGroup(string name, bool reloadableOnly) {
    OgrePINVOKE.ResourceGroupManager_unloadResourceGroup__SWIG_0(swigCPtr, name, reloadableOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadResourceGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_unloadResourceGroup__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadUnreferencedResourcesInGroup(string name, bool reloadableOnly) {
    OgrePINVOKE.ResourceGroupManager_unloadUnreferencedResourcesInGroup__SWIG_0(swigCPtr, name, reloadableOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void unloadUnreferencedResourcesInGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_unloadUnreferencedResourcesInGroup__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void clearResourceGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_clearResourceGroup(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyResourceGroup(string name) {
    OgrePINVOKE.ResourceGroupManager_destroyResourceGroup(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool isResourceGroupInitialised(string name) {
    bool ret = OgrePINVOKE.ResourceGroupManager_isResourceGroupInitialised(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isResourceGroupLoaded(string name) {
    bool ret = OgrePINVOKE.ResourceGroupManager_isResourceGroupLoaded(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool resourceGroupExists(string name) {
    bool ret = OgrePINVOKE.ResourceGroupManager_resourceGroupExists(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void addResourceLocation(string name, string locType, string resGroup, bool recursive, bool readOnly) {
    OgrePINVOKE.ResourceGroupManager_addResourceLocation__SWIG_0(swigCPtr, name, locType, resGroup, recursive, readOnly);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addResourceLocation(string name, string locType, string resGroup, bool recursive) {
    OgrePINVOKE.ResourceGroupManager_addResourceLocation__SWIG_1(swigCPtr, name, locType, resGroup, recursive);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addResourceLocation(string name, string locType, string resGroup) {
    OgrePINVOKE.ResourceGroupManager_addResourceLocation__SWIG_2(swigCPtr, name, locType, resGroup);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addResourceLocation(string name, string locType) {
    OgrePINVOKE.ResourceGroupManager_addResourceLocation__SWIG_3(swigCPtr, name, locType);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeResourceLocation(string name, string resGroup) {
    OgrePINVOKE.ResourceGroupManager_removeResourceLocation__SWIG_0(swigCPtr, name, resGroup);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeResourceLocation(string name) {
    OgrePINVOKE.ResourceGroupManager_removeResourceLocation__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool resourceLocationExists(string name, string resGroup) {
    bool ret = OgrePINVOKE.ResourceGroupManager_resourceLocationExists__SWIG_0(swigCPtr, name, resGroup);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool resourceLocationExists(string name) {
    bool ret = OgrePINVOKE.ResourceGroupManager_resourceLocationExists__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void declareResource(string name, string resourceType, string groupName, NameValueMap loadParameters) {
    OgrePINVOKE.ResourceGroupManager_declareResource__SWIG_0(swigCPtr, name, resourceType, groupName, NameValueMap.getCPtr(loadParameters));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void declareResource(string name, string resourceType, string groupName) {
    OgrePINVOKE.ResourceGroupManager_declareResource__SWIG_1(swigCPtr, name, resourceType, groupName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void declareResource(string name, string resourceType) {
    OgrePINVOKE.ResourceGroupManager_declareResource__SWIG_2(swigCPtr, name, resourceType);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void declareResource(string name, string resourceType, string groupName, ManualResourceLoader loader, NameValueMap loadParameters) {
    OgrePINVOKE.ResourceGroupManager_declareResource__SWIG_3(swigCPtr, name, resourceType, groupName, ManualResourceLoader.getCPtr(loader), NameValueMap.getCPtr(loadParameters));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void declareResource(string name, string resourceType, string groupName, ManualResourceLoader loader) {
    OgrePINVOKE.ResourceGroupManager_declareResource__SWIG_4(swigCPtr, name, resourceType, groupName, ManualResourceLoader.getCPtr(loader));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void undeclareResource(string name, string groupName) {
    OgrePINVOKE.ResourceGroupManager_undeclareResource(swigCPtr, name, groupName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public DataStreamPtr openResource(string resourceName, string groupName, Resource resourceBeingLoaded, bool throwOnFailure) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_openResource__SWIG_0(swigCPtr, resourceName, groupName, Resource.getCPtr(resourceBeingLoaded), throwOnFailure), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DataStreamPtr openResource(string resourceName, string groupName, Resource resourceBeingLoaded) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_openResource__SWIG_1(swigCPtr, resourceName, groupName, Resource.getCPtr(resourceBeingLoaded)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DataStreamPtr openResource(string resourceName, string groupName) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_openResource__SWIG_2(swigCPtr, resourceName, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DataStreamPtr openResource(string resourceName) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_openResource__SWIG_3(swigCPtr, resourceName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__listT_Ogre__SharedPtrT_Ogre__DataStream_t_t openResources(string pattern, string groupName) {
    SWIGTYPE_p_std__listT_Ogre__SharedPtrT_Ogre__DataStream_t_t ret = new SWIGTYPE_p_std__listT_Ogre__SharedPtrT_Ogre__DataStream_t_t(OgrePINVOKE.ResourceGroupManager_openResources__SWIG_0(swigCPtr, pattern, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__listT_Ogre__SharedPtrT_Ogre__DataStream_t_t openResources(string pattern) {
    SWIGTYPE_p_std__listT_Ogre__SharedPtrT_Ogre__DataStream_t_t ret = new SWIGTYPE_p_std__listT_Ogre__SharedPtrT_Ogre__DataStream_t_t(OgrePINVOKE.ResourceGroupManager_openResources__SWIG_1(swigCPtr, pattern), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public StringListPtr listResourceNames(string groupName, bool dirs) {
    StringListPtr ret = new StringListPtr(OgrePINVOKE.ResourceGroupManager_listResourceNames__SWIG_0(swigCPtr, groupName, dirs), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public StringListPtr listResourceNames(string groupName) {
    StringListPtr ret = new StringListPtr(OgrePINVOKE.ResourceGroupManager_listResourceNames__SWIG_1(swigCPtr, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t listResourceFileInfo(string groupName, bool dirs) {
    SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t ret = new SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t(OgrePINVOKE.ResourceGroupManager_listResourceFileInfo__SWIG_0(swigCPtr, groupName, dirs), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t listResourceFileInfo(string groupName) {
    SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t ret = new SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t(OgrePINVOKE.ResourceGroupManager_listResourceFileInfo__SWIG_1(swigCPtr, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public StringListPtr findResourceNames(string groupName, string pattern, bool dirs) {
    StringListPtr ret = new StringListPtr(OgrePINVOKE.ResourceGroupManager_findResourceNames__SWIG_0(swigCPtr, groupName, pattern, dirs), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public StringListPtr findResourceNames(string groupName, string pattern) {
    StringListPtr ret = new StringListPtr(OgrePINVOKE.ResourceGroupManager_findResourceNames__SWIG_1(swigCPtr, groupName, pattern), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool resourceExists(string group, string filename) {
    bool ret = OgrePINVOKE.ResourceGroupManager_resourceExists(swigCPtr, group, filename);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool resourceExistsInAnyGroup(string filename) {
    bool ret = OgrePINVOKE.ResourceGroupManager_resourceExistsInAnyGroup(swigCPtr, filename);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string findGroupContainingResource(string filename) {
    string ret = OgrePINVOKE.ResourceGroupManager_findGroupContainingResource(swigCPtr, filename);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t findResourceFileInfo(string group, string pattern, bool dirs) {
    SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t ret = new SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t(OgrePINVOKE.ResourceGroupManager_findResourceFileInfo__SWIG_0(swigCPtr, group, pattern, dirs), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t findResourceFileInfo(string group, string pattern) {
    SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t ret = new SWIGTYPE_p_Ogre__SharedPtrT_std__vectorT_Ogre__FileInfo_t_t(OgrePINVOKE.ResourceGroupManager_findResourceFileInfo__SWIG_1(swigCPtr, group, pattern), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int resourceModifiedTime(string group, string filename) {
    int ret = OgrePINVOKE.ResourceGroupManager_resourceModifiedTime(swigCPtr, group, filename);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public StringListPtr listResourceLocations(string groupName) {
    StringListPtr ret = new StringListPtr(OgrePINVOKE.ResourceGroupManager_listResourceLocations(swigCPtr, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public StringListPtr findResourceLocation(string groupName, string pattern) {
    StringListPtr ret = new StringListPtr(OgrePINVOKE.ResourceGroupManager_findResourceLocation(swigCPtr, groupName, pattern), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DataStreamPtr createResource(string filename, string groupName, bool overwrite, string locationPattern) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_createResource__SWIG_0(swigCPtr, filename, groupName, overwrite, locationPattern), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DataStreamPtr createResource(string filename, string groupName, bool overwrite) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_createResource__SWIG_1(swigCPtr, filename, groupName, overwrite), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DataStreamPtr createResource(string filename, string groupName) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_createResource__SWIG_2(swigCPtr, filename, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DataStreamPtr createResource(string filename) {
    DataStreamPtr ret = new DataStreamPtr(OgrePINVOKE.ResourceGroupManager_createResource__SWIG_3(swigCPtr, filename), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void deleteResource(string filename, string groupName, string locationPattern) {
    OgrePINVOKE.ResourceGroupManager_deleteResource__SWIG_0(swigCPtr, filename, groupName, locationPattern);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteResource(string filename, string groupName) {
    OgrePINVOKE.ResourceGroupManager_deleteResource__SWIG_1(swigCPtr, filename, groupName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteResource(string filename) {
    OgrePINVOKE.ResourceGroupManager_deleteResource__SWIG_2(swigCPtr, filename);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteMatchingResources(string filePattern, string groupName, string locationPattern) {
    OgrePINVOKE.ResourceGroupManager_deleteMatchingResources__SWIG_0(swigCPtr, filePattern, groupName, locationPattern);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteMatchingResources(string filePattern, string groupName) {
    OgrePINVOKE.ResourceGroupManager_deleteMatchingResources__SWIG_1(swigCPtr, filePattern, groupName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteMatchingResources(string filePattern) {
    OgrePINVOKE.ResourceGroupManager_deleteMatchingResources__SWIG_2(swigCPtr, filePattern);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addResourceGroupListener(ResourceGroupListener l) {
    OgrePINVOKE.ResourceGroupManager_addResourceGroupListener(swigCPtr, ResourceGroupListener.getCPtr(l));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeResourceGroupListener(ResourceGroupListener l) {
    OgrePINVOKE.ResourceGroupManager_removeResourceGroupListener(swigCPtr, ResourceGroupListener.getCPtr(l));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setWorldResourceGroupName(string groupName) {
    OgrePINVOKE.ResourceGroupManager_setWorldResourceGroupName(swigCPtr, groupName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getWorldResourceGroupName() {
    string ret = OgrePINVOKE.ResourceGroupManager_getWorldResourceGroupName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setCustomStagesForResourceGroup(string group, uint stageCount) {
    OgrePINVOKE.ResourceGroupManager_setCustomStagesForResourceGroup(swigCPtr, group, stageCount);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getCustomStagesForResourceGroup(string group) {
    uint ret = OgrePINVOKE.ResourceGroupManager_getCustomStagesForResourceGroup(swigCPtr, group);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isResourceGroupInGlobalPool(string name) {
    bool ret = OgrePINVOKE.ResourceGroupManager_isResourceGroupInGlobalPool(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void shutdownAll() {
    OgrePINVOKE.ResourceGroupManager_shutdownAll(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _registerResourceManager(string resourceType, ResourceManager rm) {
    OgrePINVOKE.ResourceGroupManager__registerResourceManager(swigCPtr, resourceType, ResourceManager.getCPtr(rm));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _unregisterResourceManager(string resourceType) {
    OgrePINVOKE.ResourceGroupManager__unregisterResourceManager(swigCPtr, resourceType);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__mapT_std__string_Ogre__ResourceManager_p_std__lessT_std__string_t_t getResourceManagers() {
    SWIGTYPE_p_std__mapT_std__string_Ogre__ResourceManager_p_std__lessT_std__string_t_t ret = new SWIGTYPE_p_std__mapT_std__string_Ogre__ResourceManager_p_std__lessT_std__string_t_t(OgrePINVOKE.ResourceGroupManager_getResourceManagers(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _registerScriptLoader(ScriptLoader su) {
    OgrePINVOKE.ResourceGroupManager__registerScriptLoader(swigCPtr, ScriptLoader.getCPtr(su));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _unregisterScriptLoader(ScriptLoader su) {
    OgrePINVOKE.ResourceGroupManager__unregisterScriptLoader(swigCPtr, ScriptLoader.getCPtr(su));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ScriptLoader _findScriptLoader(string pattern) {
    global::System.IntPtr cPtr = OgrePINVOKE.ResourceGroupManager__findScriptLoader(swigCPtr, pattern);
    ScriptLoader ret = (cPtr == global::System.IntPtr.Zero) ? null : new ScriptLoader(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ResourceManager _getResourceManager(string resourceType) {
    global::System.IntPtr cPtr = OgrePINVOKE.ResourceGroupManager__getResourceManager(swigCPtr, resourceType);
    ResourceManager ret = (cPtr == global::System.IntPtr.Zero) ? null : new ResourceManager(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _notifyResourceCreated(ResourcePtr res) {
    OgrePINVOKE.ResourceGroupManager__notifyResourceCreated(swigCPtr, ResourcePtr.getCPtr(res));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyResourceRemoved(ResourcePtr res) {
    OgrePINVOKE.ResourceGroupManager__notifyResourceRemoved(swigCPtr, ResourcePtr.getCPtr(res));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyResourceGroupChanged(string oldGroup, Resource res) {
    OgrePINVOKE.ResourceGroupManager__notifyResourceGroupChanged(swigCPtr, oldGroup, Resource.getCPtr(res));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyAllResourcesRemoved(ResourceManager manager) {
    OgrePINVOKE.ResourceGroupManager__notifyAllResourcesRemoved(swigCPtr, ResourceManager.getCPtr(manager));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyCustomStageStarted(string description) {
    OgrePINVOKE.ResourceGroupManager__notifyCustomStageStarted(swigCPtr, description);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyCustomStageEnded() {
    OgrePINVOKE.ResourceGroupManager__notifyCustomStageEnded(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public StringList getResourceGroups() {
    StringList ret = new StringList(OgrePINVOKE.ResourceGroupManager_getResourceGroups(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__listT_Ogre__ResourceGroupManager__ResourceDeclaration_t getResourceDeclarationList(string groupName) {
    SWIGTYPE_p_std__listT_Ogre__ResourceGroupManager__ResourceDeclaration_t ret = new SWIGTYPE_p_std__listT_Ogre__ResourceGroupManager__ResourceDeclaration_t(OgrePINVOKE.ResourceGroupManager_getResourceDeclarationList(swigCPtr, groupName), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_Ogre__ResourceGroupManager__ResourceLocation_t getResourceLocationList(string groupName) {
    SWIGTYPE_p_std__vectorT_Ogre__ResourceGroupManager__ResourceLocation_t ret = new SWIGTYPE_p_std__vectorT_Ogre__ResourceGroupManager__ResourceLocation_t(OgrePINVOKE.ResourceGroupManager_getResourceLocationList(swigCPtr, groupName), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setLoadingListener(ResourceLoadingListener listener) {
    OgrePINVOKE.ResourceGroupManager_setLoadingListener(swigCPtr, ResourceLoadingListener.getCPtr(listener));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ResourceLoadingListener getLoadingListener() {
    global::System.IntPtr cPtr = OgrePINVOKE.ResourceGroupManager_getLoadingListener(swigCPtr);
    ResourceLoadingListener ret = (cPtr == global::System.IntPtr.Zero) ? null : new ResourceLoadingListener(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ResourceGroupManager getSingleton() {
    ResourceGroupManager ret = new ResourceGroupManager(OgrePINVOKE.ResourceGroupManager_getSingleton(), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
