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

public enum ShadowTechnique {
  SHADOWTYPE_NONE = 0x00,
  SHADOWDETAILTYPE_ADDITIVE = 0x01,
  SHADOWDETAILTYPE_MODULATIVE = 0x02,
  SHADOWDETAILTYPE_INTEGRATED = 0x04,
  SHADOWDETAILTYPE_STENCIL = 0x10,
  SHADOWDETAILTYPE_TEXTURE = 0x20,
  SHADOWTYPE_STENCIL_MODULATIVE = SHADOWDETAILTYPE_STENCIL|SHADOWDETAILTYPE_MODULATIVE,
  SHADOWTYPE_STENCIL_ADDITIVE = SHADOWDETAILTYPE_STENCIL|SHADOWDETAILTYPE_ADDITIVE,
  SHADOWTYPE_TEXTURE_MODULATIVE = SHADOWDETAILTYPE_TEXTURE|SHADOWDETAILTYPE_MODULATIVE,
  SHADOWTYPE_TEXTURE_ADDITIVE = SHADOWDETAILTYPE_TEXTURE|SHADOWDETAILTYPE_ADDITIVE,
  SHADOWTYPE_TEXTURE_ADDITIVE_INTEGRATED = SHADOWTYPE_TEXTURE_ADDITIVE|SHADOWDETAILTYPE_INTEGRATED,
  SHADOWTYPE_TEXTURE_MODULATIVE_INTEGRATED = SHADOWTYPE_TEXTURE_MODULATIVE|SHADOWDETAILTYPE_INTEGRATED
}

}
