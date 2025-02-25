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

public class Vector2 : VectorBase2 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal Vector2(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.Vector2_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Vector2 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Vector2 obj) {
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
          OgrePINVOKE.delete_Vector2(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public Vector2() : this(OgrePINVOKE.new_Vector2__SWIG_0(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector2(float _x, float _y) : this(OgrePINVOKE.new_Vector2__SWIG_1(_x, _y), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector2(float s) : this(OgrePINVOKE.new_Vector2__SWIG_2(s), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool positionEquals(Vector2 rhs, float tolerance) {
    bool ret = OgrePINVOKE.Vector2_positionEquals__SWIG_0(swigCPtr, Vector2.getCPtr(rhs), tolerance);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool positionEquals(Vector2 rhs) {
    bool ret = OgrePINVOKE.Vector2_positionEquals__SWIG_1(swigCPtr, Vector2.getCPtr(rhs));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void makeFloor(Vector2 cmp) {
    OgrePINVOKE.Vector2_makeFloor(swigCPtr, Vector2.getCPtr(cmp));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void makeCeil(Vector2 cmp) {
    OgrePINVOKE.Vector2_makeCeil(swigCPtr, Vector2.getCPtr(cmp));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public float dotProduct(VectorBase2 vec) {
    float ret = OgrePINVOKE.Vector2_dotProduct(swigCPtr, VectorBase2.getCPtr(vec));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float squaredLength() {
    float ret = OgrePINVOKE.Vector2_squaredLength(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isZeroLength() {
    bool ret = OgrePINVOKE.Vector2_isZeroLength(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float length() {
    float ret = OgrePINVOKE.Vector2_length(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float distance(Vector2 rhs) {
    float ret = OgrePINVOKE.Vector2_distance(swigCPtr, Vector2.getCPtr(rhs));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float squaredDistance(Vector2 rhs) {
    float ret = OgrePINVOKE.Vector2_squaredDistance(swigCPtr, Vector2.getCPtr(rhs));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float normalise() {
    float ret = OgrePINVOKE.Vector2_normalise(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 normalisedCopy() {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2_normalisedCopy(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isNaN() {
    bool ret = OgrePINVOKE.Vector2_isNaN(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Radian angleBetween(Vector2 dest) {
    Radian ret = new Radian(OgrePINVOKE.Vector2_angleBetween(swigCPtr, Vector2.getCPtr(dest)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 reflect(Vector2 normal) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2_reflect(swigCPtr, Vector2.getCPtr(normal)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __sub__() {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___sub____SWIG_0(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __add__() {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___add____SWIG_0(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __mul__(float s) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___mul____SWIG_0(swigCPtr, s), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __div__(float s) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___div____SWIG_0(swigCPtr, s), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __sub__(float s) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___sub____SWIG_1(swigCPtr, s), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __add__(float s) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___add____SWIG_1(swigCPtr, s), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __add__(Vector2 b) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___add____SWIG_2(swigCPtr, Vector2.getCPtr(b)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __sub__(Vector2 b) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___sub____SWIG_2(swigCPtr, Vector2.getCPtr(b)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __mul__(Vector2 b) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___mul____SWIG_1(swigCPtr, Vector2.getCPtr(b)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector2 __div__(Vector2 b) {
    Vector2 ret = new Vector2(OgrePINVOKE.Vector2___div____SWIG_1(swigCPtr, Vector2.getCPtr(b)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override string ToString() {
    string ret = OgrePINVOKE.Vector2_ToString(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void __setitem__(uint i, float v) {
    OgrePINVOKE.Vector2___setitem__(swigCPtr, i, v);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint __len__() {
    uint ret = OgrePINVOKE.Vector2___len__(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float __getitem__(uint i) {
    float ret = OgrePINVOKE.Vector2___getitem__(swigCPtr, i);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

    public static Vector2 operator+(Vector2 lhs, Vector2 rhs) { return lhs.__add__(rhs); }
    public static Vector2 operator-(Vector2 lhs, Vector2 rhs) { return lhs.__sub__(rhs); }
    public static Vector2 operator*(Vector2 lhs, Vector2 rhs) { return lhs.__mul__(rhs); }
    public static Vector2 operator/(Vector2 lhs, Vector2 rhs) { return lhs.__div__(rhs); }
    public float this[uint i] { get { return __getitem__(i); } set { __setitem__(i, value); } }
    
}

}
