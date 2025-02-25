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

public class Animation : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Animation(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Animation obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Animation obj) {
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

  ~Animation() {
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
          OgrePINVOKE.delete_Animation(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public Animation(string name, float length) : this(OgrePINVOKE.new_Animation(name, length), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getName() {
    string ret = OgrePINVOKE.Animation_getName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float getLength() {
    float ret = OgrePINVOKE.Animation_getLength(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setLength(float len) {
    OgrePINVOKE.Animation_setLength(swigCPtr, len);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public NodeAnimationTrack createNodeTrack(ushort handle) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_createNodeTrack__SWIG_0(swigCPtr, handle);
    NodeAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new NodeAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public VertexAnimationTrack createVertexTrack(ushort handle, VertexAnimationType animType) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_createVertexTrack__SWIG_0(swigCPtr, handle, (int)animType);
    VertexAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new VertexAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public NodeAnimationTrack createNodeTrack(ushort handle, Node node) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_createNodeTrack__SWIG_1(swigCPtr, handle, Node.getCPtr(node));
    NodeAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new NodeAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public NumericAnimationTrack createNumericTrack(ushort handle, SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t anim) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_createNumericTrack(swigCPtr, handle, SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t.getCPtr(anim));
    NumericAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new NumericAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public VertexAnimationTrack createVertexTrack(ushort handle, VertexData data, VertexAnimationType animType) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_createVertexTrack__SWIG_1(swigCPtr, handle, VertexData.getCPtr(data), (int)animType);
    VertexAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new VertexAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ushort getNumNodeTracks() {
    ushort ret = OgrePINVOKE.Animation_getNumNodeTracks(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public NodeAnimationTrack getNodeTrack(ushort handle) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_getNodeTrack(swigCPtr, handle);
    NodeAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new NodeAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool hasNodeTrack(ushort handle) {
    bool ret = OgrePINVOKE.Animation_hasNodeTrack(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ushort getNumNumericTracks() {
    ushort ret = OgrePINVOKE.Animation_getNumNumericTracks(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public NumericAnimationTrack getNumericTrack(ushort handle) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_getNumericTrack(swigCPtr, handle);
    NumericAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new NumericAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool hasNumericTrack(ushort handle) {
    bool ret = OgrePINVOKE.Animation_hasNumericTrack(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ushort getNumVertexTracks() {
    ushort ret = OgrePINVOKE.Animation_getNumVertexTracks(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public VertexAnimationTrack getVertexTrack(ushort handle) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_getVertexTrack(swigCPtr, handle);
    VertexAnimationTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new VertexAnimationTrack(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool hasVertexTrack(ushort handle) {
    bool ret = OgrePINVOKE.Animation_hasVertexTrack(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void destroyNodeTrack(ushort handle) {
    OgrePINVOKE.Animation_destroyNodeTrack(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyNumericTrack(ushort handle) {
    OgrePINVOKE.Animation_destroyNumericTrack(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyVertexTrack(ushort handle) {
    OgrePINVOKE.Animation_destroyVertexTrack(swigCPtr, handle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyAllTracks() {
    OgrePINVOKE.Animation_destroyAllTracks(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyAllNodeTracks() {
    OgrePINVOKE.Animation_destroyAllNodeTracks(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyAllNumericTracks() {
    OgrePINVOKE.Animation_destroyAllNumericTracks(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyAllVertexTracks() {
    OgrePINVOKE.Animation_destroyAllVertexTracks(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(float timePos, float weight, float scale) {
    OgrePINVOKE.Animation_apply__SWIG_0(swigCPtr, timePos, weight, scale);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(float timePos, float weight) {
    OgrePINVOKE.Animation_apply__SWIG_1(swigCPtr, timePos, weight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(float timePos) {
    OgrePINVOKE.Animation_apply__SWIG_2(swigCPtr, timePos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToNode(Node node, float timePos, float weight, float scale) {
    OgrePINVOKE.Animation_applyToNode__SWIG_0(swigCPtr, Node.getCPtr(node), timePos, weight, scale);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToNode(Node node, float timePos, float weight) {
    OgrePINVOKE.Animation_applyToNode__SWIG_1(swigCPtr, Node.getCPtr(node), timePos, weight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToNode(Node node, float timePos) {
    OgrePINVOKE.Animation_applyToNode__SWIG_2(swigCPtr, Node.getCPtr(node), timePos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(Skeleton skeleton, float timePos, float weight, float scale) {
    OgrePINVOKE.Animation_apply__SWIG_3(swigCPtr, Skeleton.getCPtr(skeleton), timePos, weight, scale);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(Skeleton skeleton, float timePos, float weight) {
    OgrePINVOKE.Animation_apply__SWIG_4(swigCPtr, Skeleton.getCPtr(skeleton), timePos, weight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(Skeleton skeleton, float timePos) {
    OgrePINVOKE.Animation_apply__SWIG_5(swigCPtr, Skeleton.getCPtr(skeleton), timePos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(Skeleton skeleton, float timePos, float weight, SplitPointList blendMask, float scale) {
    OgrePINVOKE.Animation_apply__SWIG_6(swigCPtr, Skeleton.getCPtr(skeleton), timePos, weight, SplitPointList.getCPtr(blendMask), scale);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void apply(Entity entity, float timePos, float weight, bool software, bool hardware) {
    OgrePINVOKE.Animation_apply__SWIG_7(swigCPtr, Entity.getCPtr(entity), timePos, weight, software, hardware);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToAnimable(SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t anim, float timePos, float weight, float scale) {
    OgrePINVOKE.Animation_applyToAnimable__SWIG_0(swigCPtr, SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t.getCPtr(anim), timePos, weight, scale);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToAnimable(SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t anim, float timePos, float weight) {
    OgrePINVOKE.Animation_applyToAnimable__SWIG_1(swigCPtr, SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t.getCPtr(anim), timePos, weight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToAnimable(SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t anim, float timePos) {
    OgrePINVOKE.Animation_applyToAnimable__SWIG_2(swigCPtr, SWIGTYPE_p_Ogre__SharedPtrT_Ogre__AnimableValue_t.getCPtr(anim), timePos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToVertexData(VertexData data, float timePos, float weight) {
    OgrePINVOKE.Animation_applyToVertexData__SWIG_0(swigCPtr, VertexData.getCPtr(data), timePos, weight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void applyToVertexData(VertexData data, float timePos) {
    OgrePINVOKE.Animation_applyToVertexData__SWIG_1(swigCPtr, VertexData.getCPtr(data), timePos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setInterpolationMode(Animation.InterpolationMode im) {
    OgrePINVOKE.Animation_setInterpolationMode(swigCPtr, (int)im);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Animation.InterpolationMode getInterpolationMode() {
    Animation.InterpolationMode ret = (Animation.InterpolationMode)OgrePINVOKE.Animation_getInterpolationMode(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setRotationInterpolationMode(Animation.RotationInterpolationMode im) {
    OgrePINVOKE.Animation_setRotationInterpolationMode(swigCPtr, (int)im);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Animation.RotationInterpolationMode getRotationInterpolationMode() {
    Animation.RotationInterpolationMode ret = (Animation.RotationInterpolationMode)OgrePINVOKE.Animation_getRotationInterpolationMode(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void setDefaultInterpolationMode(Animation.InterpolationMode im) {
    OgrePINVOKE.Animation_setDefaultInterpolationMode((int)im);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static Animation.InterpolationMode getDefaultInterpolationMode() {
    Animation.InterpolationMode ret = (Animation.InterpolationMode)OgrePINVOKE.Animation_getDefaultInterpolationMode();
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void setDefaultRotationInterpolationMode(Animation.RotationInterpolationMode im) {
    OgrePINVOKE.Animation_setDefaultRotationInterpolationMode((int)im);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static Animation.RotationInterpolationMode getDefaultRotationInterpolationMode() {
    Animation.RotationInterpolationMode ret = (Animation.RotationInterpolationMode)OgrePINVOKE.Animation_getDefaultRotationInterpolationMode();
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__mapT_unsigned_short_Ogre__NodeAnimationTrack_p_std__lessT_unsigned_short_t_t _getNodeTrackList() {
    SWIGTYPE_p_std__mapT_unsigned_short_Ogre__NodeAnimationTrack_p_std__lessT_unsigned_short_t_t ret = new SWIGTYPE_p_std__mapT_unsigned_short_Ogre__NodeAnimationTrack_p_std__lessT_unsigned_short_t_t(OgrePINVOKE.Animation__getNodeTrackList(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__mapT_unsigned_short_Ogre__NumericAnimationTrack_p_std__lessT_unsigned_short_t_t _getNumericTrackList() {
    SWIGTYPE_p_std__mapT_unsigned_short_Ogre__NumericAnimationTrack_p_std__lessT_unsigned_short_t_t ret = new SWIGTYPE_p_std__mapT_unsigned_short_Ogre__NumericAnimationTrack_p_std__lessT_unsigned_short_t_t(OgrePINVOKE.Animation__getNumericTrackList(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__mapT_unsigned_short_Ogre__VertexAnimationTrack_p_std__lessT_unsigned_short_t_t _getVertexTrackList() {
    SWIGTYPE_p_std__mapT_unsigned_short_Ogre__VertexAnimationTrack_p_std__lessT_unsigned_short_t_t ret = new SWIGTYPE_p_std__mapT_unsigned_short_Ogre__VertexAnimationTrack_p_std__lessT_unsigned_short_t_t(OgrePINVOKE.Animation__getVertexTrackList(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void optimise(bool discardIdentityNodeTracks) {
    OgrePINVOKE.Animation_optimise__SWIG_0(swigCPtr, discardIdentityNodeTracks);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void optimise() {
    OgrePINVOKE.Animation_optimise__SWIG_1(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _collectIdentityNodeTracks(SWIGTYPE_p_std__setT_unsigned_short_t tracks) {
    OgrePINVOKE.Animation__collectIdentityNodeTracks(swigCPtr, SWIGTYPE_p_std__setT_unsigned_short_t.getCPtr(tracks));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _destroyNodeTracks(SWIGTYPE_p_std__setT_unsigned_short_t tracks) {
    OgrePINVOKE.Animation__destroyNodeTracks(swigCPtr, SWIGTYPE_p_std__setT_unsigned_short_t.getCPtr(tracks));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Animation clone(string newName) {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_clone(swigCPtr, newName);
    Animation ret = (cPtr == global::System.IntPtr.Zero) ? null : new Animation(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _keyFrameListChanged() {
    OgrePINVOKE.Animation__keyFrameListChanged(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public TimeIndex _getTimeIndex(float timePos) {
    TimeIndex ret = new TimeIndex(OgrePINVOKE.Animation__getTimeIndex(swigCPtr, timePos), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setUseBaseKeyFrame(bool useBaseKeyFrame, float keyframeTime, string baseAnimName) {
    OgrePINVOKE.Animation_setUseBaseKeyFrame__SWIG_0(swigCPtr, useBaseKeyFrame, keyframeTime, baseAnimName);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setUseBaseKeyFrame(bool useBaseKeyFrame, float keyframeTime) {
    OgrePINVOKE.Animation_setUseBaseKeyFrame__SWIG_1(swigCPtr, useBaseKeyFrame, keyframeTime);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setUseBaseKeyFrame(bool useBaseKeyFrame) {
    OgrePINVOKE.Animation_setUseBaseKeyFrame__SWIG_2(swigCPtr, useBaseKeyFrame);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getUseBaseKeyFrame() {
    bool ret = OgrePINVOKE.Animation_getUseBaseKeyFrame(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float getBaseKeyFrameTime() {
    float ret = OgrePINVOKE.Animation_getBaseKeyFrameTime(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getBaseKeyFrameAnimationName() {
    string ret = OgrePINVOKE.Animation_getBaseKeyFrameAnimationName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void _applyBaseKeyFrame() {
    OgrePINVOKE.Animation__applyBaseKeyFrame(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _notifyContainer(AnimationContainer c) {
    OgrePINVOKE.Animation__notifyContainer(swigCPtr, AnimationContainer.getCPtr(c));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public AnimationContainer getContainer() {
    global::System.IntPtr cPtr = OgrePINVOKE.Animation_getContainer(swigCPtr);
    AnimationContainer ret = (cPtr == global::System.IntPtr.Zero) ? null : new AnimationContainer(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public enum InterpolationMode : byte {
    IM_LINEAR,
    IM_SPLINE
  }

  public enum RotationInterpolationMode : byte {
    RIM_LINEAR,
    RIM_SPHERICAL
  }

}

}
