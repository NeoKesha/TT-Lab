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

public class Texture : Resource {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal Texture(global::System.IntPtr cPtr, bool cMemoryOwn) : base(OgrePINVOKE.Texture_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Texture obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Texture obj) {
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
          OgrePINVOKE.delete_Texture(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public void setTextureType(TextureType ttype) {
    OgrePINVOKE.Texture_setTextureType(swigCPtr, (int)ttype);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public TextureType getTextureType() {
    TextureType ret = (TextureType)OgrePINVOKE.Texture_getTextureType(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getNumMipmaps() {
    uint ret = OgrePINVOKE.Texture_getNumMipmaps(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setNumMipmaps(uint num) {
    OgrePINVOKE.Texture_setNumMipmaps(swigCPtr, num);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool getMipmapsHardwareGenerated() {
    bool ret = OgrePINVOKE.Texture_getMipmapsHardwareGenerated(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public float getGamma() {
    float ret = OgrePINVOKE.Texture_getGamma(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setGamma(float g) {
    OgrePINVOKE.Texture_setGamma(swigCPtr, g);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setHardwareGammaEnabled(bool enabled) {
    OgrePINVOKE.Texture_setHardwareGammaEnabled(swigCPtr, enabled);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool isHardwareGammaEnabled() {
    bool ret = OgrePINVOKE.Texture_isHardwareGammaEnabled(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setFSAA(uint fsaa, string fsaaHint) {
    OgrePINVOKE.Texture_setFSAA(swigCPtr, fsaa, fsaaHint);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getFSAA() {
    uint ret = OgrePINVOKE.Texture_getFSAA(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string getFSAAHint() {
    string ret = OgrePINVOKE.Texture_getFSAAHint(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getHeight() {
    uint ret = OgrePINVOKE.Texture_getHeight(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getWidth() {
    uint ret = OgrePINVOKE.Texture_getWidth(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getDepth() {
    uint ret = OgrePINVOKE.Texture_getDepth(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getSrcHeight() {
    uint ret = OgrePINVOKE.Texture_getSrcHeight(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getSrcWidth() {
    uint ret = OgrePINVOKE.Texture_getSrcWidth(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint getSrcDepth() {
    uint ret = OgrePINVOKE.Texture_getSrcDepth(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setHeight(uint h) {
    OgrePINVOKE.Texture_setHeight(swigCPtr, h);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setWidth(uint w) {
    OgrePINVOKE.Texture_setWidth(swigCPtr, w);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setDepth(uint d) {
    OgrePINVOKE.Texture_setDepth(swigCPtr, d);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public int getUsage() {
    int ret = OgrePINVOKE.Texture_getUsage(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setUsage(int u) {
    OgrePINVOKE.Texture_setUsage(swigCPtr, u);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void createInternalResources() {
    OgrePINVOKE.Texture_createInternalResources(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void copyToTexture(TexturePtr target) {
    OgrePINVOKE.Texture_copyToTexture(swigCPtr, TexturePtr.getCPtr(target));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void loadImage(Image img) {
    OgrePINVOKE.Texture_loadImage(swigCPtr, Image.getCPtr(img));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void loadRawData(DataStreamPtr stream, ushort uWidth, ushort uHeight, PixelFormat eFormat) {
    OgrePINVOKE.Texture_loadRawData(swigCPtr, DataStreamPtr.getCPtr(stream), uWidth, uHeight, (int)eFormat);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void _loadImages(SWIGTYPE_p_std__vectorT_Ogre__Image_const_p_t images) {
    OgrePINVOKE.Texture__loadImages(swigCPtr, SWIGTYPE_p_std__vectorT_Ogre__Image_const_p_t.getCPtr(images));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public PixelFormat getFormat() {
    PixelFormat ret = (PixelFormat)OgrePINVOKE.Texture_getFormat(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public PixelFormat getDesiredFormat() {
    PixelFormat ret = (PixelFormat)OgrePINVOKE.Texture_getDesiredFormat(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public PixelFormat getSrcFormat() {
    PixelFormat ret = (PixelFormat)OgrePINVOKE.Texture_getSrcFormat(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setFormat(PixelFormat pf) {
    OgrePINVOKE.Texture_setFormat(swigCPtr, (int)pf);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool hasAlpha() {
    bool ret = OgrePINVOKE.Texture_hasAlpha(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setDesiredIntegerBitDepth(ushort bits) {
    OgrePINVOKE.Texture_setDesiredIntegerBitDepth(swigCPtr, bits);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ushort getDesiredIntegerBitDepth() {
    ushort ret = OgrePINVOKE.Texture_getDesiredIntegerBitDepth(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setDesiredFloatBitDepth(ushort bits) {
    OgrePINVOKE.Texture_setDesiredFloatBitDepth(swigCPtr, bits);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public ushort getDesiredFloatBitDepth() {
    ushort ret = OgrePINVOKE.Texture_getDesiredFloatBitDepth(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setDesiredBitDepths(ushort integerBits, ushort floatBits) {
    OgrePINVOKE.Texture_setDesiredBitDepths(swigCPtr, integerBits, floatBits);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getNumFaces() {
    uint ret = OgrePINVOKE.Texture_getNumFaces(swigCPtr);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual HardwarePixelBufferPtr getBuffer(uint face, uint mipmap) {
    HardwarePixelBufferPtr ret = new HardwarePixelBufferPtr(OgrePINVOKE.Texture_getBuffer__SWIG_0(swigCPtr, face, mipmap), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual HardwarePixelBufferPtr getBuffer(uint face) {
    HardwarePixelBufferPtr ret = new HardwarePixelBufferPtr(OgrePINVOKE.Texture_getBuffer__SWIG_1(swigCPtr, face), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual HardwarePixelBufferPtr getBuffer() {
    HardwarePixelBufferPtr ret = new HardwarePixelBufferPtr(OgrePINVOKE.Texture_getBuffer__SWIG_2(swigCPtr), false);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void convertToImage(Image destImage, bool includeMipMaps) {
    OgrePINVOKE.Texture_convertToImage__SWIG_0(swigCPtr, Image.getCPtr(destImage), includeMipMaps);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void convertToImage(Image destImage) {
    OgrePINVOKE.Texture_convertToImage__SWIG_1(swigCPtr, Image.getCPtr(destImage));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void getCustomAttribute(string name, out global::System.IntPtr pData) {
    OgrePINVOKE.Texture_getCustomAttribute__SWIG_0(swigCPtr, name, out pData);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint getCustomAttribute(string name) {
    uint ret = OgrePINVOKE.Texture_getCustomAttribute__SWIG_1(swigCPtr, name);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void createShaderAccessPoint(uint bindPoint, TextureAccess access, int mipmapLevel, int textureArrayIndex, PixelFormat format) {
    OgrePINVOKE.Texture_createShaderAccessPoint__SWIG_0(swigCPtr, bindPoint, (int)access, mipmapLevel, textureArrayIndex, (int)format);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void createShaderAccessPoint(uint bindPoint, TextureAccess access, int mipmapLevel, int textureArrayIndex) {
    OgrePINVOKE.Texture_createShaderAccessPoint__SWIG_1(swigCPtr, bindPoint, (int)access, mipmapLevel, textureArrayIndex);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void createShaderAccessPoint(uint bindPoint, TextureAccess access, int mipmapLevel) {
    OgrePINVOKE.Texture_createShaderAccessPoint__SWIG_2(swigCPtr, bindPoint, (int)access, mipmapLevel);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void createShaderAccessPoint(uint bindPoint, TextureAccess access) {
    OgrePINVOKE.Texture_createShaderAccessPoint__SWIG_3(swigCPtr, bindPoint, (int)access);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void createShaderAccessPoint(uint bindPoint) {
    OgrePINVOKE.Texture_createShaderAccessPoint__SWIG_4(swigCPtr, bindPoint);
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

  public void setLayerNames(StringList names) {
    OgrePINVOKE.Texture_setLayerNames(swigCPtr, StringList.getCPtr(names));
    if (OgrePINVOKE.SWIGPendingException.Pending) throw OgrePINVOKE.SWIGPendingException.Retrieve();
  }

}

}
