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

public class AnimationState : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal AnimationState(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(AnimationState obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(AnimationState obj) {
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

  ~AnimationState() {
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
          OgrePINVOKE.delete_AnimationState(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public AnimationState(string animName, AnimationStateSet parent, float timePos, float length, float weight, bool enabled) : this(OgrePINVOKE.new_AnimationState__SWIG_0(animName, AnimationStateSet.getCPtr(parent), timePos, length, weight, enabled), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public AnimationState(string animName, AnimationStateSet parent, float timePos, float length, float weight) : this(OgrePINVOKE.new_AnimationState__SWIG_1(animName, AnimationStateSet.getCPtr(parent), timePos, length, weight), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public AnimationState(string animName, AnimationStateSet parent, float timePos, float length) : this(OgrePINVOKE.new_AnimationState__SWIG_2(animName, AnimationStateSet.getCPtr(parent), timePos, length), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public AnimationState(AnimationStateSet parent, AnimationState rhs) : this(OgrePINVOKE.new_AnimationState__SWIG_3(AnimationStateSet.getCPtr(parent), AnimationState.getCPtr(rhs)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public string getAnimationName() {
    string ret = OgrePINVOKE.AnimationState_getAnimationName(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float getTimePosition() {
    float ret = OgrePINVOKE.AnimationState_getTimePosition(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setTimePosition(float timePos) {
    OgrePINVOKE.AnimationState_setTimePosition(swigCPtr, timePos);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public float getLength() {
    float ret = OgrePINVOKE.AnimationState_getLength(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setLength(float len) {
    OgrePINVOKE.AnimationState_setLength(swigCPtr, len);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public float getWeight() {
    float ret = OgrePINVOKE.AnimationState_getWeight(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setWeight(float weight) {
    OgrePINVOKE.AnimationState_setWeight(swigCPtr, weight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void addTime(float offset) {
    OgrePINVOKE.AnimationState_addTime(swigCPtr, offset);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool hasEnded() {
    bool ret = OgrePINVOKE.AnimationState_hasEnded(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool getEnabled() {
    bool ret = OgrePINVOKE.AnimationState_getEnabled(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setEnabled(bool enabled) {
    OgrePINVOKE.AnimationState_setEnabled(swigCPtr, enabled);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setLoop(bool loop) {
    OgrePINVOKE.AnimationState_setLoop(swigCPtr, loop);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getLoop() {
    bool ret = OgrePINVOKE.AnimationState_getLoop(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void copyStateFrom(AnimationState animState) {
    OgrePINVOKE.AnimationState_copyStateFrom(swigCPtr, AnimationState.getCPtr(animState));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public AnimationStateSet getParent() {
    global::System.IntPtr cPtr = OgrePINVOKE.AnimationState_getParent(swigCPtr);
    AnimationStateSet ret = (cPtr == global::System.IntPtr.Zero) ? null : new AnimationStateSet(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void createBlendMask(uint blendMaskSizeHint, float initialWeight) {
    OgrePINVOKE.AnimationState_createBlendMask__SWIG_0(swigCPtr, blendMaskSizeHint, initialWeight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void createBlendMask(uint blendMaskSizeHint) {
    OgrePINVOKE.AnimationState_createBlendMask__SWIG_1(swigCPtr, blendMaskSizeHint);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void destroyBlendMask() {
    OgrePINVOKE.AnimationState_destroyBlendMask(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _setBlendMask(SplitPointList blendMask) {
    OgrePINVOKE.AnimationState__setBlendMask(swigCPtr, SplitPointList.getCPtr(blendMask));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public SplitPointList getBlendMask() {
    global::System.IntPtr cPtr = OgrePINVOKE.AnimationState_getBlendMask(swigCPtr);
    SplitPointList ret = (cPtr == global::System.IntPtr.Zero) ? null : new SplitPointList(cPtr, false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool hasBlendMask() {
    bool ret = OgrePINVOKE.AnimationState_hasBlendMask(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setBlendMaskEntry(uint boneHandle, float weight) {
    OgrePINVOKE.AnimationState_setBlendMaskEntry(swigCPtr, boneHandle, weight);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public float getBlendMaskEntry(uint boneHandle) {
    float ret = OgrePINVOKE.AnimationState_getBlendMaskEntry(swigCPtr, boneHandle);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
