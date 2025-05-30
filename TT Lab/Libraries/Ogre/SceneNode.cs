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

public class SceneNode : Node {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal SceneNode(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.SceneNode_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SceneNode obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(SceneNode obj) {
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
          OgrePINVOKE.delete_SceneNode(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public SceneNode(SceneManager creator) : this(OgrePINVOKE.new_SceneNode__SWIG_0(SceneManager.getCPtr(creator)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SceneNode(SceneManager creator, string name) : this(OgrePINVOKE.new_SceneNode__SWIG_1(SceneManager.getCPtr(creator), name), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void attachObject(MovableObject obj) {
    OgrePINVOKE.SceneNode_attachObject(swigCPtr, MovableObject.getCPtr(obj));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint numAttachedObjects() {
    uint ret = OgrePINVOKE.SceneNode_numAttachedObjects(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public MovableObject getAttachedObject(uint index) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_getAttachedObject__SWIG_0(swigCPtr, index);
    MovableObject ret = (cPtr == global::System.IntPtr.Zero) ? null : new MovableObject(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public MovableObject getAttachedObject(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_getAttachedObject__SWIG_1(swigCPtr, name);
    MovableObject ret = (cPtr == global::System.IntPtr.Zero) ? null : new MovableObject(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual MovableObject detachObject(ushort index) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_detachObject__SWIG_0(swigCPtr, index);
    MovableObject ret = (cPtr == global::System.IntPtr.Zero) ? null : new MovableObject(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void detachObject(MovableObject obj) {
    OgrePINVOKE.SceneNode_detachObject__SWIG_1(swigCPtr, MovableObject.getCPtr(obj));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual MovableObject detachObject(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_detachObject__SWIG_2(swigCPtr, name);
    MovableObject ret = (cPtr == global::System.IntPtr.Zero) ? null : new MovableObject(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void detachAllObjects() {
    OgrePINVOKE.SceneNode_detachAllObjects(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyAllObjects() {
    OgrePINVOKE.SceneNode_destroyAllObjects(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool isInSceneGraph() {
    bool ret = OgrePINVOKE.SceneNode_isInSceneGraph(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _notifyRootNode() {
    OgrePINVOKE.SceneNode__notifyRootNode(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void _update(bool updateChildren, bool parentHasChanged) {
    OgrePINVOKE.SceneNode__update(swigCPtr, updateChildren, parentHasChanged);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void _updateBounds() {
    OgrePINVOKE.SceneNode__updateBounds(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _findVisibleObjects(Camera cam, RenderQueue queue, VisibleObjectsBoundsInfo visibleBounds, bool includeChildren, bool displayNodes, bool onlyShadowCasters) {
    OgrePINVOKE.SceneNode__findVisibleObjects__SWIG_0(swigCPtr, Camera.getCPtr(cam), RenderQueue.getCPtr(queue), VisibleObjectsBoundsInfo.getCPtr(visibleBounds), includeChildren, displayNodes, onlyShadowCasters);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _findVisibleObjects(Camera cam, RenderQueue queue, VisibleObjectsBoundsInfo visibleBounds, bool includeChildren, bool displayNodes) {
    OgrePINVOKE.SceneNode__findVisibleObjects__SWIG_1(swigCPtr, Camera.getCPtr(cam), RenderQueue.getCPtr(queue), VisibleObjectsBoundsInfo.getCPtr(visibleBounds), includeChildren, displayNodes);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _findVisibleObjects(Camera cam, RenderQueue queue, VisibleObjectsBoundsInfo visibleBounds, bool includeChildren) {
    OgrePINVOKE.SceneNode__findVisibleObjects__SWIG_2(swigCPtr, Camera.getCPtr(cam), RenderQueue.getCPtr(queue), VisibleObjectsBoundsInfo.getCPtr(visibleBounds), includeChildren);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _findVisibleObjects(Camera cam, RenderQueue queue, VisibleObjectsBoundsInfo visibleBounds) {
    OgrePINVOKE.SceneNode__findVisibleObjects__SWIG_3(swigCPtr, Camera.getCPtr(cam), RenderQueue.getCPtr(queue), VisibleObjectsBoundsInfo.getCPtr(visibleBounds));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public AxisAlignedBox _getWorldAABB() {
    AxisAlignedBox ret = new AxisAlignedBox(OgrePINVOKE.SceneNode__getWorldAABB(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public MovableObjectList getAttachedObjects() {
    MovableObjectList ret = new MovableObjectList(OgrePINVOKE.SceneNode_getAttachedObjects(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SceneManager getCreator() {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_getCreator(swigCPtr);
    SceneManager ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneManager(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void removeAndDestroyChild(string name) {
    OgrePINVOKE.SceneNode_removeAndDestroyChild__SWIG_0(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeAndDestroyChild(ushort index) {
    OgrePINVOKE.SceneNode_removeAndDestroyChild__SWIG_1(swigCPtr, index);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeAndDestroyChild(SceneNode child) {
    OgrePINVOKE.SceneNode_removeAndDestroyChild__SWIG_2(swigCPtr, SceneNode.getCPtr(child));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeAndDestroyAllChildren() {
    OgrePINVOKE.SceneNode_removeAndDestroyAllChildren(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyChildAndObjects(string name) {
    OgrePINVOKE.SceneNode_destroyChildAndObjects__SWIG_0(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyChildAndObjects(ushort index) {
    OgrePINVOKE.SceneNode_destroyChildAndObjects__SWIG_1(swigCPtr, index);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyChildAndObjects(SceneNode child) {
    OgrePINVOKE.SceneNode_destroyChildAndObjects__SWIG_2(swigCPtr, SceneNode.getCPtr(child));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyAllChildrenAndObjects() {
    OgrePINVOKE.SceneNode_destroyAllChildrenAndObjects(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void loadChildren(string filename) {
    OgrePINVOKE.SceneNode_loadChildren(swigCPtr, filename);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void saveChildren(string filename) {
    OgrePINVOKE.SceneNode_saveChildren(swigCPtr, filename);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void showBoundingBox(bool bShow) {
    OgrePINVOKE.SceneNode_showBoundingBox(swigCPtr, bShow);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getShowBoundingBox() {
    bool ret = OgrePINVOKE.SceneNode_getShowBoundingBox(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual SceneNode createChildSceneNode(Vector3 translate, Quaternion rotate) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_createChildSceneNode__SWIG_0(swigCPtr, Vector3.getCPtr(translate), Quaternion.getCPtr(rotate));
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual SceneNode createChildSceneNode(Vector3 translate) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_createChildSceneNode__SWIG_1(swigCPtr, Vector3.getCPtr(translate));
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual SceneNode createChildSceneNode() {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_createChildSceneNode__SWIG_2(swigCPtr);
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual SceneNode createChildSceneNode(string name, Vector3 translate, Quaternion rotate) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_createChildSceneNode__SWIG_3(swigCPtr, name, Vector3.getCPtr(translate), Quaternion.getCPtr(rotate));
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual SceneNode createChildSceneNode(string name, Vector3 translate) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_createChildSceneNode__SWIG_4(swigCPtr, name, Vector3.getCPtr(translate));
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual SceneNode createChildSceneNode(string name) {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_createChildSceneNode__SWIG_5(swigCPtr, name);
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void findLights(SWIGTYPE_p_std__vectorT_Ogre__Light_p_t destList, float radius, uint lightMask) {
    OgrePINVOKE.SceneNode_findLights__SWIG_0(swigCPtr, SWIGTYPE_p_std__vectorT_Ogre__Light_p_t.getCPtr(destList), radius, lightMask);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void findLights(SWIGTYPE_p_std__vectorT_Ogre__Light_p_t destList, float radius) {
    OgrePINVOKE.SceneNode_findLights__SWIG_1(swigCPtr, SWIGTYPE_p_std__vectorT_Ogre__Light_p_t.getCPtr(destList), radius);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setFixedYawAxis(bool useFixed, Vector3 fixedAxis) {
    OgrePINVOKE.SceneNode_setFixedYawAxis__SWIG_0(swigCPtr, useFixed, Vector3.getCPtr(fixedAxis));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setFixedYawAxis(bool useFixed) {
    OgrePINVOKE.SceneNode_setFixedYawAxis__SWIG_1(swigCPtr, useFixed);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void yaw(Radian angle, Node.TransformSpace relativeTo) {
    OgrePINVOKE.SceneNode_yaw__SWIG_0(swigCPtr, Radian.getCPtr(angle), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void yaw(Radian angle) {
    OgrePINVOKE.SceneNode_yaw__SWIG_1(swigCPtr, Radian.getCPtr(angle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDirection(float x, float y, float z, Node.TransformSpace relativeTo, Vector3 localDirectionVector) {
    OgrePINVOKE.SceneNode_setDirection__SWIG_0(swigCPtr, x, y, z, (int)relativeTo, Vector3.getCPtr(localDirectionVector));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDirection(float x, float y, float z, Node.TransformSpace relativeTo) {
    OgrePINVOKE.SceneNode_setDirection__SWIG_1(swigCPtr, x, y, z, (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDirection(float x, float y, float z) {
    OgrePINVOKE.SceneNode_setDirection__SWIG_2(swigCPtr, x, y, z);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDirection(Vector3 vec, Node.TransformSpace relativeTo, Vector3 localDirectionVector) {
    OgrePINVOKE.SceneNode_setDirection__SWIG_3(swigCPtr, Vector3.getCPtr(vec), (int)relativeTo, Vector3.getCPtr(localDirectionVector));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDirection(Vector3 vec, Node.TransformSpace relativeTo) {
    OgrePINVOKE.SceneNode_setDirection__SWIG_4(swigCPtr, Vector3.getCPtr(vec), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDirection(Vector3 vec) {
    OgrePINVOKE.SceneNode_setDirection__SWIG_5(swigCPtr, Vector3.getCPtr(vec));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void lookAt(Vector3 targetPoint, Node.TransformSpace relativeTo, Vector3 localDirectionVector) {
    OgrePINVOKE.SceneNode_lookAt__SWIG_0(swigCPtr, Vector3.getCPtr(targetPoint), (int)relativeTo, Vector3.getCPtr(localDirectionVector));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void lookAt(Vector3 targetPoint, Node.TransformSpace relativeTo) {
    OgrePINVOKE.SceneNode_lookAt__SWIG_1(swigCPtr, Vector3.getCPtr(targetPoint), (int)relativeTo);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setAutoTracking(bool enabled, SceneNode target, Vector3 localDirectionVector, Vector3 offset) {
    OgrePINVOKE.SceneNode_setAutoTracking__SWIG_0(swigCPtr, enabled, SceneNode.getCPtr(target), Vector3.getCPtr(localDirectionVector), Vector3.getCPtr(offset));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setAutoTracking(bool enabled, SceneNode target, Vector3 localDirectionVector) {
    OgrePINVOKE.SceneNode_setAutoTracking__SWIG_1(swigCPtr, enabled, SceneNode.getCPtr(target), Vector3.getCPtr(localDirectionVector));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setAutoTracking(bool enabled, SceneNode target) {
    OgrePINVOKE.SceneNode_setAutoTracking__SWIG_2(swigCPtr, enabled, SceneNode.getCPtr(target));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setAutoTracking(bool enabled) {
    OgrePINVOKE.SceneNode_setAutoTracking__SWIG_3(swigCPtr, enabled);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SceneNode getAutoTrackTarget() {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_getAutoTrackTarget(swigCPtr);
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 getAutoTrackOffset() {
    Vector3 ret = new Vector3(OgrePINVOKE.SceneNode_getAutoTrackOffset(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 getAutoTrackLocalDirection() {
    Vector3 ret = new Vector3(OgrePINVOKE.SceneNode_getAutoTrackLocalDirection(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _autoTrack() {
    OgrePINVOKE.SceneNode__autoTrack(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SceneNode getParentSceneNode() {
    global::System.IntPtr cPtr = OgrePINVOKE.SceneNode_getParentSceneNode(swigCPtr);
    SceneNode ret = (cPtr == global::System.IntPtr.Zero) ? null : new SceneNode(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setVisible(bool visible, bool cascade) {
    OgrePINVOKE.SceneNode_setVisible__SWIG_0(swigCPtr, visible, cascade);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setVisible(bool visible) {
    OgrePINVOKE.SceneNode_setVisible__SWIG_1(swigCPtr, visible);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void flipVisibility(bool cascade) {
    OgrePINVOKE.SceneNode_flipVisibility__SWIG_0(swigCPtr, cascade);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void flipVisibility() {
    OgrePINVOKE.SceneNode_flipVisibility__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDebugDisplayEnabled(bool enabled, bool cascade) {
    OgrePINVOKE.SceneNode_setDebugDisplayEnabled__SWIG_0(swigCPtr, enabled, cascade);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDebugDisplayEnabled(bool enabled) {
    OgrePINVOKE.SceneNode_setDebugDisplayEnabled__SWIG_1(swigCPtr, enabled);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
