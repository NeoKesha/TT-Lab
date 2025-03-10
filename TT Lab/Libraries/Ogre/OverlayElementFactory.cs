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

public class OverlayElementFactory : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal OverlayElementFactory(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OverlayElementFactory obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(OverlayElementFactory obj) {
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

  ~OverlayElementFactory() {
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
          OgreOverlayPINVOKE.delete_OverlayElementFactory(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public virtual OverlayElement createOverlayElement(string instanceName) {
    global::System.IntPtr cPtr = OgreOverlayPINVOKE.OverlayElementFactory_createOverlayElement(swigCPtr, instanceName);
    OverlayElement ret = (cPtr == global::System.IntPtr.Zero) ? null : new OverlayElement(cPtr, false);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void destroyOverlayElement(OverlayElement pElement) {
    if (SwigDerivedClassHasMethod("destroyOverlayElement", swigMethodTypes1)) OgreOverlayPINVOKE.OverlayElementFactory_destroyOverlayElementSwigExplicitOverlayElementFactory(swigCPtr, OverlayElement.getCPtr(pElement)); else OgreOverlayPINVOKE.OverlayElementFactory_destroyOverlayElement(swigCPtr, OverlayElement.getCPtr(pElement));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual string getTypeName() {
    string ret = OgreOverlayPINVOKE.OverlayElementFactory_getTypeName(swigCPtr);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public OverlayElementFactory() : this(OgreOverlayPINVOKE.new_OverlayElementFactory(), true) {
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    SwigDirectorConnect();
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("createOverlayElement", swigMethodTypes0))
      swigDelegate0 = new SwigDelegateOverlayElementFactory_0(SwigDirectorMethodcreateOverlayElement);
    if (SwigDerivedClassHasMethod("destroyOverlayElement", swigMethodTypes1))
      swigDelegate1 = new SwigDelegateOverlayElementFactory_1(SwigDirectorMethoddestroyOverlayElement);
    if (SwigDerivedClassHasMethod("getTypeName", swigMethodTypes2))
      swigDelegate2 = new SwigDelegateOverlayElementFactory_2(SwigDirectorMethodgetTypeName);
    OgreOverlayPINVOKE.OverlayElementFactory_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2);
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

      if (methodInfo.IsVirtual && (methodInfo.DeclaringType.IsSubclassOf(typeof(OverlayElementFactory))) &&
        methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType) {
        return true;
      }
    }

    return false;
  }

  private global::System.IntPtr SwigDirectorMethodcreateOverlayElement(string instanceName) {
    return OverlayElement.getCPtr(createOverlayElement(instanceName)).Handle;
  }

  private void SwigDirectorMethoddestroyOverlayElement(global::System.IntPtr pElement) {
    destroyOverlayElement((pElement == global::System.IntPtr.Zero) ? null : new OverlayElement(pElement, false));
  }

  private string SwigDirectorMethodgetTypeName() {
    return getTypeName();
  }

  public delegate global::System.IntPtr SwigDelegateOverlayElementFactory_0(string instanceName);
  public delegate void SwigDelegateOverlayElementFactory_1(global::System.IntPtr pElement);
  public delegate string SwigDelegateOverlayElementFactory_2();

  private SwigDelegateOverlayElementFactory_0 swigDelegate0;
  private SwigDelegateOverlayElementFactory_1 swigDelegate1;
  private SwigDelegateOverlayElementFactory_2 swigDelegate2;

  private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] { typeof(string) };
  private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] { typeof(OverlayElement) };
  private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] {  };
}

}
