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

public class MovableObject : ShadowCaster {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal MovableObject(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.MovableObject_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MovableObject obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(MovableObject obj) {
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
          OgrePINVOKE.delete_MovableObject(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public class Listener : global::System.IDisposable {
    private global::System.Runtime.InteropServices.HandleRef swigCPtr;
    protected bool swigCMemOwn;
  
    internal Listener(global::System.IntPtr cPtr, bool cMemoryOwn) {
      swigCMemOwn = cMemoryOwn;
      swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Listener obj) {
      return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
    }
  
    internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Listener obj) {
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
  
    ~Listener() {
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
            OgrePINVOKE.delete_MovableObject_Listener(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public Listener() : this(OgrePINVOKE.new_MovableObject_Listener(), true) {
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      SwigDirectorConnect();
    }
  
    public virtual void objectDestroyed(MovableObject arg0) {
      if (SwigDerivedClassHasMethod("objectDestroyed", swigMethodTypes0)) OgrePINVOKE.MovableObject_Listener_objectDestroyedSwigExplicitListener(swigCPtr, MovableObject.getCPtr(arg0)); else OgrePINVOKE.MovableObject_Listener_objectDestroyed(swigCPtr, MovableObject.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public virtual void objectAttached(MovableObject arg0) {
      if (SwigDerivedClassHasMethod("objectAttached", swigMethodTypes1)) OgrePINVOKE.MovableObject_Listener_objectAttachedSwigExplicitListener(swigCPtr, MovableObject.getCPtr(arg0)); else OgrePINVOKE.MovableObject_Listener_objectAttached(swigCPtr, MovableObject.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public virtual void objectDetached(MovableObject arg0) {
      if (SwigDerivedClassHasMethod("objectDetached", swigMethodTypes2)) OgrePINVOKE.MovableObject_Listener_objectDetachedSwigExplicitListener(swigCPtr, MovableObject.getCPtr(arg0)); else OgrePINVOKE.MovableObject_Listener_objectDetached(swigCPtr, MovableObject.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public virtual void objectMoved(MovableObject arg0) {
      if (SwigDerivedClassHasMethod("objectMoved", swigMethodTypes3)) OgrePINVOKE.MovableObject_Listener_objectMovedSwigExplicitListener(swigCPtr, MovableObject.getCPtr(arg0)); else OgrePINVOKE.MovableObject_Listener_objectMoved(swigCPtr, MovableObject.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public virtual bool objectRendering(MovableObject arg0, Camera arg1) {
      bool ret = (SwigDerivedClassHasMethod("objectRendering", swigMethodTypes4) ? OgrePINVOKE.MovableObject_Listener_objectRenderingSwigExplicitListener(swigCPtr, MovableObject.getCPtr(arg0), Camera.getCPtr(arg1)) : OgrePINVOKE.MovableObject_Listener_objectRendering(swigCPtr, MovableObject.getCPtr(arg0), Camera.getCPtr(arg1)));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
  
    public virtual SWIGTYPE_p_std__vectorT_Ogre__Light_p_t objectQueryLights(MovableObject arg0) {
      global::System.IntPtr cPtr = (SwigDerivedClassHasMethod("objectQueryLights", swigMethodTypes5) ? OgrePINVOKE.MovableObject_Listener_objectQueryLightsSwigExplicitListener(swigCPtr, MovableObject.getCPtr(arg0)) : OgrePINVOKE.MovableObject_Listener_objectQueryLights(swigCPtr, MovableObject.getCPtr(arg0)));
      SWIGTYPE_p_std__vectorT_Ogre__Light_p_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_Ogre__Light_p_t(cPtr, false);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    }
  
    private void SwigDirectorConnect() {
      if (SwigDerivedClassHasMethod("objectDestroyed", swigMethodTypes0))
        swigDelegate0 = new SwigDelegateListener_0(SwigDirectorMethodobjectDestroyed);
      if (SwigDerivedClassHasMethod("objectAttached", swigMethodTypes1))
        swigDelegate1 = new SwigDelegateListener_1(SwigDirectorMethodobjectAttached);
      if (SwigDerivedClassHasMethod("objectDetached", swigMethodTypes2))
        swigDelegate2 = new SwigDelegateListener_2(SwigDirectorMethodobjectDetached);
      if (SwigDerivedClassHasMethod("objectMoved", swigMethodTypes3))
        swigDelegate3 = new SwigDelegateListener_3(SwigDirectorMethodobjectMoved);
      if (SwigDerivedClassHasMethod("objectRendering", swigMethodTypes4))
        swigDelegate4 = new SwigDelegateListener_4(SwigDirectorMethodobjectRendering);
      if (SwigDerivedClassHasMethod("objectQueryLights", swigMethodTypes5))
        swigDelegate5 = new SwigDelegateListener_5(SwigDirectorMethodobjectQueryLights);
      OgrePINVOKE.MovableObject_Listener_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2, swigDelegate3, swigDelegate4, swigDelegate5);
    }
  
    private bool SwigDerivedClassHasMethod(string methodName, global::System.Type[] methodTypes) {
      global::System.Reflection.MethodInfo[] methodInfos = this.GetType().GetMethods(
          global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Instance);
      foreach (global::System.Reflection.MethodInfo methodInfo in methodInfos) {
        if (methodInfo.DeclaringType == null)
          continue;
  
        if (methodInfo.Name != methodName)
          continue;
  
        var parameters = methodInfo.GetParameters();
        if (parameters.Length != methodTypes.Length)
          continue;
  
        bool parametersMatch = true;
        for (var i = 0; i < parameters.Length; i++) {
          if (parameters[i].ParameterType != methodTypes[i]) {
            parametersMatch = false;
            break;
          }
        }
  
        if (!parametersMatch)
          continue;
  
        if (methodInfo.IsVirtual && (methodInfo.DeclaringType.IsSubclassOf(typeof(Listener))) &&
          methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType) {
          return true;
        }
      }
  
      return false;
    }
  
    private void SwigDirectorMethodobjectDestroyed(global::System.IntPtr arg0) {
      objectDestroyed((arg0 == global::System.IntPtr.Zero) ? null : new MovableObject(arg0, false));
    }
  
    private void SwigDirectorMethodobjectAttached(global::System.IntPtr arg0) {
      objectAttached((arg0 == global::System.IntPtr.Zero) ? null : new MovableObject(arg0, false));
    }
  
    private void SwigDirectorMethodobjectDetached(global::System.IntPtr arg0) {
      objectDetached((arg0 == global::System.IntPtr.Zero) ? null : new MovableObject(arg0, false));
    }
  
    private void SwigDirectorMethodobjectMoved(global::System.IntPtr arg0) {
      objectMoved((arg0 == global::System.IntPtr.Zero) ? null : new MovableObject(arg0, false));
    }
  
    private bool SwigDirectorMethodobjectRendering(global::System.IntPtr arg0, global::System.IntPtr arg1) {
      return objectRendering((arg0 == global::System.IntPtr.Zero) ? null : new MovableObject(arg0, false), (arg1 == global::System.IntPtr.Zero) ? null : new Camera(arg1, false));
    }
  
    private global::System.IntPtr SwigDirectorMethodobjectQueryLights(global::System.IntPtr arg0) {
      return SWIGTYPE_p_std__vectorT_Ogre__Light_p_t.getCPtr(objectQueryLights((arg0 == global::System.IntPtr.Zero) ? null : new MovableObject(arg0, false))).Handle;
    }
  
    public delegate void SwigDelegateListener_0(global::System.IntPtr arg0);
    public delegate void SwigDelegateListener_1(global::System.IntPtr arg0);
    public delegate void SwigDelegateListener_2(global::System.IntPtr arg0);
    public delegate void SwigDelegateListener_3(global::System.IntPtr arg0);
    public delegate bool SwigDelegateListener_4(global::System.IntPtr arg0, global::System.IntPtr arg1);
    public delegate global::System.IntPtr SwigDelegateListener_5(global::System.IntPtr arg0);
  
    private SwigDelegateListener_0 swigDelegate0;
    private SwigDelegateListener_1 swigDelegate1;
    private SwigDelegateListener_2 swigDelegate2;
    private SwigDelegateListener_3 swigDelegate3;
    private SwigDelegateListener_4 swigDelegate4;
    private SwigDelegateListener_5 swigDelegate5;
  
    private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] { typeof(MovableObject) };
    private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] { typeof(MovableObject) };
    private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] { typeof(MovableObject) };
    private static global::System.Type[] swigMethodTypes3 = new global::System.Type[] { typeof(MovableObject) };
    private static global::System.Type[] swigMethodTypes4 = new global::System.Type[] { typeof(MovableObject), typeof(Camera) };
    private static global::System.Type[] swigMethodTypes5 = new global::System.Type[] { typeof(MovableObject) };
  }

  public virtual void _notifyCreator(MovableObjectFactory fact) {
    OgrePINVOKE.MovableObject__notifyCreator(swigCPtr, MovableObjectFactory.getCPtr(fact));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public MovableObjectFactory _getCreator() {
    global::System.IntPtr cPtr = OgrePINVOKE.MovableObject__getCreator(swigCPtr);
    MovableObjectFactory ret = (cPtr == global::System.IntPtr.Zero) ? null : new MovableObjectFactory(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void _notifyManager(SceneManager man) {
    OgrePINVOKE.MovableObject__notifyManager(swigCPtr, SceneManager.getCPtr(man));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SceneManager _getManager() {
    global::System.IntPtr cPtr = OgrePINVOKE.MovableObject__getManager(swigCPtr);
    SceneManager ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneManager(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void _releaseManualHardwareResources() {
    OgrePINVOKE.MovableObject__releaseManualHardwareResources(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void _restoreManualHardwareResources() {
    OgrePINVOKE.MovableObject__restoreManualHardwareResources(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getName() {
    string ret = OgrePINVOKE.MovableObject_getName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getMovableType() {
    string ret = OgrePINVOKE.MovableObject_getMovableType(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Node getParentNode() {
    global::System.IntPtr cPtr = OgrePINVOKE.MovableObject_getParentNode(swigCPtr);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SceneNode getParentSceneNode() {
    global::System.IntPtr cPtr = OgrePINVOKE.MovableObject_getParentSceneNode(swigCPtr);
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isParentTagPoint() {
    bool ret = OgrePINVOKE.MovableObject_isParentTagPoint(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _notifyAttached(Node parent, bool isTagPoint) {
    OgrePINVOKE.MovableObject__notifyAttached__SWIG_0(swigCPtr, Node.getCPtr(parent), isTagPoint);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyAttached(Node parent) {
    OgrePINVOKE.MovableObject__notifyAttached__SWIG_1(swigCPtr, Node.getCPtr(parent));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool isAttached() {
    bool ret = OgrePINVOKE.MovableObject_isAttached(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void detachFromParent() {
    OgrePINVOKE.MovableObject_detachFromParent(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual bool isInScene() {
    bool ret = OgrePINVOKE.MovableObject_isInScene(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void _notifyMoved() {
    OgrePINVOKE.MovableObject__notifyMoved(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyCurrentCamera(Camera cam) {
    OgrePINVOKE.MovableObject__notifyCurrentCamera(swigCPtr, Camera.getCPtr(cam));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public AxisAlignedBox getBoundingBox() {
    AxisAlignedBox ret = new AxisAlignedBox(OgrePINVOKE.MovableObject_getBoundingBox(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float getBoundingRadius() {
    float ret = OgrePINVOKE.MovableObject_getBoundingRadius(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float getBoundingRadiusScaled() {
    float ret = OgrePINVOKE.MovableObject_getBoundingRadiusScaled(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override AxisAlignedBox getWorldBoundingBox(bool derive) {
    AxisAlignedBox ret = new AxisAlignedBox(OgrePINVOKE.MovableObject_getWorldBoundingBox__SWIG_0(swigCPtr, derive), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override AxisAlignedBox getWorldBoundingBox() {
    AxisAlignedBox ret = new AxisAlignedBox(OgrePINVOKE.MovableObject_getWorldBoundingBox__SWIG_1(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Sphere getWorldBoundingSphere(bool derive) {
    Sphere ret = new Sphere(OgrePINVOKE.MovableObject_getWorldBoundingSphere__SWIG_0(swigCPtr, derive), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Sphere getWorldBoundingSphere() {
    Sphere ret = new Sphere(OgrePINVOKE.MovableObject_getWorldBoundingSphere__SWIG_1(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _updateRenderQueue(RenderQueue queue) {
    OgrePINVOKE.MovableObject__updateRenderQueue(swigCPtr, RenderQueue.getCPtr(queue));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setVisible(bool visible) {
    OgrePINVOKE.MovableObject_setVisible(swigCPtr, visible);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getVisible() {
    bool ret = OgrePINVOKE.MovableObject_getVisible(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool isVisible() {
    bool ret = OgrePINVOKE.MovableObject_isVisible(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setRenderingDistance(float dist) {
    OgrePINVOKE.MovableObject_setRenderingDistance(swigCPtr, dist);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public float getRenderingDistance() {
    float ret = OgrePINVOKE.MovableObject_getRenderingDistance(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setRenderingMinPixelSize(float pixelSize) {
    OgrePINVOKE.MovableObject_setRenderingMinPixelSize(swigCPtr, pixelSize);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public float getRenderingMinPixelSize() {
    float ret = OgrePINVOKE.MovableObject_getRenderingMinPixelSize(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public UserObjectBindings getUserObjectBindings() {
    UserObjectBindings ret = new UserObjectBindings(OgrePINVOKE.MovableObject_getUserObjectBindings__SWIG_0(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setRenderQueueGroup(byte queueID) {
    OgrePINVOKE.MovableObject_setRenderQueueGroup(swigCPtr, queueID);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setRenderQueueGroupAndPriority(byte queueID, ushort priority) {
    OgrePINVOKE.MovableObject_setRenderQueueGroupAndPriority(swigCPtr, queueID, priority);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public byte getRenderQueueGroup() {
    byte ret = OgrePINVOKE.MovableObject_getRenderQueueGroup(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Affine3 _getParentNodeFullTransform() {
    Affine3 ret = new Affine3(OgrePINVOKE.MovableObject__getParentNodeFullTransform(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setQueryFlags(uint flags) {
    OgrePINVOKE.MovableObject_setQueryFlags(swigCPtr, flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addQueryFlags(uint flags) {
    OgrePINVOKE.MovableObject_addQueryFlags(swigCPtr, flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeQueryFlags(uint flags) {
    OgrePINVOKE.MovableObject_removeQueryFlags(swigCPtr, flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual uint getQueryFlags() {
    uint ret = OgrePINVOKE.MovableObject_getQueryFlags(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void setDefaultQueryFlags(uint flags) {
    OgrePINVOKE.MovableObject_setDefaultQueryFlags(flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static uint getDefaultQueryFlags() {
    uint ret = OgrePINVOKE.MovableObject_getDefaultQueryFlags();
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setVisibilityFlags(uint flags) {
    OgrePINVOKE.MovableObject_setVisibilityFlags(swigCPtr, flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addVisibilityFlags(uint flags) {
    OgrePINVOKE.MovableObject_addVisibilityFlags(swigCPtr, flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeVisibilityFlags(uint flags) {
    OgrePINVOKE.MovableObject_removeVisibilityFlags(swigCPtr, flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual uint getVisibilityFlags() {
    uint ret = OgrePINVOKE.MovableObject_getVisibilityFlags(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void setDefaultVisibilityFlags(uint flags) {
    OgrePINVOKE.MovableObject_setDefaultVisibilityFlags(flags);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static uint getDefaultVisibilityFlags() {
    uint ret = OgrePINVOKE.MovableObject_getDefaultVisibilityFlags();
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setListener(MovableObject.Listener listener) {
    OgrePINVOKE.MovableObject_setListener(swigCPtr, MovableObject.Listener.getCPtr(listener));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public MovableObject.Listener getListener() {
    global::System.IntPtr cPtr = OgrePINVOKE.MovableObject_getListener(swigCPtr);
    MovableObject.Listener ret = (cPtr == global::System.IntPtr.Zero) ? null : new MovableObject.Listener(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_Ogre__Light_p_t queryLights() {
    SWIGTYPE_p_std__vectorT_Ogre__Light_p_t ret = new SWIGTYPE_p_std__vectorT_Ogre__Light_p_t(OgrePINVOKE.MovableObject_queryLights(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getLightMask() {
    uint ret = OgrePINVOKE.MovableObject_getLightMask(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setLightMask(uint lightMask) {
    OgrePINVOKE.MovableObject_setLightMask(swigCPtr, lightMask);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__vectorT_Ogre__Light_p_t _getLightList() {
    global::System.IntPtr cPtr = OgrePINVOKE.MovableObject__getLightList(swigCPtr);
    SWIGTYPE_p_std__vectorT_Ogre__Light_p_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_Ogre__Light_p_t(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setCastShadows(bool enabled) {
    OgrePINVOKE.MovableObject_setCastShadows(swigCPtr, enabled);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override bool getCastShadows() {
    bool ret = OgrePINVOKE.MovableObject_getCastShadows(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool getReceivesShadows() {
    bool ret = OgrePINVOKE.MovableObject_getReceivesShadows(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override float getPointExtrusionDistance(Light l) {
    float ret = OgrePINVOKE.MovableObject_getPointExtrusionDistance(swigCPtr, Light.getCPtr(l));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getTypeFlags() {
    uint ret = OgrePINVOKE.MovableObject_getTypeFlags(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void visitRenderables(Renderable.Visitor visitor, bool debugRenderables) {
    OgrePINVOKE.MovableObject_visitRenderables__SWIG_0(swigCPtr, Renderable.Visitor.getCPtr(visitor), debugRenderables);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void visitRenderables(Renderable.Visitor visitor) {
    OgrePINVOKE.MovableObject_visitRenderables__SWIG_1(swigCPtr, Renderable.Visitor.getCPtr(visitor));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDebugDisplayEnabled(bool enabled) {
    OgrePINVOKE.MovableObject_setDebugDisplayEnabled(swigCPtr, enabled);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool isDebugDisplayEnabled() {
    bool ret = OgrePINVOKE.MovableObject_isDebugDisplayEnabled(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Entity castEntity() {
    global::System.IntPtr cPtr = OgrePINVOKE.MovableObject_castEntity(swigCPtr);
    Entity ret = (cPtr == global::System.IntPtr.Zero) ? null : new Entity(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
