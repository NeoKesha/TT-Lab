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

public class Node : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Node(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Node obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Node obj) {
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

  ~Node() {
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
          OgrePINVOKE.delete_Node(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
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
            OgrePINVOKE.delete_Node_Listener(swigCPtr);
          }
          swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
        }
      }
    }
  
    public Listener() : this(OgrePINVOKE.new_Node_Listener(), true) {
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      SwigDirectorConnect();
    }
  
    public virtual void nodeUpdated(Node arg0) {
      if (SwigDerivedClassHasMethod("nodeUpdated", swigMethodTypes0)) OgrePINVOKE.Node_Listener_nodeUpdatedSwigExplicitListener(swigCPtr, Node.getCPtr(arg0)); else OgrePINVOKE.Node_Listener_nodeUpdated(swigCPtr, Node.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public virtual void nodeDestroyed(Node arg0) {
      if (SwigDerivedClassHasMethod("nodeDestroyed", swigMethodTypes1)) OgrePINVOKE.Node_Listener_nodeDestroyedSwigExplicitListener(swigCPtr, Node.getCPtr(arg0)); else OgrePINVOKE.Node_Listener_nodeDestroyed(swigCPtr, Node.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public virtual void nodeAttached(Node arg0) {
      if (SwigDerivedClassHasMethod("nodeAttached", swigMethodTypes2)) OgrePINVOKE.Node_Listener_nodeAttachedSwigExplicitListener(swigCPtr, Node.getCPtr(arg0)); else OgrePINVOKE.Node_Listener_nodeAttached(swigCPtr, Node.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    public virtual void nodeDetached(Node arg0) {
      if (SwigDerivedClassHasMethod("nodeDetached", swigMethodTypes3)) OgrePINVOKE.Node_Listener_nodeDetachedSwigExplicitListener(swigCPtr, Node.getCPtr(arg0)); else OgrePINVOKE.Node_Listener_nodeDetached(swigCPtr, Node.getCPtr(arg0));
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    }
  
    private void SwigDirectorConnect() {
      if (SwigDerivedClassHasMethod("nodeUpdated", swigMethodTypes0))
        swigDelegate0 = new SwigDelegateListener_0(SwigDirectorMethodnodeUpdated);
      if (SwigDerivedClassHasMethod("nodeDestroyed", swigMethodTypes1))
        swigDelegate1 = new SwigDelegateListener_1(SwigDirectorMethodnodeDestroyed);
      if (SwigDerivedClassHasMethod("nodeAttached", swigMethodTypes2))
        swigDelegate2 = new SwigDelegateListener_2(SwigDirectorMethodnodeAttached);
      if (SwigDerivedClassHasMethod("nodeDetached", swigMethodTypes3))
        swigDelegate3 = new SwigDelegateListener_3(SwigDirectorMethodnodeDetached);
      OgrePINVOKE.Node_Listener_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2, swigDelegate3);
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
  
    private void SwigDirectorMethodnodeUpdated(global::System.IntPtr arg0) {
      nodeUpdated((arg0 == global::System.IntPtr.Zero) ? null : new Node(arg0, false));
    }
  
    private void SwigDirectorMethodnodeDestroyed(global::System.IntPtr arg0) {
      nodeDestroyed((arg0 == global::System.IntPtr.Zero) ? null : new Node(arg0, false));
    }
  
    private void SwigDirectorMethodnodeAttached(global::System.IntPtr arg0) {
      nodeAttached((arg0 == global::System.IntPtr.Zero) ? null : new Node(arg0, false));
    }
  
    private void SwigDirectorMethodnodeDetached(global::System.IntPtr arg0) {
      nodeDetached((arg0 == global::System.IntPtr.Zero) ? null : new Node(arg0, false));
    }
  
    public delegate void SwigDelegateListener_0(global::System.IntPtr arg0);
    public delegate void SwigDelegateListener_1(global::System.IntPtr arg0);
    public delegate void SwigDelegateListener_2(global::System.IntPtr arg0);
    public delegate void SwigDelegateListener_3(global::System.IntPtr arg0);
  
    private SwigDelegateListener_0 swigDelegate0;
    private SwigDelegateListener_1 swigDelegate1;
    private SwigDelegateListener_2 swigDelegate2;
    private SwigDelegateListener_3 swigDelegate3;
  
    private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] { typeof(Node) };
    private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] { typeof(Node) };
    private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] { typeof(Node) };
    private static global::System.Type[] swigMethodTypes3 = new global::System.Type[] { typeof(Node) };
  }

  public string getName() {
    string ret = OgrePINVOKE.Node_getName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Node getParent() {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_getParent(swigCPtr);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Quaternion getOrientation() {
    Quaternion ret = new Quaternion(OgrePINVOKE.Node_getOrientation(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setOrientation(Quaternion q) {
    OgrePINVOKE.Node_setOrientation__SWIG_0(swigCPtr, Quaternion.getCPtr(q));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setOrientation(float w, float x, float y, float z) {
    OgrePINVOKE.Node_setOrientation__SWIG_1(swigCPtr, w, x, y, z);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void resetOrientation() {
    OgrePINVOKE.Node_resetOrientation(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setPosition(Vector3 pos) {
    OgrePINVOKE.Node_setPosition__SWIG_0(swigCPtr, Vector3.getCPtr(pos));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setPosition(float x, float y, float z) {
    OgrePINVOKE.Node_setPosition__SWIG_1(swigCPtr, x, y, z);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector3 getPosition() {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_getPosition(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setScale(Vector3 scale) {
    OgrePINVOKE.Node_setScale__SWIG_0(swigCPtr, Vector3.getCPtr(scale));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setScale(float x, float y, float z) {
    OgrePINVOKE.Node_setScale__SWIG_1(swigCPtr, x, y, z);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector3 getScale() {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_getScale(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setInheritOrientation(bool inherit) {
    OgrePINVOKE.Node_setInheritOrientation(swigCPtr, inherit);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getInheritOrientation() {
    bool ret = OgrePINVOKE.Node_getInheritOrientation(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setInheritScale(bool inherit) {
    OgrePINVOKE.Node_setInheritScale(swigCPtr, inherit);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getInheritScale() {
    bool ret = OgrePINVOKE.Node_getInheritScale(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void scale(Vector3 scale) {
    OgrePINVOKE.Node_scale__SWIG_0(swigCPtr, Vector3.getCPtr(scale));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void scale(float x, float y, float z) {
    OgrePINVOKE.Node_scale__SWIG_1(swigCPtr, x, y, z);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(Vector3 d, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_translate__SWIG_0(swigCPtr, Vector3.getCPtr(d), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(Vector3 d) {
    OgrePINVOKE.Node_translate__SWIG_1(swigCPtr, Vector3.getCPtr(d));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(float x, float y, float z, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_translate__SWIG_2(swigCPtr, x, y, z, (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(float x, float y, float z) {
    OgrePINVOKE.Node_translate__SWIG_3(swigCPtr, x, y, z);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(Matrix3 axes, Vector3 move, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_translate__SWIG_4(swigCPtr, Matrix3.getCPtr(axes), Vector3.getCPtr(move), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(Matrix3 axes, Vector3 move) {
    OgrePINVOKE.Node_translate__SWIG_5(swigCPtr, Matrix3.getCPtr(axes), Vector3.getCPtr(move));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(Matrix3 axes, float x, float y, float z, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_translate__SWIG_6(swigCPtr, Matrix3.getCPtr(axes), x, y, z, (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void translate(Matrix3 axes, float x, float y, float z) {
    OgrePINVOKE.Node_translate__SWIG_7(swigCPtr, Matrix3.getCPtr(axes), x, y, z);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void roll(Radian angle, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_roll__SWIG_0(swigCPtr, Radian.getCPtr(angle), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void roll(Radian angle) {
    OgrePINVOKE.Node_roll__SWIG_1(swigCPtr, Radian.getCPtr(angle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void pitch(Radian angle, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_pitch__SWIG_0(swigCPtr, Radian.getCPtr(angle), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void pitch(Radian angle) {
    OgrePINVOKE.Node_pitch__SWIG_1(swigCPtr, Radian.getCPtr(angle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void yaw(Radian angle, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_yaw__SWIG_0(swigCPtr, Radian.getCPtr(angle), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void yaw(Radian angle) {
    OgrePINVOKE.Node_yaw__SWIG_1(swigCPtr, Radian.getCPtr(angle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void rotate(Vector3 axis, Radian angle, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_rotate__SWIG_0(swigCPtr, Vector3.getCPtr(axis), Radian.getCPtr(angle), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void rotate(Vector3 axis, Radian angle) {
    OgrePINVOKE.Node_rotate__SWIG_1(swigCPtr, Vector3.getCPtr(axis), Radian.getCPtr(angle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void rotate(Quaternion q, Node.TransformSpace relativeTo) {
    OgrePINVOKE.Node_rotate__SWIG_2(swigCPtr, Quaternion.getCPtr(q), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void rotate(Quaternion q) {
    OgrePINVOKE.Node_rotate__SWIG_3(swigCPtr, Quaternion.getCPtr(q));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Matrix3 getLocalAxes() {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Node_getLocalAxes(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node createChild(Vector3 translate, Quaternion rotate) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_createChild__SWIG_0(swigCPtr, Vector3.getCPtr(translate), Quaternion.getCPtr(rotate));
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node createChild(Vector3 translate) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_createChild__SWIG_1(swigCPtr, Vector3.getCPtr(translate));
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node createChild() {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_createChild__SWIG_2(swigCPtr);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node createChild(string name, Vector3 translate, Quaternion rotate) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_createChild__SWIG_3(swigCPtr, name, Vector3.getCPtr(translate), Quaternion.getCPtr(rotate));
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node createChild(string name, Vector3 translate) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_createChild__SWIG_4(swigCPtr, name, Vector3.getCPtr(translate));
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node createChild(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_createChild__SWIG_5(swigCPtr, name);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void addChild(Node child) {
    OgrePINVOKE.Node_addChild(swigCPtr, Node.getCPtr(child));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ushort numChildren() {
    ushort ret = OgrePINVOKE.Node_numChildren(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Node getChild(ushort index) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_getChild__SWIG_0(swigCPtr, index);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Node getChild(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_getChild__SWIG_1(swigCPtr, name);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public NodeList getChildren() {
    NodeList ret = new NodeList(OgrePINVOKE.Node_getChildren(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node removeChild(ushort index) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_removeChild__SWIG_0(swigCPtr, index);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node removeChild(Node child) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_removeChild__SWIG_1(swigCPtr, Node.getCPtr(child));
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual Node removeChild(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_removeChild__SWIG_2(swigCPtr, name);
    Node ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void removeAllChildren() {
    OgrePINVOKE.Node_removeAllChildren(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _setDerivedPosition(Vector3 pos) {
    OgrePINVOKE.Node__setDerivedPosition(swigCPtr, Vector3.getCPtr(pos));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _setDerivedOrientation(Quaternion q) {
    OgrePINVOKE.Node__setDerivedOrientation(swigCPtr, Quaternion.getCPtr(q));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Quaternion _getDerivedOrientation() {
    Quaternion ret = new Quaternion(OgrePINVOKE.Node__getDerivedOrientation(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 _getDerivedPosition() {
    Vector3 ret = new Vector3(OgrePINVOKE.Node__getDerivedPosition(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 _getDerivedScale() {
    Vector3 ret = new Vector3(OgrePINVOKE.Node__getDerivedScale(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Affine3 _getFullTransform() {
    Affine3 ret = new Affine3(OgrePINVOKE.Node__getFullTransform(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void _update(bool updateChildren, bool parentHasChanged) {
    OgrePINVOKE.Node__update(swigCPtr, updateChildren, parentHasChanged);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setListener(Node.Listener listener) {
    OgrePINVOKE.Node_setListener(swigCPtr, Node.Listener.getCPtr(listener));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Node.Listener getListener() {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_getListener(swigCPtr);
    Node.Listener ret = (cPtr == global::System.IntPtr.Zero) ? null : new Node.Listener(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setInitialState() {
    OgrePINVOKE.Node_setInitialState(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void resetToInitialState() {
    OgrePINVOKE.Node_resetToInitialState(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector3 getInitialPosition() {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_getInitialPosition(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 convertWorldToLocalPosition(Vector3 worldPos) {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_convertWorldToLocalPosition(swigCPtr, Vector3.getCPtr(worldPos)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 convertLocalToWorldPosition(Vector3 localPos) {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_convertLocalToWorldPosition(swigCPtr, Vector3.getCPtr(localPos)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 convertWorldToLocalDirection(Vector3 worldDir, bool useScale) {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_convertWorldToLocalDirection(swigCPtr, Vector3.getCPtr(worldDir), useScale), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 convertLocalToWorldDirection(Vector3 localDir, bool useScale) {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_convertLocalToWorldDirection(swigCPtr, Vector3.getCPtr(localDir), useScale), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Quaternion convertWorldToLocalOrientation(Quaternion worldOrientation) {
    Quaternion ret = new Quaternion(OgrePINVOKE.Node_convertWorldToLocalOrientation(swigCPtr, Quaternion.getCPtr(worldOrientation)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Quaternion convertLocalToWorldOrientation(Quaternion localOrientation) {
    Quaternion ret = new Quaternion(OgrePINVOKE.Node_convertLocalToWorldOrientation(swigCPtr, Quaternion.getCPtr(localOrientation)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Quaternion getInitialOrientation() {
    Quaternion ret = new Quaternion(OgrePINVOKE.Node_getInitialOrientation(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 getInitialScale() {
    Vector3 ret = new Vector3(OgrePINVOKE.Node_getInitialScale(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float getSquaredViewDepth(Camera cam) {
    float ret = OgrePINVOKE.Node_getSquaredViewDepth(swigCPtr, Camera.getCPtr(cam));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void needUpdate(bool forceParentUpdate) {
    OgrePINVOKE.Node_needUpdate__SWIG_0(swigCPtr, forceParentUpdate);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void needUpdate() {
    OgrePINVOKE.Node_needUpdate__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void requestUpdate(Node child, bool forceParentUpdate) {
    OgrePINVOKE.Node_requestUpdate__SWIG_0(swigCPtr, Node.getCPtr(child), forceParentUpdate);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void requestUpdate(Node child) {
    OgrePINVOKE.Node_requestUpdate__SWIG_1(swigCPtr, Node.getCPtr(child));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void cancelUpdate(Node child) {
    OgrePINVOKE.Node_cancelUpdate(swigCPtr, Node.getCPtr(child));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static void queueNeedUpdate(Node n) {
    OgrePINVOKE.Node_queueNeedUpdate(Node.getCPtr(n));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static void processQueuedUpdates() {
    OgrePINVOKE.Node_processQueuedUpdates();
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public UserObjectBindings getUserObjectBindings() {
    UserObjectBindings ret = new UserObjectBindings(OgrePINVOKE.Node_getUserObjectBindings__SWIG_0(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SceneNode castSceneNode() {
    global::System.IntPtr cPtr = OgrePINVOKE.Node_castSceneNode(swigCPtr);
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public enum TransformSpace {
    TS_LOCAL,
    TS_PARENT,
    TS_WORLD
  }

}

}
