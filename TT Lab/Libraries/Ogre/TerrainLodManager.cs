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

public class TerrainLodManager : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal TerrainLodManager(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TerrainLodManager obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(TerrainLodManager obj) {
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

  ~TerrainLodManager() {
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
          OgreTerrainPINVOKE.delete_TerrainLodManager(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public static uint TERRAINLODDATA_CHUNK_ID {
    get {
      uint ret = OgreTerrainPINVOKE.TerrainLodManager_TERRAINLODDATA_CHUNK_ID_get();
      if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static ushort TERRAINLODDATA_CHUNK_VERSION {
    get {
      ushort ret = OgreTerrainPINVOKE.TerrainLodManager_TERRAINLODDATA_CHUNK_VERSION_get();
      if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public class LoadLodRequest : global::System.IDisposable {
    private global::System.Runtime.InteropServices.HandleRef swigCPtr;
    protected bool swigCMemOwn;
  
    internal LoadLodRequest(global::System.IntPtr cPtr, bool cMemoryOwn) {
      swigCMemOwn = cMemoryOwn;
      swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LoadLodRequest obj) {
      return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef swigRelease(LoadLodRequest obj) {
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
  
    ~LoadLodRequest() {
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
            OgreTerrainPINVOKE.delete_TerrainLodManager_LoadLodRequest(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public LoadLodRequest(TerrainLodManager r, ushort preparedLod, ushort loadedLod, ushort target) : this(OgreTerrainPINVOKE.new_TerrainLodManager_LoadLodRequest(TerrainLodManager.getCPtr(r), preparedLod, loadedLod, target), true) {
      if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    }
  
    public TerrainLodManager requestee {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_requestee_set(swigCPtr, TerrainLodManager.getCPtr(value));
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        global::System.IntPtr cPtr = OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_requestee_get(swigCPtr);
        TerrainLodManager ret = (cPtr == global::System.IntPtr.Zero) ? null : new TerrainLodManager(cPtr, false);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ushort currentPreparedLod {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_currentPreparedLod_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        ushort ret = OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_currentPreparedLod_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ushort currentLoadedLod {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_currentLoadedLod_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        ushort ret = OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_currentLoadedLod_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ushort requestedLod {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_requestedLod_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        ushort ret = OgreTerrainPINVOKE.TerrainLodManager_LoadLodRequest_requestedLod_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
  }

  public class LodInfo : global::System.IDisposable {
    private global::System.Runtime.InteropServices.HandleRef swigCPtr;
    protected bool swigCMemOwn;
  
    internal LodInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {
      swigCMemOwn = cMemoryOwn;
      swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LodInfo obj) {
      return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef swigRelease(LodInfo obj) {
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
  
    ~LodInfo() {
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
            OgreTerrainPINVOKE.delete_TerrainLodManager_LodInfo(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public uint treeStart {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LodInfo_treeStart_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        uint ret = OgreTerrainPINVOKE.TerrainLodManager_LodInfo_treeStart_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public uint treeEnd {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LodInfo_treeEnd_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        uint ret = OgreTerrainPINVOKE.TerrainLodManager_LodInfo_treeEnd_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public bool isLast {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LodInfo_isLast_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        bool ret = OgreTerrainPINVOKE.TerrainLodManager_LodInfo_isLast_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public ushort resolution {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LodInfo_resolution_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        ushort ret = OgreTerrainPINVOKE.TerrainLodManager_LodInfo_resolution_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public uint size {
      set {
        OgreTerrainPINVOKE.TerrainLodManager_LodInfo_size_set(swigCPtr, value);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
      } 
      get {
        uint ret = OgreTerrainPINVOKE.TerrainLodManager_LodInfo_size_get(swigCPtr);
        if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
        return ret;
      } 
    }
  
    public LodInfo() : this(OgreTerrainPINVOKE.new_TerrainLodManager_LodInfo(), true) {
      if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    }
  
  }

  public TerrainLodManager(Terrain t, DataStreamPtr stream) : this(OgreTerrainPINVOKE.new_TerrainLodManager__SWIG_0(Terrain.getCPtr(t), DataStreamPtr.getCPtr(stream)), true) {
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public TerrainLodManager(Terrain t, string filename) : this(OgreTerrainPINVOKE.new_TerrainLodManager__SWIG_1(Terrain.getCPtr(t), filename), true) {
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public TerrainLodManager(Terrain t) : this(OgreTerrainPINVOKE.new_TerrainLodManager__SWIG_2(Terrain.getCPtr(t)), true) {
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void open(string filename) {
    OgreTerrainPINVOKE.TerrainLodManager_open(swigCPtr, filename);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void close() {
    OgreTerrainPINVOKE.TerrainLodManager_close(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool isOpen() {
    bool ret = OgreTerrainPINVOKE.TerrainLodManager_isOpen(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void updateToLodLevel(int lodLevel, bool synchronous) {
    OgreTerrainPINVOKE.TerrainLodManager_updateToLodLevel__SWIG_0(swigCPtr, lodLevel, synchronous);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void updateToLodLevel(int lodLevel) {
    OgreTerrainPINVOKE.TerrainLodManager_updateToLodLevel__SWIG_1(swigCPtr, lodLevel);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public static void saveLodData(SWIGTYPE_p_Ogre__StreamSerialiser stream, Terrain terrain) {
    OgreTerrainPINVOKE.TerrainLodManager_saveLodData(SWIGTYPE_p_Ogre__StreamSerialiser.getCPtr(stream), Terrain.getCPtr(terrain));
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void fillBufferAtLod(uint lodLevel, SWIGTYPE_p_float data, uint dataSize) {
    OgreTerrainPINVOKE.TerrainLodManager_fillBufferAtLod(swigCPtr, lodLevel, SWIGTYPE_p_float.getCPtr(data), dataSize);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void readLodData(ushort lowerLodBound, ushort higherLodBound) {
    OgreTerrainPINVOKE.TerrainLodManager_readLodData(swigCPtr, lowerLodBound, higherLodBound);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public void waitForDerivedProcesses() {
    OgreTerrainPINVOKE.TerrainLodManager_waitForDerivedProcesses(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
  }

  public int getHighestLodPrepared() {
    int ret = OgreTerrainPINVOKE.TerrainLodManager_getHighestLodPrepared(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int getHighestLodLoaded() {
    int ret = OgreTerrainPINVOKE.TerrainLodManager_getHighestLodLoaded(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int getTargetLodLevel() {
    int ret = OgreTerrainPINVOKE.TerrainLodManager_getTargetLodLevel(swigCPtr);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public TerrainLodManager.LodInfo getLodInfo(uint lodLevel) {
    TerrainLodManager.LodInfo ret = new TerrainLodManager.LodInfo(OgreTerrainPINVOKE.TerrainLodManager_getLodInfo(swigCPtr, lodLevel), false);
    if (OgreTerrainPINVOKE.SWIGPendingException.Pending) throw OgreTerrainPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
