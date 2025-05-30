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

public class Matrix3 : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Matrix3(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Matrix3 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Matrix3 obj) {
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

  ~Matrix3() {
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
          OgrePINVOKE.delete_Matrix3(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public Matrix3() : this(OgrePINVOKE.new_Matrix3__SWIG_0(), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Matrix3(SWIGTYPE_p_a_3__float arr) : this(OgrePINVOKE.new_Matrix3__SWIG_1(SWIGTYPE_p_a_3__float.getCPtr(arr)), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Matrix3(float fEntry00, float fEntry01, float fEntry02, float fEntry10, float fEntry11, float fEntry12, float fEntry20, float fEntry21, float fEntry22) : this(OgrePINVOKE.new_Matrix3__SWIG_2(fEntry00, fEntry01, fEntry02, fEntry10, fEntry11, fEntry12, fEntry20, fEntry21, fEntry22), true) {
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector3 GetColumn(uint iCol) {
    Vector3 ret = new Vector3(OgrePINVOKE.Matrix3_GetColumn(swigCPtr, iCol), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetColumn(uint iCol, Vector3 vec) {
    OgrePINVOKE.Matrix3_SetColumn(swigCPtr, iCol, Vector3.getCPtr(vec));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void FromAxes(Vector3 xAxis, Vector3 yAxis, Vector3 zAxis) {
    OgrePINVOKE.Matrix3_FromAxes(swigCPtr, Vector3.getCPtr(xAxis), Vector3.getCPtr(yAxis), Vector3.getCPtr(zAxis));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Matrix3 __add__(Matrix3 rkMatrix) {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3___add__(swigCPtr, Matrix3.getCPtr(rkMatrix)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 __sub__(Matrix3 rkMatrix) {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3___sub____SWIG_0(swigCPtr, Matrix3.getCPtr(rkMatrix)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 __mul__(Matrix3 rkMatrix) {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3___mul____SWIG_0(swigCPtr, Matrix3.getCPtr(rkMatrix)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 __sub__() {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3___sub____SWIG_1(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 __mul__(float fScalar) {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3___mul____SWIG_1(swigCPtr, fScalar), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 Transpose() {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3_Transpose(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Inverse(Matrix3 rkInverse, float fTolerance) {
    bool ret = OgrePINVOKE.Matrix3_Inverse__SWIG_0(swigCPtr, Matrix3.getCPtr(rkInverse), fTolerance);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Inverse(Matrix3 rkInverse) {
    bool ret = OgrePINVOKE.Matrix3_Inverse__SWIG_1(swigCPtr, Matrix3.getCPtr(rkInverse));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 Inverse(float fTolerance) {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3_Inverse__SWIG_2(swigCPtr, fTolerance), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 Inverse() {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3_Inverse__SWIG_3(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float Determinant() {
    float ret = OgrePINVOKE.Matrix3_Determinant(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 transpose() {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3_transpose(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Matrix3 inverse() {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3_inverse(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float determinant() {
    float ret = OgrePINVOKE.Matrix3_determinant(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool hasNegativeScale() {
    bool ret = OgrePINVOKE.Matrix3_hasNegativeScale(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SingularValueDecomposition(Matrix3 rkL, Vector3 rkS, Matrix3 rkR) {
    OgrePINVOKE.Matrix3_SingularValueDecomposition(swigCPtr, Matrix3.getCPtr(rkL), Vector3.getCPtr(rkS), Matrix3.getCPtr(rkR));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SingularValueComposition(Matrix3 rkL, Vector3 rkS, Matrix3 rkR) {
    OgrePINVOKE.Matrix3_SingularValueComposition(swigCPtr, Matrix3.getCPtr(rkL), Vector3.getCPtr(rkS), Matrix3.getCPtr(rkR));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public Matrix3 orthonormalised() {
    Matrix3 ret = new Matrix3(OgrePINVOKE.Matrix3_orthonormalised(swigCPtr), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void QDUDecomposition(Matrix3 rkQ, Vector3 rkD, Vector3 rkU) {
    OgrePINVOKE.Matrix3_QDUDecomposition(swigCPtr, Matrix3.getCPtr(rkQ), Vector3.getCPtr(rkD), Vector3.getCPtr(rkU));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public float SpectralNorm() {
    float ret = OgrePINVOKE.Matrix3_SpectralNorm(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void ToAngleAxis(Vector3 rkAxis, Radian rfAngle) {
    OgrePINVOKE.Matrix3_ToAngleAxis__SWIG_0(swigCPtr, Vector3.getCPtr(rkAxis), Radian.getCPtr(rfAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void ToAngleAxis(Vector3 rkAxis, Degree rfAngle) {
    OgrePINVOKE.Matrix3_ToAngleAxis__SWIG_1(swigCPtr, Vector3.getCPtr(rkAxis), Degree.getCPtr(rfAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void FromAngleAxis(Vector3 rkAxis, Radian fRadians) {
    OgrePINVOKE.Matrix3_FromAngleAxis(swigCPtr, Vector3.getCPtr(rkAxis), Radian.getCPtr(fRadians));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool ToEulerAnglesXYZ(Radian rfYAngle, Radian rfPAngle, Radian rfRAngle) {
    bool ret = OgrePINVOKE.Matrix3_ToEulerAnglesXYZ(swigCPtr, Radian.getCPtr(rfYAngle), Radian.getCPtr(rfPAngle), Radian.getCPtr(rfRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool ToEulerAnglesXZY(Radian rfYAngle, Radian rfPAngle, Radian rfRAngle) {
    bool ret = OgrePINVOKE.Matrix3_ToEulerAnglesXZY(swigCPtr, Radian.getCPtr(rfYAngle), Radian.getCPtr(rfPAngle), Radian.getCPtr(rfRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool ToEulerAnglesYXZ(Radian rfYAngle, Radian rfPAngle, Radian rfRAngle) {
    bool ret = OgrePINVOKE.Matrix3_ToEulerAnglesYXZ(swigCPtr, Radian.getCPtr(rfYAngle), Radian.getCPtr(rfPAngle), Radian.getCPtr(rfRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool ToEulerAnglesYZX(Radian rfYAngle, Radian rfPAngle, Radian rfRAngle) {
    bool ret = OgrePINVOKE.Matrix3_ToEulerAnglesYZX(swigCPtr, Radian.getCPtr(rfYAngle), Radian.getCPtr(rfPAngle), Radian.getCPtr(rfRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool ToEulerAnglesZXY(Radian rfYAngle, Radian rfPAngle, Radian rfRAngle) {
    bool ret = OgrePINVOKE.Matrix3_ToEulerAnglesZXY(swigCPtr, Radian.getCPtr(rfYAngle), Radian.getCPtr(rfPAngle), Radian.getCPtr(rfRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool ToEulerAnglesZYX(Radian rfYAngle, Radian rfPAngle, Radian rfRAngle) {
    bool ret = OgrePINVOKE.Matrix3_ToEulerAnglesZYX(swigCPtr, Radian.getCPtr(rfYAngle), Radian.getCPtr(rfPAngle), Radian.getCPtr(rfRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void FromEulerAnglesXYZ(Radian fYAngle, Radian fPAngle, Radian fRAngle) {
    OgrePINVOKE.Matrix3_FromEulerAnglesXYZ(swigCPtr, Radian.getCPtr(fYAngle), Radian.getCPtr(fPAngle), Radian.getCPtr(fRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void FromEulerAnglesXZY(Radian fYAngle, Radian fPAngle, Radian fRAngle) {
    OgrePINVOKE.Matrix3_FromEulerAnglesXZY(swigCPtr, Radian.getCPtr(fYAngle), Radian.getCPtr(fPAngle), Radian.getCPtr(fRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void FromEulerAnglesYXZ(Radian fYAngle, Radian fPAngle, Radian fRAngle) {
    OgrePINVOKE.Matrix3_FromEulerAnglesYXZ(swigCPtr, Radian.getCPtr(fYAngle), Radian.getCPtr(fPAngle), Radian.getCPtr(fRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void FromEulerAnglesYZX(Radian fYAngle, Radian fPAngle, Radian fRAngle) {
    OgrePINVOKE.Matrix3_FromEulerAnglesYZX(swigCPtr, Radian.getCPtr(fYAngle), Radian.getCPtr(fPAngle), Radian.getCPtr(fRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void FromEulerAnglesZXY(Radian fYAngle, Radian fPAngle, Radian fRAngle) {
    OgrePINVOKE.Matrix3_FromEulerAnglesZXY(swigCPtr, Radian.getCPtr(fYAngle), Radian.getCPtr(fPAngle), Radian.getCPtr(fRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void FromEulerAnglesZYX(Radian fYAngle, Radian fPAngle, Radian fRAngle) {
    OgrePINVOKE.Matrix3_FromEulerAnglesZYX(swigCPtr, Radian.getCPtr(fYAngle), Radian.getCPtr(fPAngle), Radian.getCPtr(fRAngle));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void EigenSolveSymmetric(SWIGTYPE_p_float afEigenvalue, Vector3 akEigenvector) {
    OgrePINVOKE.Matrix3_EigenSolveSymmetric(swigCPtr, SWIGTYPE_p_float.getCPtr(afEigenvalue), Vector3.getCPtr(akEigenvector));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public static void TensorProduct(Vector3 rkU, Vector3 rkV, Matrix3 rkProduct) {
    OgrePINVOKE.Matrix3_TensorProduct(Vector3.getCPtr(rkU), Vector3.getCPtr(rkV), Matrix3.getCPtr(rkProduct));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool hasScale() {
    bool ret = OgrePINVOKE.Matrix3_hasScale(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static float EPSILON {
    get {
      float ret = OgrePINVOKE.Matrix3_EPSILON_get();
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static Matrix3 ZERO {
    get {
      global::System.IntPtr cPtr = OgrePINVOKE.Matrix3_ZERO_get();
      Matrix3 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Matrix3(cPtr, false);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public static Matrix3 IDENTITY {
    get {
      global::System.IntPtr cPtr = OgrePINVOKE.Matrix3_IDENTITY_get();
      Matrix3 ret = (cPtr == global::System.IntPtr.Zero) ? null : new Matrix3(cPtr, false);
      if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public override string ToString() {
    string ret = OgrePINVOKE.Matrix3_ToString(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3 __mul__(Vector3 v) {
    Vector3 ret = new Vector3(OgrePINVOKE.Matrix3___mul____SWIG_2(swigCPtr, Vector3.getCPtr(v)), true);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
