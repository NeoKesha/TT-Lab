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

public class OgreOverlay {
  public static SWIGTYPE_p_std__vectorT_unsigned_int_t utftoc32(string str) {
    SWIGTYPE_p_std__vectorT_unsigned_int_t ret = new SWIGTYPE_p_std__vectorT_unsigned_int_t(OgreOverlayPINVOKE.utftoc32(str), true);
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void DrawRenderingSettings(SWIGTYPE_p_std__string renderSystemName) {
    OgreOverlayPINVOKE.DrawRenderingSettings(SWIGTYPE_p_std__string.getCPtr(renderSystemName));
    if (OgreOverlayPINVOKE.SWIGPendingException.Pending) throw OgreOverlayPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
